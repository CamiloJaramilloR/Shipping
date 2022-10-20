using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Application.Contracts.Adapters;
using Shipping.Domain;
using Shipping.Infrastructure.Clients;

namespace Shipping.Infrastructure.Adapters
{
    public abstract class ProviderAdapter : IProviderAdapter
    {
        protected readonly IClientRest _clientRest;
        protected Uri _url;
        protected string _credentials;

        public ProviderAdapter(IClientRest clientRest, Uri url, string credentials)
        {
            this._clientRest = clientRest;
            this._url = url;
            this._credentials = credentials;                       
        }

        public abstract Task<decimal> CalculateCost(Package package);        
    }
}
