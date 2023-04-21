using AFwalks.API.Models.DTO;
using FluentValidation;

namespace AFwalks.API.Validators
{
	public class UpdateWalkDifficultyRequestValidator : AbstractValidator<UpdateWalkDifficultyRequestVM>
	{
		public UpdateWalkDifficultyRequestValidator()
		{
			RuleFor(x => x.Code).NotEmpty();
		}
	}
}
