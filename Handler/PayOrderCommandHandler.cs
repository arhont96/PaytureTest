using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaytureTest.Command;
using PaytureTest.Model;
using PaytureTest.Service;
using Serilog;

namespace PaytureTest.Handler
{
    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, bool>
    {
        private readonly IPayService _payService;
        private readonly ILogger _logger;

        private const string Success = "True";
        private const string Failure = "False";
        private const string SecurePayment = "3DS";
        public PayOrderCommandHandler(IPayService payService, ILogger logger)
        {
            _payService = payService;
            _logger = logger;
        }
        public async Task<bool> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            request.Order.OrderPaymentState = OrderPaymentState.Processing;
            try
            {
                var result = await _payService.Pay(request.Order.Payment);

                request.Order.PaymentResponse = result;
                
                if (!string.IsNullOrEmpty(result.ErrCode))
                {
                    request.Order.OrderPaymentState = OrderPaymentState.Failed;
                    return false;
                }

                //todo: Set to enum?
                switch (result.Success)
                {
                    case Success:
                        request.Order.OrderPaymentState = OrderPaymentState.Success;
                        return true;
                    case Failure:
                        request.Order.OrderPaymentState = OrderPaymentState.Failed;
                        return false;
                    case SecurePayment:
                        request.Order.OrderPaymentState = OrderPaymentState.WaitingFor3DS;
                        return true;
                }

                return result.ErrCode == null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                request.Order.OrderPaymentState = OrderPaymentState.Failed;
                return false;
            }
        }
    }
}
