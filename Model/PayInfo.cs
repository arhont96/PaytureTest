using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaytureTest.Model
{
    public class PayInfo : IPayInfo
    {
        //todo: validation?
        public string PAN { get; set; }
        public int EMonth { get; set; }
        public int EYear { get; set; }
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
        public int? SecureCode { get; set; }
        public string CardHolder { get; set; }
    }
}
