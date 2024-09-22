using Microsoft.AspNetCore.Hosting;
using OnlineStationary.DTOs.BookDto;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Interfaces.IServices;
using OnlineStationary.Models.Domain;
using OnlineStationary.Models.Enum;
using OnlineStationary.Response;
using System.Xml.Linq;

namespace OnlineStationary.Implementations.Service
{
    public class BookService : IBookService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICurrentUserRepository _currentUser;
        public BookService(IAuthorRepository authorRepository, IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment, ICurrentUserRepository currentUser)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
            _currentUser = currentUser;
        }
        public BaseResponse CreateBook(BookRequestModel bookRequest)
        {
            var getAuthor = _authorRepository.GetAuthor(x => x.Email == _currentUser.GetCurrentUser());
            if (getAuthor == null)
            {
                return new BaseResponse
                {
                    Message = "Author not rgistered",
                    IsSucessful = false
                };
            }
            var img = SaveImage(bookRequest.Image);
            var book = new Book
            {
                Name = bookRequest.Name,
                Tittle = bookRequest.Tittle,
                Description = bookRequest.Description,
                PageNumber = bookRequest.PageNumber,
                 Category = bookRequest.Category.ToString(),    

                Quantity = bookRequest.Quantity,
                Price = bookRequest.Price,

                Image = img,
                AuthorId = getAuthor.Id
            };
            _bookRepository.CreateBook(book);
            _bookRepository.Save();

            return new BaseResponse
            {
                Message = "Bood added",
                IsSucessful = true
            };
        }

        public BaseResponse DeleteBook(Guid id)
        {
            var getBook = _bookRepository.GetBookById(id);
            if (getBook == null) {
                return new BaseResponse
                {
                    Message = "Book not found",
                    IsSucessful = false
                };
            }
            _bookRepository.DeleteBook(getBook);
            _bookRepository.Save();
            return new BaseResponse
            {
                Message = "Book deleted sucessful",
                IsSucessful = true
            };
        }


        public BaseResponse<BookResponseModel> BookAvalability()
        {
            var getBook = _bookRepository.GetAllBooks();
            if (getBook == null) {
                return new BaseResponse<BookResponseModel>
                {
                    Message = "Book not found",
                    IsSucessful = false
                };
            }

            foreach (var book in getBook)
            {
                book.Name = book.Name;
                book.Description = book.Description;
                book.AuthorId = book.AuthorId;
                book.PageNumber = book.PageNumber;
                book.Image = book.Image;
                book.Category = book.Category;
                book.Description = book.Description;
                book.Tittle = book.Tittle; ;
                book.Price = book.Price;
                book.Quantity = book.Quantity;
                _bookRepository.UpdateBook(book);
            }
            _bookRepository.Save();



            return new BaseResponse<BookResponseModel>
            {
              //  Data = book,
                Message = "Book details",
                IsSucessful = true
            };
        }
        
        public BaseResponse<BookResponseModel> GetBook(Guid id)
        {
            var getBook = _bookRepository.GetBookById(id);
            if (getBook == null) {
                return new BaseResponse<BookResponseModel>
                {
                    Message = "Book not found",
                    IsSucessful = false
                };
            }

            var book = new BookResponseModel
            {
                Id = getBook.Id,
                AuthorId = getBook.AuthorId,
                Name = getBook.Name,
                Tittle = getBook.Tittle,
               // Category = getBook.Category,
                Description = getBook.Description,
                Image = getBook.Image,
                PageNumber = getBook.PageNumber,
                Price = getBook.Price,
            };
            return new BaseResponse<BookResponseModel>
            {
                Data = book,
                Message = "Book details",
                IsSucessful = true
            };
        }


        public BaseResponse<ICollection<BookResponseModel>> GetBooks()
        {
            var getBoooks = _bookRepository.GetAllBooks();
            if (getBoooks == null)
            {
                return new BaseResponse<ICollection<BookResponseModel>>
                {
                    Message = "Books not created",
                    IsSucessful = false
                };
            }

            var books = getBoooks.Select(x => new BookResponseModel()
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                Name = x.Name,
                Tittle = x.Tittle,
               // Category = x.Category,
                Description = x.Description,
                Image = x.Image,
                PageNumber = x.PageNumber,
                Price = x.Price,
                Quantity = x.Quantity,
               
                
            }).ToList();

            return new BaseResponse<ICollection<BookResponseModel>>
            {
                Data = books,
                Message = "List of book  created",
                IsSucessful = true
            };
        }
        public BaseResponse UpdateBook(Guid id, BookUpdateRequest bookUpdateRequest)
        {
            var getBook = _bookRepository.GetBookById(id);
            if (getBook == null)
            {
                return new BaseResponse
                {
                    Message = "Book not found",
                    IsSucessful = false
                };
            }

            var book = new Book();
            book.Name = bookUpdateRequest.Name;
            book.PageNumber = bookUpdateRequest.PageNumber;
            book.Price = bookUpdateRequest.Price;
            book.Description = bookUpdateRequest.Description;
            _bookRepository.UpdateBook(book);
            _bookRepository.Save();

            return new BaseResponse
            {
                Message = "Update sucessful",
                IsSucessful = true
            };
        }


        private string SaveImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            var uploadDir = "uploads";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var uniqueFileName = Guid.NewGuid().ToString().Substring(0, 5) + "_" + file.FileName;
            var fullPath = Path.Combine(filePath, uniqueFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return $"/{uploadDir}/{uniqueFileName}";
        }

        public BaseResponse<ICollection<BookResponseModel>> GetAllBooksAValable()
        {
            var booksAllabe  = BookAvalability();
            if (booksAllabe.IsSucessful  == false )
            {
                return new BaseResponse<ICollection<BookResponseModel>>
                {
                    Message = booksAllabe.Message,
                    IsSucessful = false,
                };
            }

            var geAllBoooks = _bookRepository.GetAllBooks();
            if (geAllBoooks == null)
            {
                return new BaseResponse<ICollection<BookResponseModel>>
                {
                    Message = "Books not created",
                    IsSucessful = false
                };
            }

            return new BaseResponse<ICollection<BookResponseModel>>
            {

                Message = "List of book  created",
                IsSucessful = true,
                Data = geAllBoooks.Select(x => new BookResponseModel
                {
                    Id = x.Id,
                    AuthorId = x.AuthorId,
                    Name = x.Name,
                    Tittle = x.Tittle,
                    // Category = x.Category,
                    Description = x.Description,
                    Image = x.Image,
                    PageNumber = x.PageNumber,
                    Price = x.Price,
                    Quantity = x.Quantity,
                }).ToList()

            };

        }
    }
}
