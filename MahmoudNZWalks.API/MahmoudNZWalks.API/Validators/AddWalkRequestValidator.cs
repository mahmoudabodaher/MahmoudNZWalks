using FluentValidation;
using MahmoudNZWalks.API.Models.DTOs;

namespace MahmoudNZWalks.API.Validators
{
    public class AddWalkRequestValidator : AbstractValidator<AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(r => r.Lenght).GreaterThan(0);
            RuleFor(r => r.Name).NotEmpty();
        }
    }
}
