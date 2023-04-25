using AFwalks.API.Models.DTO;
using FluentValidation;

namespace AFwalks.API.Validators
{
	public class AuthLoginRequestValidator : AbstractValidator<LoginRequest>
	{
		public AuthLoginRequestValidator()
		{
			RuleFor(x => x.Username).NotEmpty();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}
