using Microsoft.EntityFrameworkCore;
using OnlineStationary.Context;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Models.Domain;

namespace OnlineStationary.Implementations.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationContext _context;
        public AuthorRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Author CreateAuthor(Author author)
        {
            _context.Authors.Add(author);   
            _context.SaveChanges();
            return author;
        }

        public bool DeleteAuthor(Author author)
        {
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Author> GetAllAuthors()
        {
            return _context.Authors.Include(x => x.Books).ToList();
        }

        public Author? GetAuthor(Func<Author, bool> predicate)
        {
            var getAuthor = _context.Authors.FirstOrDefault(predicate);
            return getAuthor;
        }

        public Author? GetAuthorById(Guid id)
        {
            var getAuthor = _context.Authors.
                Include(_x => _x.Books).
                FirstOrDefault(x => x.Id == id);
            return getAuthor;
        }

        public Author UpdateAuthor(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
            return author;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
