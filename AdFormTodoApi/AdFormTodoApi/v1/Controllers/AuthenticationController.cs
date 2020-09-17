using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdFormTodoApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenicateService _authenticateService;
        public AuthenticationController(IAuthenicateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        /// <summary>
        /// Method to authenticate User
        /// </summary>
        /// <param name="User"></param>
        /// <returns>string</returns>
        [HttpPost]
        public IActionResult Post([FromBody]User userObj)
        {
            var user = _authenticateService.Authenticate(userObj.Username, userObj.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect" });

            }
            else
            {
                return Ok(new { message = "Welcome "+user.Username});
            }
        }


    }
}
