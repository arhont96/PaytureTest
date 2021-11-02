using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PaytureTest.Model;

namespace PaytureTest.Command
{
    public class PayOrderCommand : IRequest<bool>
    {
        public IOrder Order { get; set; }
    }
}
