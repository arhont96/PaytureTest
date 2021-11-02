using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaytureTest.Model
{
    public interface IPayInfo
    {
        /// <summary>
        /// Номер карты (цифры без пробелов)
        /// </summary>
        string PAN { get; set; }
        /// <summary>
        /// Месяц истечения срока действия карты
        /// </summary>
        int EMonth { get; set; }
        /// <summary>
        /// Год истечения срока действия карты
        /// </summary>
        int EYear { get; set; }
        Guid OrderId { get; set; }
        /// <summary>
        /// Сумма платежа в копейках
        /// </summary>
        int Amount { get; set; }
        /// <summary>
        /// CVC2/CVV2 код 
        /// </summary>
        int? SecureCode { get; set; }
        /// <summary>
        /// Фамилия и имя держателя карты
        /// </summary>
        string CardHolder { get; set; }
    }
}
