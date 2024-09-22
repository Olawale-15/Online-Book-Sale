using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStationary.DTOs.CustomerDto;
using OnlineStationary.Interfaces.IServices;

namespace OnlineStationary.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterCustomer(CustomerRequestModel model)
        {
            var createCustomer = _customerService.CreateCustomer(model);
            if (!createCustomer.IsSucessful)
            {
                TempData["error"] = createCustomer.Message;
                return View(createCustomer);
            }
            TempData["success"] = createCustomer.Message;
            return RedirectToAction("Login", "User");
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var deleteCustomer = _customerService.DeleteCustomer(id);
            if (!deleteCustomer.IsSucessful)
            {
                return View(deleteCustomer);
            }
            return RedirectToAction("GetAllCustomer", "Customer");
        }

        [HttpGet]
        public IActionResult GetCustomer(Guid id)
        {
            var getCustomer = _customerService.GetCustomer(id);
            if (!getCustomer.IsSucessful)
            {
                TempData["Error"] = getCustomer.Message;
            }
            TempData["sucess"] = getCustomer.Message;
            return View(getCustomer.Data);
        }

        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            var getAllCustomer = _customerService.GetAllCustomers();
            if (!getAllCustomer.IsSucessful)
            {
                TempData["Error"] = getAllCustomer.Message;
            }
            TempData["sucess"] = getAllCustomer.Message;
            return View(getAllCustomer.Data);
        }

        [HttpPost]
        public IActionResult UpdateCustomer(Guid id, CustomerUpdateRequestModel customerRequestModel)
        {
            var updateCustomer = _customerService.UpdateCustomer(id, customerRequestModel);
            if (!updateCustomer.IsSucessful) {
                TempData["error"] = updateCustomer.Message;
                return View(updateCustomer);
            }
            TempData["sucess"] = updateCustomer.Message;
            return RedirectToAction("GetAllCustomer", "Customer");
        }
    }
}
