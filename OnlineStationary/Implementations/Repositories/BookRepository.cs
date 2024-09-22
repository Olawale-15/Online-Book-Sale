using Microsoft.EntityFrameworkCore;
using OnlineStationary.Context;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Models.Domain;

namespace OnlineStationary.Implementations.Repositories
{
    public class BookRepository:IBookRepository
    {
        private readonly ApplicationContext _context;
        public BookRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Book CreateBook(Book book)
        {
           _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public bool DeleteBook(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Book> GetAllBooks()
        {
           return  _context.Books.ToList();
        }

        public Book? GetBook(Func<Book, bool> predicate)
        {
            var getBook = _context.Books.FirstOrDefault(predicate);
            return getBook;
        }

        public Book? GetBookById(Guid id)
        {
            var getBook = _context.Books.FirstOrDefault(x => x.Id == id);
            return getBook;
        }

        public Book UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return book;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
