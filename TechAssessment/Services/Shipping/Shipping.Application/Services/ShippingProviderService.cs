using Shipping.Application.Contracts.Adapters;
using Shipping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Services
{
    public class ShippingProviderService:IShippingProviderService
    {
        private readonly IProviderAdapter _providerAdapter;

        public ShippingProviderService(IProviderAdapter providerAdapter)
        {
            _providerAdapter= providerAdapter;
        }

        public async Task<decimal> GetRate(Package package)
        {
            return await _providerAdapter.CalculateCost(package); 
        }
    }
}
