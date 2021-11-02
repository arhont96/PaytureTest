using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using PaytureTest.Service;
using PaytureTest.Repository;
using Serilog;

namespace PaytureTestTests.Util
{
    class MockCollection<T>
    {
        public T CreatedInstance { get; set; }
        public Mock<IOrderRepository> OrderRepository { get; set; }
        public Mock<IPayService> PayService { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public ILogger Logger { get; set; }
    }
}
