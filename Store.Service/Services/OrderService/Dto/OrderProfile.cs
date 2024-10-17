using AutoMapper;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.Dto
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<Order, OrderDetailsDto>()
                .ForMember(dest => dest.DeliveryMethodName, options => options.MapFrom(SRC => SRC.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, options => options.MapFrom(SRC => SRC.DeliveryMethod.Price));
          
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(SRC => SRC.ProductItem.ProductId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(SRC => SRC.ProductItem.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureUrlResolver>()).ReverseMap();
        }
    }
}
