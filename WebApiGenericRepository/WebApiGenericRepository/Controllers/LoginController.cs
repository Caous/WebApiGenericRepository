using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGenericRepository.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiGenericRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
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

            }
            return UnprocessableEntity("Paramenters with error");
        }

       
    }
}
