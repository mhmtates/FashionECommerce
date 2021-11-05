using Entities.Dto;
using FluentValidation;


namespace Business.ValidationRules.FluentValidation
{
    public class OrderDetailsValidation: AbstractValidator<OrderDetailsDto>
    {
        // Kullanıcı Model içerisinde Property'in içerisini dolduracaksa Doğrulama yapıyoruz.
        public OrderDetailsValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Name).MaximumLength(150).WithMessage("150 Karakterden Fazla Olamaz.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.VariantName).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.ProductsId).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.OrdersId).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.VariantName).MaximumLength(50).WithMessage("50 Karakterden Fazla Olamaz.");
        }
    }
}
