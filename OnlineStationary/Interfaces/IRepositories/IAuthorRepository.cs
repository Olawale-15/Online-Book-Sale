using OnlineStationary.Models.Domain;

namespace OnlineStationary.Interfaces.IRepositories
{
    public interface IAuthorRepository
    {
        Author CreateAuthor(Author author);
        Author UpdateAuthor(Author author);
        bool DeleteAuthor(Author author);
        Author? GetAuthor(Func<Author, bool> predicate);
        Author? GetAuthorById(Guid id);
        ICollection<Author> GetAllAuthors();
        void Save();
    }
}
