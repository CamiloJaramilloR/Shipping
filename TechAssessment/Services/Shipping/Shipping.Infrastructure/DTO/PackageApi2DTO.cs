using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.DTO
{
    public class PackageApi2DTO
    {
        public string Consignee { get; set; }
        public string Consignor { get; set; }
        public int[] Cartons { get; set; }
    }
}
