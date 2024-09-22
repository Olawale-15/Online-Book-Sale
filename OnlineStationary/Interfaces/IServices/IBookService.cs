using OnlineStationary.DTOs.BookDto;
using OnlineStationary.Response;

namespace OnlineStationary.Interfaces.IServices
{
    public interface IBookService
    {
        BaseResponse CreateBook(BookRequestModel bookRequest);
        BaseResponse UpdateBook(Guid id, BookUpdateRequest bookUpdateRequest);
        BaseResponse DeleteBook(Guid id);
        BaseResponse<BookResponseModel> BookAvalability();

        BaseResponse<BookResponseModel> GetBook(Guid id);

        BaseResponse<ICollection<BookResponseModel>> GetBooks();
        BaseResponse<ICollection<BookResponseModel>> GetAllBooksAValable();
    }
}
