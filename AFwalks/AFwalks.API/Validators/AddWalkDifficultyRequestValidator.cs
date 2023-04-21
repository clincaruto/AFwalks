using AFwalks.API.Models.DTO;
using FluentValidation;

namespace AFwalks.API.Validators
{
	public class AddWalkDifficultyRequestValidator : AbstractValidator<AddWalkDifficultyRequestVM>
	{
		public AddWalkDifficultyRequestValidator()
		{
			RuleFor(x => x.Code).NotEmpty();
		}
	}
}
