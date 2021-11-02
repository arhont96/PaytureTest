using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaytureTest.Model
{
    public class Payment : IPayment
    {
        public string Key { get; set; }
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
        public IPayInfo PayInfo { get; set; }
        public string PaytureId { get; set; }
        public string CustomerKey { get; set; }
        public string Cheque { get; set; }
    }
}
