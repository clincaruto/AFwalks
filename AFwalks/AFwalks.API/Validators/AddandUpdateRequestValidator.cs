using AFwalks.API.Models.DTO;
using FluentValidation;

namespace AFwalks.API.Validators
{
	public class AddandUpdateRequestValidator
	{
		//public class AddWalkRequestValidator : AbstractValidator<AddWalkRequestVM> 
		//{
		//	public AddWalkRequestValidator() 
		//	{ 
		//		RuleFor(x => x.Name).NotEmpty();
		//		RuleFor(x => x.Length).GreaterThan(0);
		//	}
		//}


		public class UpdateWalkRequestValidator : AbstractValidator<UpdateWalkRequestVM> 
		{
			public UpdateWalkRequestValidator() 
			{
				RuleFor(x => x.Name).NotEmpty();
				RuleFor(x => x.Length).NotEmpty();
			}
		}
	}


}
