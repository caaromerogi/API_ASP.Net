using FluentValidation;
using PruebaAPI.DTO;

namespace PruebaAPI.Validators;

public class OwnerValidator : AbstractValidator<CreateOwnerDTO>
{
    public OwnerValidator(){
        RuleFor(o => o.FirstName).NotEmpty().MaximumLength(2);
        RuleFor(o => o.LastName).MinimumLength(3);
    }
}