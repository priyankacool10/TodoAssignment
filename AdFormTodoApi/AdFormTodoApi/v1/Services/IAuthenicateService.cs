using AdFormTodoApi.Core.Models;

namespace AdFormTodoApi.Services
{
    public interface IAuthenicateService
    {
        User Authenticate(string username, string password);
    }
}
