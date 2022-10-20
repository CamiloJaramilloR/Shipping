using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Domain;

namespace Shipping.Application.Contracts.Adapters
{
    public interface IProviderAdapter
    {
        Task<decimal> CalculateCost(Package package);
    }
}
