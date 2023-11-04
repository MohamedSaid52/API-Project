 using API.BLL.Entities;
using API.BLL.Entities.Order;
using API.DTO;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());

            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
            CreateMap<BasketItem,BasketItemDTO>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Adress,AdreesDTO>().ReverseMap();
            CreateMap<Order, OrderDetailsDTO>()
               .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
               .ForMember(dest => dest.ShippingPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDTO>()
                 .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ItemOrderd.ProductItemId))
                  .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ItemOrderd.ProductName))
                  .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.ItemOrderd.PictureUrl))
                    .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemUrlResolver>());

        }
    }
}
