using FluentValidation;
using MahmoudNZWalks.API.Models.DTOs;

namespace MahmoudNZWalks.API.Validators
{
    public class UpdateRegionRequestValidator : AbstractValidator<UpdateRegionRequest> 
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(r => r.Code).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Area).GreaterThan(0);
            RuleFor(r => r.population).GreaterThanOrEqualTo(0);
        }
    }
}
