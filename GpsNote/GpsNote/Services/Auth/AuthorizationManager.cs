using GpsNote.Models;
using GpsNote.Services.Repository;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GpsNote.Services.Auth
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IRepository<User> repository;

        public AuthorizationManager(IRepository<User> repo)
        {
            repository = repo;
        }


        public async Task<bool> RegUser(string name, string email, string password)
        {
            // check for the unique email
            User user = await repository.FindWithCommand($"SELECT * FROM Users WHERE Email='{email}'");

            if (user != null)
            {
                return false;
            }

            // add new user to database
            user = new User {
                Name = name,
                Email = email,
                Password = password
            };
            await repository.Add(user);

            return true;
        }

        public async Task<bool> SignIn(string email, string password)
        {
            User user = await repository.FindWithCommand($"SELECT * FROM Users WHERE Email='{email}' AND Password='{password}'");

            if (user != null)
            {
                Preferences.Set("UserId", user.Id);
                return true;
            }

            return false;
        }
    }
}
