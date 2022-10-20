using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.DTO
{
    public class PackageApi1DTO
    {
        public string ContactAdress { get; set; }
        public string WarehouseAdress { get; set; }
        public int[] PackageDimensions { get; set; }
    }
}
