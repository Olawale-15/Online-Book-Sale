using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStationary.DTOs.OrderDto;
using OnlineStationary.Implementations.Service;
using OnlineStationary.Interfaces.IServices;

namespace OnlineStationary.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        public OrderController(IOrderService orderService, IBookService bookService, IAuthorService authorService)
        {
            _orderService = orderService;
            _bookService = bookService;
            _authorService = authorService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MakeOrder()
        {
            //ViewBag.Books = new SelectList(_bookService.GetBooks(), "Id", "Title");
            //ViewBag.Authors = new SelectList(_authorService.GetAllAuthors(), "Id", "FullName");
            //var getallOrder = _orderService.GetOrders().Data;

            //var selectItems = getallOrder.Select(x => new
            //{
            //    Id = x.Id,
            //    TotalPrice = x.TotalPrice,
            //    //BookName = x.OrderItemResponseModels.
            //});


            //ViewBag.getallOrder = new SelectList(selectItems, "Id", "TotalPrice");
            return View();
        }

        [HttpPost]
        public IActionResult MakeOrder(OrderRequestModel orderRequestModel)
        {
            var createOrder = _orderService.CreateOrder(orderRequestModel);
            if (!createOrder.IsSucessful)
            {
                TempData["Error"] = createOrder.Message;
                return View(orderRequestModel);
            }

            TempData["sucess"] = createOrder.Message;
            return RedirectToAction("GetAllBooks", "Book");
        }

        public IActionResult GetOrder(Guid id)
        {
            var getOrder = _orderService.GetOrder(id);
            if (!getOrder.IsSucessful)
            {
                return View();
            }

            return View(getOrder);


        }
    }
}
