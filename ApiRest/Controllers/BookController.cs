using Microsoft.AspNetCore.Mvc;
using ApiRest.Data.VO;
using ApiRest.Model;
using ApiRest.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _booksService;

        public BookController(IBookService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _booksService.FindAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var person = await _booksService.FindByIDAsync(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookVO books)
        {
            if (books == null) return BadRequest();
            return Ok(await _booksService.CreateAsync(books));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BookVO books)
        {
            if (books == null) return BadRequest();
            return Ok(await _booksService.UpdateAsync(books));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _booksService.DeleteAsync(id);
            return NoContent();
        }
    }
}
