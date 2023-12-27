
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksControllerr : ControllerBase
    {
        private readonly BooksService _booksService;
        public BooksControllerr(BooksService booksService) => _booksService = booksService;

        // GET: api/<BooksControllerr>
        [HttpGet]
        public async Task<List<Book>> Get() =>
            await _booksService.GetAsync();


        // GET api/<BooksControllerr>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _booksService.GetAsync(id);

            return book == null ? NotFound() : book;
        }

        // POST api/<BooksControllerr>
        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            await _booksService.CreateAsync(book);

            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        // PUT api/<BooksControllerr>/5
        
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Book updatedBook)
        {
            var book = await _booksService.GetAsync(id);
            if (book == null) return NotFound();
            updatedBook.Id = book.Id;

            await _booksService.UpdateAsync(id, updatedBook);
            return NoContent();
        }

        // DELETE api/<BooksControllerr>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _booksService.RemoveAsync(id);

            return NoContent();
        }
    }
}
