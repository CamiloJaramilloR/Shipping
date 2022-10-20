using AutoMapper;
using Shipping.Domain;
using Shipping.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Package, PackageApi1DTO>()
                .ForMember(m => m.ContactAdress, d => d.MapFrom(opt => opt.SourceAdress))
                .ForMember(m => m.WarehouseAdress, d => d.MapFrom(opt => opt.DestinationAdress))
                .ForMember(m => m.PackageDimensions, d => d.MapFrom(opt => opt.CartonDimensions));
            CreateMap<Package, PackageApi2DTO>()
                .ForMember(m => m.Consignor, d => d.MapFrom(opt => opt.SourceAdress))
                .ForMember(m => m.Consignee, d => d.MapFrom(opt => opt.DestinationAdress))
                .ForMember(m => m.Cartons, d => d.MapFrom(opt => opt.CartonDimensions));
            CreateMap<Package, PackageApi3DTO>()
                .ForMember(m => m.Source, d => d.MapFrom(opt => opt.SourceAdress))
                .ForMember(m => m.Destination, d => d.MapFrom(opt => opt.DestinationAdress))
                .ForMember(m => m.Package, d => d.MapFrom(opt => opt.CartonDimensions));            
        }
    }
}
