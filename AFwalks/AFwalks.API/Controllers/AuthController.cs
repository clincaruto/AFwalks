using AFwalks.API.Models.DTO;
using AFwalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AFwalks.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : Controller
	{
		private readonly IUserRepository userRepository;
		private readonly ITokenHandler tokenHandler;

		public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
		{
			this.userRepository = userRepository;
			this.tokenHandler = tokenHandler;
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
		{
			// Validate the incoming request

			// Check if user is authenticated
			// Check username and Password
			var user = await userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

			if (user != null)
			{
				// Generate a JWT Token
				var token = await tokenHandler.CreateTokenAsync(user);
				return Ok(token);
			}

			return BadRequest("Username or Password is incorrect.");
			
		}
	}
}
