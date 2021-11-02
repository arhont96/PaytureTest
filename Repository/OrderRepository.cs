using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaytureTest.Model;

namespace PaytureTest.Repository
{
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// Mock to get some data instead of real repository
        /// </summary>
        /// <returns></returns>
        public async Task<IOrder> Get()
        {
            var id = Guid.NewGuid();
            return new Order()
            {
                OrderId = id,
                OrderPaymentState = OrderPaymentState.New,
                Payment = new Payment()
                {
                    OrderId = id,
                    Amount = 100,
                    Cheque = "",
                    CustomerKey = "SomeCustomerKey",
                    Key = "SomeKey",
                    PayInfo = new PayInfo()
                    {
                        OrderId = id,
                        Amount = 100,
                        CardHolder = "Card Holder",
                        EMonth = 12,
                        EYear = 21,
                        PAN = "4444888800003333",
                        SecureCode = 123
                    }
                }
            };
        }
    }
}
