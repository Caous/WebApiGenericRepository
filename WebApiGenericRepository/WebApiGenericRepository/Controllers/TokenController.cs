using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiGenericRepository.Dto;
using WebApiGenericRepository.Infraestructure.Database;
using WebApiGenericRepository.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiGenericRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public TokenController(IConfiguration config, AppDbContext appDbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _config = config;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // POST api/<TokenController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto usuario)
        {
            if (ModelState.IsValid)
            {
                User usr = await _userManager.FindByEmailAsync(usuario.Email);
                if (usr != null)
                {
                    var check = await _signInManager.CheckPasswordSignInAsync(usr, usuario.Password, false);
                    if (check.Succeeded)
                    {
                        //cria claims baseado nas informações do usuário
                        var claims = new[] {
                                             new Claim(JwtRegisteredClaimNames.Sub, _config["TokenConfigurantion:Subject"]),
                                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                             new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                                             new Claim("Id", usr.Id.ToString()),
                                             new Claim("Nome", usr.FirstName),
                                             new Claim("Login", usr.UserName),
                                             new Claim("Email", usr.Email)
                                            };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenConfigurantion:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(_config["TokenConfigurantion:Issuer"],
                                     _config["TokenConfigurantion:Audience"], claims,
                                     expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));

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
