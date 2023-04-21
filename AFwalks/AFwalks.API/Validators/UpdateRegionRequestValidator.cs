using AFwalks.API.Models.DTO;
using FluentValidation;

namespace AFwalks.API.Validators
{
	public class UpdateRegionRequestValidator :AbstractValidator<UpdateRegionRequesrVM>
	{
		public UpdateRegionRequestValidator()
		{
			RuleFor(x => x.Code).NotEmpty();
			RuleFor(x => x.Name).NotEmpty().WithMessage("This field is required");
			RuleFor(x => x.Area).GreaterThan(0);
			RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
		}
	}
}
