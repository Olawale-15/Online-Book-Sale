using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using OnlineStationary.DTOs.WalletDto;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Interfaces.IServices;
using System.Transactions;

namespace OnlineStationary.Controllers
{
    public class WalletController : Controller
    {
        private readonly IWalletService _walletService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICurrentUserRepository _currentUserRepository;
        private readonly IUserRepository _userRepository;
        public WalletController(IWalletService walletService, ITransactionRepository transactionRepository, IHttpContextAccessor httpContextAccessor, ICurrentUserRepository currentUserRepository, IUserRepository userRepository)
        {
            _walletService = walletService;
            _transactionRepository = transactionRepository;
            _contextAccessor = httpContextAccessor;
            _currentUserRepository = currentUserRepository;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult FundWallet()
        {
            return View();
        }


        [HttpPost]
        public IActionResult FundWallet(Guid id, decimal amount)
        {
            var loggedInEmail = _userRepository.GetUser(x => x.Email == _currentUserRepository.GetCurrentUser());
            if (loggedInEmail == null) {
                TempData["Error"] = "User not found";
                return RedirectToAction("Index", "Home");
            }
            
            var fundWallet = _walletService.FundWallet(id, amount);
            if(!fundWallet.IsSucessful)
            {
                TempData["Error"] = fundWallet.Message;
                return View(fundWallet);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
