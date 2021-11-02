using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using PaytureTest.Handler;
using PaytureTest.Repository;
using PaytureTest.Service;
using Serilog.Core;
using Serilog.Events;

namespace PaytureTestTests.Util
{
    internal static class MockUtil
    {
        public static MockCollection<PayOrderCommandHandler> GetPayCommandCommandHandler()
        {
            var mockCollection = new MockCollection<PayOrderCommandHandler>();
            mockCollection.PopulateMocks();

            mockCollection.CreatedInstance = new PayOrderCommandHandler(mockCollection.PayService.Object, mockCollection.Logger);

            return mockCollection;
        }
        public static MockCollection<T> PopulateMocks<T>(this MockCollection<T> mockCollection)
        {
            mockCollection.OrderRepository = new Mock<IOrderRepository>();
            mockCollection.PayService = new Mock<IPayService>();
            mockCollection.CancellationToken = CancellationToken.None;
            mockCollection.Logger = Logger.None;

            return mockCollection;
        }
    }
}
