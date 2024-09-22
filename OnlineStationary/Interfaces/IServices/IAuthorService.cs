using OnlineStationary.DTOs.AuthorDto;
using OnlineStationary.Response;

namespace OnlineStationary.Interfaces.IServices
{
    public interface IAuthorService
    {
        BaseResponse CreateAuthor(AuthorRequestModel authorRequest);
        BaseResponse UpdateAuthor(Guid id, AuthorUpdateRequestModel authorUpdateRequest);
        BaseResponse DeleteAuthor(Guid id);
        BaseResponse<AuthorResponseModel> GetAuthor(Guid id);
        BaseResponse<ICollection<AuthorResponseModel>> GetAllAuthors();
    }
}
