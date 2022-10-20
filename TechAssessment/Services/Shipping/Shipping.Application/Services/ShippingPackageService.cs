using Shipping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Services
{
    public class ShippingPackageService:IShippingPackageService
    {
        public readonly IEnumerable<IShippingProviderService> _listShippingProviderService;

        public ShippingPackageService(IEnumerable<IShippingProviderService> listShippingProviderService)
        {
            _listShippingProviderService = listShippingProviderService;
        }

        public async Task<decimal> GetLowestRate(Package package)
        {
            List<Task<decimal>> tasks = new List<Task<decimal>>();
            foreach(IShippingProviderService provider in _listShippingProviderService)
            {
                tasks.Add(provider.GetRate(package));
            }
            await Task.WhenAll(tasks);
            
            var lowestRate= tasks.Min(task => task.Result);

            return lowestRate;
        }
    }
}
