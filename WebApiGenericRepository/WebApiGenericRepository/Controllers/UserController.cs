using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGenericRepository.Dto;
using WebApiGenericRepository.Model;
using WebApiGenericRepository.Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiGenericRepository.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserRepository _personRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUrlHelper _urlHelper;

        public UserController(ILogger<UserController> logger, IUserRepository personRepository, UserManager<User> userManager, SignInManager<User> signInManager, IUrlHelper urlHelper)
        {
            _logger = logger;
            _personRepository = personRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _urlHelper = urlHelper;
        }
        // GET: api/<User>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personRepository.GetAll());
        }

        // GET api/<User>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (ModelState.IsValid)
            {
                User us = await _userManager.FindByIdAsync(id.ToString());
                if (us != null)
                {

                    UserDto usertDto = new UserDto
                    {
                        UserName = us.UserName,
                        Email = us.Email,
                        FirstName = us.FirstName,
                        LastName = us.LastName,
                    };
                    CreateLinks(usertDto);
                    return Ok(usertDto);
                }
                else
                {
                    return Conflict("User not found");
                }
            }

            return UnprocessableEntity("Paramenters with error");

        }

        // POST api/<User>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto model)
        {
            if (ModelState.IsValid)
            {
                User usr = await _userManager.FindByNameAsync(model.FirstName);
                if (usr == null)
                {
                    usr = new User
                    {
                        UserName = model.UserName,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PasswordHash = model.Password,
                        Email = model.Email,
                        IdDepartament = model.DepartamentId,
                        DtInclused = DateTime.Now
                    };

                    var result = await _userManager.CreateAsync(usr, model.Password);

                    if (result.Succeeded)
                    {
                        return Ok(usr);
                    }
                    else
                    {
                        string error = string.Empty;
                        if (result.Errors != null)
                        {
                            foreach (var item in result.Errors)
                            {
                                error = error + " " + item.Description;
                            }
                        }
                        return BadRequest("Have a error in server with method to create user error: " + error);
                    }
                }
                else
                {
                    return Conflict("Users exists");
                }
            }

            return UnprocessableEntity("Paramenters with error");
        }

        // PUT api/<User>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto model)
        {
            if (ModelState.IsValid)
            {
                User usr = await _userManager.FindByIdAsync(id.ToString());
                if (usr == null)
                {
                    var check = await _signInManager.CheckPasswordSignInAsync(usr, model.Password, false);
                    if (check.Succeeded)
                    {


                        usr.UserName = model.UserName;
                        usr.FirstName = model.FirstName;
                        usr.LastName = model.LastName;
                        usr.Email = model.Email;
                        usr.IdDepartament = model.DepartamentId;


                        var result = await _userManager.UpdateAsync(usr);

                        if (result.Succeeded)
                        {
                            return Ok(usr);
                        }
                        else
                        {
                            return BadRequest("Have a error in server with method to create user");
                        }
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

        // DELETE api/<User>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                User usr = await _userManager.FindByIdAsync(id.ToString());
                if (usr == null)
                {


                    var result = await _userManager.DeleteAsync(usr);

                    if (result.Succeeded)
                    {
                        return Ok(usr);
                    }
                    else
                    {
                        return BadRequest("Have a error in server with method to deleted user");
                    }
                }
                else
                {
                    return Conflict("Users not found");
                }
            }

            return UnprocessableEntity("Paramenters with error");
        }

        private void CreateLinks(UserDto user)
        {
            user.Links.Add(new LinkDTO(_urlHelper.Link(nameof(Get), new { id = 1 }), _rel: "self", _method: "GET"));
            user.Links.Add(new LinkDTO(_urlHelper.Link(nameof(Put), new { id = 1 }), _rel: "update-cliente", _method: "PUT"));
            user.Links.Add(new LinkDTO(_urlHelper.Link(nameof(Delete), new { id = 1 }), _rel: "delete-cliente", _method: "DELETE"));
        }
    }
}
