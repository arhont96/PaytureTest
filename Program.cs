using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaytureTest.Command;
using PaytureTest.Infrastructure.Modules;
using PaytureTest.Model;
using PaytureTest.Repository;
using PaytureTest.Service;
using Serilog;
using Serilog.Events;

namespace PaytureTest
{
    public class Program
    {
        private static IOrderRepository OrderRepository;
        private static IMediator Mediator;

        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            Configure();

            //Authentication + authorization
            //Access checks etc

            var order = await OrderRepository.Get();

            var command = new PayOrderCommand()
            {
                Order = order
            };

            order.OrderPaymentState = OrderPaymentState.Hold;
            var result = await Mediator.Send(command);

            if (order.OrderPaymentState == OrderPaymentState.WaitingFor3DS)
            {
                //Handle 3DS payment
            }

            if (order.OrderPaymentState == OrderPaymentState.Success)
            {
                Console.WriteLine("Some happy message");
            }
            else
            {
                Console.WriteLine("Some not so happy message");
                Console.WriteLine(order.PaymentResponse.ErrCode);
                //Handle re-try payment if needed
            }

            Console.WriteLine(JsonConvert.SerializeObject(order));
            await host.RunAsync();
        }

        public static IServiceProvider Configure()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddScoped<IPayService, PayService>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddLogging(r =>
                {
                    r.AddSerilog();
                })
                .AddSingleton(Log.Logger)
                .AddMediatR(Assembly.GetExecutingAssembly()
            );

            var services = serviceProvider.BuildServiceProvider();
            OrderRepository = services.GetRequiredService<IOrderRepository>();
            Mediator = services.GetRequiredService<IMediator>();

            var container = new ContainerBuilder();
            container.RegisterModule(new MediatRModule());
            container.Populate(serviceProvider);
            serviceProvider.BuildServiceProvider();
            
            return new AutofacServiceProvider(container.Build());
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostingContext, builder) =>
            {
                builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
            })
            .UseSerilog((_, config) =>
            {
                config
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.Console();
            });
        }
    }
}
