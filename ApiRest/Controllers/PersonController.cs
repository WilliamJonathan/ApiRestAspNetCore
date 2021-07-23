using Microsoft.AspNetCore.Mvc;
using ApiRest.Data.VO;
using ApiRest.Service;
using System.Threading.Tasks;

/*
 * recebe a chamada do client, 
 * e passa essa chamada para o devido controlador
 */

namespace ApiRest.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {

        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personService.FindAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var person = await _personService.FindByIDAsync(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(await _personService.CreateAsync(person));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(await _personService.UpdateAsync(person));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _personService.DeleteAsync(id);
            return NoContent();
        }

    }
}
