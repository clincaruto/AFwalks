using AFwalks.API.Data;
using AFwalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AFwalks.API.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AFwalksDbContext context;

		public UserRepository(AFwalksDbContext context)
		{
			this.context = context;
		}
		public async Task<User> AuthenticateAsync(string username, string password)
		{
			var user = await context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
			if (user == null) 
			{
				return null;
			}
			
			var userRoles = await context.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();

			if (userRoles.Any())
			{
				user.Roles = new List<string>();
				foreach (var userRole in userRoles)
				{
					var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
					if (role != null)
					{
						user.Roles.Add(role.Name);
					}
				}
			}

			user.Password = null;
			return user;
		}
	}
}
