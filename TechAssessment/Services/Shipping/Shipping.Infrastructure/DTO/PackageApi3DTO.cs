using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shipping.Infrastructure.DTO
{
    public class PackageApi3DTO
    {
        [XmlElement]
        public string Source { get; set; }
        [XmlElement]
        public string Destination { get; set; }
        [XmlArray("Packages")]
        public int[] Package { get; set; }

    }
}
