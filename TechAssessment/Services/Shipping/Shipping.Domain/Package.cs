using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Domain
{
    public class Package
    {
        public string SourceAdress { get; set; }
        public string DestinationAdress { get; set; }
        public int[] CartonDimensions { get; set; }
    }
}
