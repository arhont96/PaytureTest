using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaytureTest.Model;

namespace PaytureTest.Repository
{
    public interface IOrderRepository
    {
        Task<IOrder> Get();
    }
}
