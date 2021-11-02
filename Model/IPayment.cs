using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaytureTest.Model
{
    public interface IPayment
    {
        /// <summary>
        /// Наименование платёжного Терминала
        /// </summary>
        string Key { get; set; }

        Guid OrderId { get; set; }
        /// <summary>
        /// Сумма платежа в копейках
        /// </summary>
        int Amount { get; set; }
        /// <summary>
        /// Параметры для совершения транзакции
        /// </summary>
        IPayInfo PayInfo { get; set; }
        /// <summary>
        /// Идентификатор платежа в системе PaytureAntiFraud
        /// </summary>
        string PaytureId { get; set; }
        /// <summary>
        /// Идентификатор покупателя в системе PaytureAntiFraud
        /// </summary>
        string CustomerKey { get; set; }
        /// <summary>
        /// Информация о чеке в формате Json
        /// </summary>
        public string Cheque { get; set; }
    }
}
