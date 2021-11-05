using Entities.Dto;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
   public class SlidesValidation:AbstractValidator<SlidesDto>
    {
        public SlidesValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Boş Bırakılamaz");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("30 Karakterden Fazla Girilemez.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Boş Bırakılamaz");
            RuleFor(x => x.Title).MaximumLength(100).WithMessage("100 Karakterden Fazla Girilemez.");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Boş Bırakılamaz");

        }
    }
}
