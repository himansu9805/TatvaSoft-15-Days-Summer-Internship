using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : BaseRepository
    {
        public ListResponse<Book> GetBooks(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower().Trim();
            var query = _context.Books.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();
            List<Book> books = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Book>
            {
                records = books,
                totalRecords = totalRecords,
            };
        }

        public Book GetBook(int id)
        {
            return _context.Books.FirstOrDefault(c => c.Id == id);
        }

        public Book AddBook(Book book)
        {
            var entry = _context.Books.Add(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Book UpdateBook(Book book)
        {
            var data = _context.Books.FirstOrDefault(c => c.Id == book.Id);
            if(data == null)
            {
                return null;
            }
            var entry = _context.Books.Update(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteBook(int id)
        {
            var data = _context.Books.FirstOrDefault(c => c.Id == id);
            if (data == null)
            {
                return false;
            }
            _context.Books.Remove(data);
            _context.SaveChanges();
            return true;
        }
    }
}
