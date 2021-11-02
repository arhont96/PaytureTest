using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaytureTest.Model
{
    public class Order : IOrder
    {
        public Guid OrderId { get; set; }
        public OrderPaymentState OrderPaymentState { get; set; }
        public IPayment Payment { get; set; } = null;
        public PaymentResponse PaymentResponse { get; set; } = null;
    }

    public enum OrderPaymentState
    {
        New,
        Hold,
        Processing,
        WaitingFor3DS,
        Failed,
        Success
    }
}
