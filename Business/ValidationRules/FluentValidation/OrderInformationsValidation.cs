using Entities.Dto;
using FluentValidation;
namespace Business.ValidationRules.FluentValidation
{
    public class OrderInformationsValidation: AbstractValidator<OrderInformationsDto>
    {
        public OrderInformationsValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Sms).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.InfoDate).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.OrdersId).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.CustomersId).NotEmpty().WithMessage("Boş Bırakılamaz.");

            RuleFor(x => x.Message).MaximumLength(150).WithMessage("150 Karakterden Fazla Olamaz.");

        }
    }
}
