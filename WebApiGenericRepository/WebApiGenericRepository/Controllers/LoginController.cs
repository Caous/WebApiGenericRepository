using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGenericRepository.Dto;
using WebApiGenericRepository.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiGenericRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUrlHelper _urlHelper;

        public LoginController(ILogger<LoginController> logger, UserManager<User> userManager, SignInManager<User> signInManager, IUrlHelper urlHelper)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _urlHelper = urlHelper;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            LoginDto loginDto = new LoginDto {
                UserName = "Tested",
                Token = "ASDASVACSXADS"
            };
            return Ok(loginDto);
        }

        

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                User usr = await _userManager.FindByEmailAsync(model.Email);

                if (usr != null)
                {
                    var checkuser = await _signInManager.CheckPasswordSignInAsync(usr, model.Password, false);
                    if (checkuser.Succeeded)
                    {
                        return Ok("User Logged");
                    }
                    else
                    {
                        return Unauthorized("Password not confered");
                    }
                }
                else
                {
                    return Conflict("Users not found");
                }
            }
            return UnprocessableEntity("Paramenters with error");
        }

       
    }
}
