using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shipping.Infrastructure.DTO
{
    public class RateApi3DTO
    {
        [XmlElement]
        public decimal Quote { get; set; }
    }
}
