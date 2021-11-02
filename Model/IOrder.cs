using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaytureTest.Model
{
    public interface IOrder
    {
        Guid OrderId { get; set; }
        OrderPaymentState OrderPaymentState { get; set; }
        IPayment Payment { get; set; }
        public PaymentResponse PaymentResponse { get; set; }
    }
}
