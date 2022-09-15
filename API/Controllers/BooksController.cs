using System.Reflection.Metadata.Ecma335;
using API.Data;
using API.Entities;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BooksController : BaseApiController
    {
        private readonly IRepository<Book, int> _bookRepository;
        public BooksController(IRepository<Book, int> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks() => await _bookRepository.GetAll();
        [HttpGet("{id}")] //api/books/id
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book =  await _bookRepository.GetById(id);
            if(book == null) return NotFound();
            return book;
        }
        [HttpPost]
        public async Task<ActionResult> CreateBook([Bind()] Book book)
        {
            if(ModelState.IsValid)
            {
                await _bookRepository.Add(book);
                await _bookRepository.Save();
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _bookRepository.Delete(id);
            await _bookRepository.Save();
            return Ok();
        }
    }
}