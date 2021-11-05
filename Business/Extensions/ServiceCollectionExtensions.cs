using Business.Abstract;
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts.EntityFramework;
using DataAccess.Concrete.Repositories;
using Entities.Dto;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection MyService(this IServiceCollection ServiceCollection)
        {
            ServiceCollection.AddDbContext<FashionContext>();
            ServiceCollection.AddScoped<IUnitOfWorks, UnitOfWorks>();
            ServiceCollection.AddScoped<IProductsService, ProductsManager>();
            ServiceCollection.AddScoped<ICategoriesService, CategoriesManager>();
            ServiceCollection.AddScoped<ICustomersService, CustomersManager>();
            ServiceCollection.AddScoped<IOrdersService, OrdersManager>();
            ServiceCollection.AddScoped<IUsersAdminService, UserAdminManager>();
            ServiceCollection.AddScoped<IProductImagesService, ProductImagesManager>();
            ServiceCollection.AddScoped<IVariantsService, VariantsManager>();
            ServiceCollection.AddScoped<ITemporaryBasketsService, TemporaryBasketsManager>();
            ServiceCollection.AddScoped<IOrderDetailsService, OrderDetailsManager>();
            ServiceCollection.AddScoped<IOrderInformationsService, OrderInformationsManager>();
            ServiceCollection.AddScoped<ISlidesService, SlidesManager>();
            ServiceCollection.AddSingleton<IValidator<CategoriesDto>, CategoriesValidation>();
            ServiceCollection.AddSingleton<IValidator<CustomersUpdateDto>, CustomersValidation>();
            ServiceCollection.AddSingleton<IValidator<OrderInformationsDto>, OrderInformationsValidation>();
            ServiceCollection.AddSingleton<IValidator<OrderNotesDto>, OrderNotesValidation>();
            ServiceCollection.AddSingleton<IValidator<OrderDetailsDto>, OrderDetailsValidation>();
            ServiceCollection.AddSingleton<IValidator<OrdersUpdateDto>, OrdersValidation>();
            ServiceCollection.AddSingleton<IValidator<ProductImagesDto>, ProductImagesValidation>();
            ServiceCollection.AddSingleton<IValidator<ProductsUpdateDto>, ProductsValidation>();
            ServiceCollection.AddSingleton<IValidator<UsersAdminDto>, UsersAdminValidation>();
            ServiceCollection.AddSingleton<IValidator<VariantsDto>, VariantsValidation>();
            ServiceCollection.AddSingleton<IValidator<SlidesDto>, SlidesValidation>();
            return ServiceCollection;
        }

    }
}
