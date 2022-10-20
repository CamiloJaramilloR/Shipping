using Shipping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Services
{
    public interface IShippingPackageService
    {
        Task<decimal> GetLowestRate(Package package);
    }
}
