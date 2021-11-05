using AutoMapper;
using Entities.Concrete;
using Entities.Dto;

namespace Business.AutoMapper.Profiles
{
    public class AllProfile : Profile
    {
        public AllProfile()
        {
            CreateMap<ProductsUpdateDto, Products>(); 
            CreateMap<Products, ProductsUpdateDto>(); 
            CreateMap<Products, ProductsDto>(); 
            CreateMap<ProductImagesDto, ProductImages>(); 
            CreateMap<ProductImages, ProductImagesDto>(); 
            CreateMap<VariantsDto, Variants>(); 
            CreateMap<Variants, VariantsDto>(); 
            CreateMap<CategoriesDto, Categories>(); 
            CreateMap<Categories, CategoriesDto>(); 
            CreateMap<CustomersUpdateDto, Customers>(); 
            CreateMap<Customers, CustomersUpdateDto>(); 
            CreateMap<Customers, CustomersDto>(); 
            CreateMap<OrdersUpdateDto, Orders>(); 
            CreateMap<Orders, OrdersUpdateDto>(); 
            CreateMap<Orders, OrdersDto>(); 
            CreateMap<OrderDetailsDto, OrderDetails>(); 
            CreateMap<OrderDetails, OrderDetailsDto>(); 
            CreateMap<OrderInformationsDto, OrderInformations>(); 
            CreateMap<OrderInformations, OrderInformationsDto>(); 
            CreateMap<OrderNotesDto, OrderNotes>(); 
            CreateMap<OrderNotes, OrderNotesDto>(); 
            CreateMap<TemporaryBasketsDto, TemporaryBaskets>(); 
            CreateMap<TemporaryBaskets, TemporaryBasketsDto>(); 
            CreateMap<UsersAdminDto, UsersAdmin>(); 
            CreateMap<UsersAdmin, UsersAdminDto>(); 
            CreateMap<Orders, OrdersUpdateListDto>(); 
            CreateMap<OrdersUpdateListDto, Orders>(); 
            CreateMap<AutoBaskets, AutoBasketsDto>(); 
            CreateMap<AutoBasketsDto, AutoBaskets>();
            CreateMap<SlidesDto, Slides>();
            CreateMap<Slides, SlidesDto>();
        }
    }
}
