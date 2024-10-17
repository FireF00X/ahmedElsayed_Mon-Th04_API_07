using AutoMapper;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Product.Dto
{
    public class ProductProfile  : Profile
    {
        public ProductProfile()
        {
            CreateMap<Store.Data.Entities.Product, ProductDetailsDto>()
            .ForMember(dest => dest.BrandName, options => options.MapFrom(SRC => SRC.Brand.Name))
            .ForMember(dest => dest.TypeName, options => options.MapFrom(SRC => SRC.Type.Name))
            .ForMember(dest => dest.PictureUrl, options => options.MapFrom< ProductPictureUrlResolver>());

            CreateMap<BrandType, BrandTypesDetailsDto>();
            CreateMap<ProductType, BrandTypesDetailsDto>();
            
        }

    }
}
