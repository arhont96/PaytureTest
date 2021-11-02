using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PaytureTest.Model;
using RestSharp;
using RestSharp.Serializers;

namespace PaytureTest.Service
{
    public class PayService : IPayService
    {
        private readonly RestClient _client;
        private const string BaseUri = "https://sandbox3.payture.com/api/";
        private const string PayUri = "Pay";
        private const string RefundUri = "Refund";
        private const string SecurePayUri = "3DS";
        public PayService()
        {
            _client = new RestClient(BaseUri);
        }
        public async Task<PaymentResponse> Pay(IPayment payment)
        {
            var request = new RestRequest(GetPayUri(payment), Method.POST)
            {
                XmlSerializer = new DotNetXmlSerializer(),
                RequestFormat = DataFormat.Xml
            };

            var response = await _client.ExecuteAsync<PaymentResponse>(request);

            return response.Data;
        }

        public Task<PaymentResponse> Pay3DS(IPayment payment)
        {
            throw new NotImplementedException();
        }

        public Task Refund(IPayment payment)
        {
            throw new NotImplementedException();
        }

        private string GetPayUri(IPayment payment)
        {
            return $"/{PayUri}?{CreateUri(payment)}";
        }

        private string GetRefundUri(IPayment payment)
        {
            return $"/{RefundUri}?{CreateUri(payment)}";
        }

        private string Get3DSUri(IPayment payment)
        {
            return $"/{SecurePayUri}?{CreateUri(payment)}";
        }

        private string CreateUri(object info)
        {
            var properties = new List<PropertyInfo>(info.GetType().GetProperties());
            var uri = "";
            var innerUri = "";
            foreach (var property in properties)
            {
                var propValue = property.GetValue(info, null);
                if(propValue == null)
                    continue;
                var innerProps = new List<PropertyInfo>(propValue?.GetType()?.GetProperties());
                if (innerProps.Count > 2)
                {
                    innerUri = $"{property.Name}={string.Join(";", innerProps.Select(r => $"{r.Name}={r.GetValue(propValue, null)}"))}";
                }
                else
                {
                    uri += $"{property.Name}={propValue}&";
                }
            }

            return uri + innerUri;
        }
    }
}
