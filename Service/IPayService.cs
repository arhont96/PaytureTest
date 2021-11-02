using PaytureTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaytureTest.Service
{
    public interface IPayService
    {
        Task<PaymentResponse> Pay(IPayment payment);
        Task<PaymentResponse> Pay3DS(IPayment payment);
        Task Refund(IPayment payment);
    }
}
