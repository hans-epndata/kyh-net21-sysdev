using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AuthenticationController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUp model) 
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult("All required fields must be supplied and valid");

            return await _accountManager.SignUpAsync(model);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignIn model)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult("All required fields must be supplied and valid");

            return await _accountManager.SignInAsync(model);
        }
    }
}
