using Microsoft.AspNetCore.Mvc;
using OnlineStationary.DTOs.BookDto;
using OnlineStationary.Interfaces.IServices;

namespace OnlineStationary.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBook(BookRequestModel model)
        {
            var createBook = _bookService.CreateBook(model);
            if (createBook == null)
            {
                return View(createBook);
            }

            return RedirectToAction("GetAllBooks");
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var getAllBooks = _bookService.GetBooks();
            return View(getAllBooks.Data);
        }
        
        [HttpGet]
        public IActionResult GetAllAvailablesBook()
        {
            var getAllBooks = _bookService.GetAllBooksAValable();
            return View(getAllBooks.Data);
        }

        [HttpGet]
        public IActionResult GetBook(Guid id)
        {
            var getBook = _bookService.GetBook(id);
            if (getBook == null)
            {
                return NotFound();
            }
            return View(getBook.Data);
        }

        [HttpDelete]
        public IActionResult DeleteBook(Guid id) { 
            var deleteBook = _bookService.DeleteBook(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPatch]
        public IActionResult UpdateBook(Guid id, BookUpdateRequest bookUpdateRequest)
        {
            var updateBook = _bookService.UpdateBook(id, bookUpdateRequest);
            if (updateBook == null)
            {
                return View(updateBook);
            }
            return RedirectToAction();
        }
    }
}
