using AdFormTodoApi.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace AdFormTodoApi.Services
{
    public class AuthenticateService : IAuthenicateService
    {
        private readonly List<User> userList = new List<User>()
        {
            new User {Username ="user1", Password="pass1" },
            new User{ Username ="user2", Password = "pass2"}

        };
        public User Authenticate(string username, string password)
        {
            var user = userList.SingleOrDefault(x=> x.Username== username && x.Password== password);
            if (user == null)
                return null;
            user.Password = null;
            return user;
        }
    }
}
