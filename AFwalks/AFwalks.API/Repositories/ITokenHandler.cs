using AFwalks.API.Models.Domain;

namespace AFwalks.API.Repositories
{
	public interface ITokenHandler
	{
		Task<string> CreateTokenAsync(User user);
	}
}
