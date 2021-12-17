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
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentController : ControllerBase
    {
        private readonly ILogger<DepartamentController> _logger;

        public IDepartamentRepository _departamentRepository { get; }

        public DepartamentController(ILogger<DepartamentController> logger, IDepartamentRepository departamentRepository)
        {
            _logger = logger;
            _departamentRepository = departamentRepository;
        }
        // GET: api/<Departament>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _departamentRepository.GetAll());
        }

        // GET api/<Departament>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _departamentRepository.FindById(id));
        }

        // POST api/<Departament>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DepartamentDto model)
        {
            if (ModelState.IsValid)
            {
                Departament derp = await _departamentRepository.FindByName(model.NameDepartament);
                if (derp == null)
                {
                    derp = new Departament
                    {
                        DesDepartament = model.DesDepartament,
                        NameDepartament = model.NameDepartament,
                        DtInclused = DateTime.Now
                    };

                    derp = await _departamentRepository.Create(derp);

                    if (derp != null)
                    {
                        return Ok(derp);
                    }
                    else
                    {
                        return BadRequest("Have a error in server with method to create department");
                    }
                }
                else
                {
                    return Conflict("Departament exists");
                }
            }

            return UnprocessableEntity("Paramenters with error");
        }

        // PUT api/<Departament>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DepartamentDto model)
        {
            if (ModelState.IsValid)
            {
                Departament derp = await _departamentRepository.FindById(id);
                if (derp != null)
                {
                    derp.DesDepartament = model.DesDepartament;
                    derp.NameDepartament = model.NameDepartament;                                        

                    derp = await _departamentRepository.Update(derp);

                    if (derp != null)
                    {
                        return Ok(derp);
                    }
                    else
                    {
                        return BadRequest("Have a error in server with method to update department");
                    }
                }
                else
                {
                    return Conflict("Departament not found");
                }
            }

            return UnprocessableEntity("Paramenters with error");
        }

        // DELETE api/<Departament>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                Departament derp = await _departamentRepository.FindById(id);
                if (derp != null)
                {
                    

                    bool result = await _departamentRepository.Delete(id);

                    if (result)
                    {
                        return Ok(derp);
                    }
                    else
                    {
                        return BadRequest("Have a error in server with method to delete department");
                    }
                }
                else
                {
                    return Conflict("Departament not found");
                }
            }

            return UnprocessableEntity("Paramenters with error");
        }
    }
}
