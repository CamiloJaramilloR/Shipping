using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Clients
{
    public interface IClientRest
    {
        Task<dynamic> CallRestEndPointAsync(Uri url, string credentials, dynamic package);
    }
}
