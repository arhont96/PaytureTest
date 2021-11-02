using System;
using System.Threading.Tasks;
using Moq;
using PaytureTest.Command;
using PaytureTest.Model;
using Xunit;
using PaytureTestTests.Util;

namespace PaytureTestTests
{
    [Trait("Category", "Unit")]
    public class PayOrderCommandHandlerTest
    {
        [Fact]
        public async Task HandleAsync_PaymentSuccessful()
        {
            //Arrange
            var id = Guid.NewGuid();
            var order = new Order()
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
            var mock = MockUtil.GetPayCommandCommandHandler();
            mock.OrderRepository.Setup(r => r.Get()).ReturnsAsync(order);
            mock.PayService.Setup(r => r.Pay(order.Payment)).ReturnsAsync(new PaymentResponse()
            {
                OrderId = order.OrderId.ToString(),
                Key = "Some Key",
                Success = "True",
                ErrCode = null
            });

            var request = new PayOrderCommand()
            {
                Order = order
            };

            //Act
            var result = await mock.CreatedInstance.Handle(request, mock.CancellationToken);

            //Assert
            Assert.True(result);
            Assert.True(order.OrderPaymentState == OrderPaymentState.Success);
        }

        [Fact]
        public async Task HandleAsync_PaymentFailed()
        {
            //Arrange
            var id = Guid.NewGuid();
            var order = new Order()
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
            var mock = MockUtil.GetPayCommandCommandHandler();
            mock.OrderRepository.Setup(r => r.Get()).ReturnsAsync(order);
            mock.PayService.Setup(r => r.Pay(order.Payment)).ReturnsAsync(new PaymentResponse()
            {
                OrderId = order.OrderId.ToString(),
                Key = "Some Key",
                Success = "False",
                ErrCode = "Unauthorized"
            });

            var request = new PayOrderCommand()
            {
                Order = order
            };

            //Act
            var result = await mock.CreatedInstance.Handle(request, mock.CancellationToken);

            //Assert
            Assert.False(result);
            Assert.True(order.OrderPaymentState == OrderPaymentState.Failed);
        }

        [Fact]
        public async Task HandleAsync_PaymentThrowsException()
        {
            //Arrange
            var id = Guid.NewGuid();
            var order = new Order()
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
            var mock = MockUtil.GetPayCommandCommandHandler();
            mock.OrderRepository.Setup(r => r.Get()).ReturnsAsync(order);
            mock.PayService.Setup(r => r.Pay(order.Payment)).ThrowsAsync(new Exception("Some Exception"));

            var request = new PayOrderCommand()
            {
                Order = order
            };

            //Act
            var result = await mock.CreatedInstance.Handle(request, mock.CancellationToken);

            //Assert
            Assert.False(result);
            Assert.True(order.OrderPaymentState == OrderPaymentState.Failed);
        }

        [Fact]
        public async Task HandleAsync_PaymentAwaitsFor3DS()
        {
            //Arrange
            var id = Guid.NewGuid();
            var order = new Order()
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
            var mock = MockUtil.GetPayCommandCommandHandler();
            mock.OrderRepository.Setup(r => r.Get()).ReturnsAsync(order);
            mock.PayService.Setup(r => r.Pay(order.Payment)).ReturnsAsync(new PaymentResponse()
            {
                OrderId = order.OrderId.ToString(),
                Key = "Some Key",
                Success = "3DS",
                ErrCode = null
            });

            var request = new PayOrderCommand()
            {
                Order = order
            };

            //Act
            var result = await mock.CreatedInstance.Handle(request, mock.CancellationToken);

            //Assert
            Assert.True(result);
            Assert.True(order.OrderPaymentState == OrderPaymentState.WaitingFor3DS);
        }
    }
}
