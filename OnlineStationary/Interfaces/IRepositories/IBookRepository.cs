using OnlineStationary.Models.Domain;

namespace OnlineStationary.Interfaces.IRepositories
{
    public interface IBookRepository
    {
        Book CreateBook(Book book);
        Book UpdateBook(Book book);
        bool DeleteBook(Book book);
        Book? GetBookById(Guid id);
        Book? GetBook(Func<Book, bool> predicate);
        ICollection<Book> GetAllBooks();
        void Save();
    }
}
