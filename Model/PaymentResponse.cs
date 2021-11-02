using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaytureTest.Model
{
    public class PaymentResponse
    {
        /// <summary>
        /// Признак успешности операции.
        /// Может принимать значения True, False, 3DS
        /// </summary>
        public string Success { get; set; }
        public string OrderId { get; set; }
        /// <summary>
        /// Наименование платежного Терминала
        /// </summary>
        public string Key { get; set; }
        public string ErrCode { get; set; }
    }
}
