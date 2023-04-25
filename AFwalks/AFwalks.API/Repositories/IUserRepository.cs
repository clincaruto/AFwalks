using AFwalks.API.Models.Domain;

namespace AFwalks.API.Repositories
{
	public interface IUserRepository
	{
		Task<User> AuthenticateAsync(string username, string password);
	}
}
