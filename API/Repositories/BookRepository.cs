using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class BookRepository : IRepository<Book, int>
    {
        private readonly LibContext _context;
        public BookRepository(LibContext context) => _context = context;
        public async Task<Book> Add(Book entity)
        {
            await _context.Books.AddAsync(entity);
            return entity;
        }
        public async Task<Boolean> Delete(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b=> b.Id == id);
            if(book != null)
            {
                _context.Remove(book);
                return true;
            }
            else return false;;
        }
        public async Task<List<Book>> GetAll() => await _context.Books.ToListAsync();
        public async Task<Book> GetById(int id) => await _context.Books.FindAsync(id);
        public async Task Save() => await _context.SaveChangesAsync();
    }
}