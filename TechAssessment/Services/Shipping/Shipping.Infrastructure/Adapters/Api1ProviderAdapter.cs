using AutoMapper;
using Shipping.Application.Contracts.Adapters;
using Shipping.Domain;
using Shipping.Infrastructure.Clients;
using Shipping.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Adapters
{
    public class Api1ProviderAdapter : ProviderAdapter
    {
        private readonly IMapper _mapper;
        public Api1ProviderAdapter(Uri url, string credentials, IClientRest clientRest, IMapper mapper)
            : base(clientRest,url,credentials)
        {
            this._mapper = mapper;
        }
        public async override Task<decimal> CalculateCost(Package package)
        {
            var response = new RateApi1DTO();
            var packageInput = this._mapper.Map<PackageApi1DTO>(package);
            response = await this._clientRest.CallRestEndPointAsync(_url,_credentials,packageInput);
            return response.Total;            
        }
    }
}
