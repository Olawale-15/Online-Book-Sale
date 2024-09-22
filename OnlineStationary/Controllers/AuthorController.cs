using Microsoft.AspNetCore.Mvc;
using OnlineStationary.DTOs.AuthorDto;
using OnlineStationary.Interfaces.IServices;

namespace OnlineStationary.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterAuthor(AuthorRequestModel authorRequestModel)
        {
            var createAuthor = _authorService.CreateAuthor(authorRequestModel);
            if (createAuthor == null)
            {
                return View(createAuthor);
            }
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult GetAuthor(Guid id)
        {
            var getAuthorResponse = _authorService.GetAuthor(id);
            if (!getAuthorResponse.IsSucessful || getAuthorResponse.Data == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(getAuthorResponse.Data);
        }

        [HttpGet]
        public IActionResult GetAllAuthor()
        {
            var getAllAuthorResponse = _authorService.GetAllAuthors();
            if (!getAllAuthorResponse.IsSucessful || getAllAuthorResponse.Data == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(getAllAuthorResponse.Data);
        }

        [HttpPut]
        public IActionResult UpdateAuthor(Guid id, AuthorUpdateRequestModel authorUpdateRequestModel)
        {
            var updateAuthor = _authorService.UpdateAuthor(id, authorUpdateRequestModel);
            if (updateAuthor == null)
            {
                return NotFound();
            }
            return View(updateAuthor);
        }

        [HttpDelete]
        public IActionResult DeleteAuthor(Guid id)
        {
            var deleteAuthor = _authorService.DeleteAuthor(id);
            if (deleteAuthor == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
