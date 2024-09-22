using OnlineStationary.DTOs.OrderDto;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Interfaces.IServices;
using OnlineStationary.Models.Domain;
using OnlineStationary.Response;

namespace OnlineStationary.Implementations.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICurrentUserRepository _currentUserRepository;
        public OrderService(IBookRepository bookRepository, IOrderRepository orderRepository, IWalletRepository walletRepository, ICustomerRepository customerRepository, IAuthorRepository authorRepository, ICurrentUserRepository currentUserRepository)
        {
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _walletRepository = walletRepository;
            _customerRepository = customerRepository;
            _authorRepository = authorRepository;
            _currentUserRepository = currentUserRepository;
        }
        public BaseResponse CreateOrder(OrderRequestModel orderRequest)
        {
            decimal totalPrice = 0;
            var getCurrentUser = _customerRepository.GetCustomer(x => x.Email == _currentUserRepository.GetCurrentUser());
            var getCustomer = _customerRepository.GetCustomerById(getCurrentUser.Id);
            var getAuthor = _authorRepository.GetAuthorById(orderRequest.AuthorId);
            var getCustomerWallet = _walletRepository.GetCustomerWallet(getCurrentUser.Id);
            var getAuthorWallet = _walletRepository.GetAuthorWallet(orderRequest.AuthorId);

            var getBook = _bookRepository.GetBookById(orderRequest.BookId);
            if (getBook != null)
            {
                if (getBook.Quantity < orderRequest.Quantity)
                {
                    return new BaseResponse
                    {
                        Message = $"{getBook.Quantity} books available in stock",
                        IsSucessful = false,
                    };
                }
                totalPrice = getBook.Price * orderRequest.Quantity;
                if (getCustomerWallet.Balance < totalPrice)
                {
                    return new BaseResponse
                    {
                        Message = "Insuficient Balance",
                        IsSucessful = false,
                    };
                }

                getBook.Quantity -= orderRequest.Quantity;
                getCustomerWallet.Balance -= totalPrice;
                getAuthorWallet.Balance += totalPrice;

                _walletRepository.UpdateWallet(getAuthorWallet);
                _walletRepository.UpdateWallet(getCustomerWallet);
                _bookRepository.UpdateBook(getBook);
                _walletRepository.Save();
                _bookRepository.Save();

                var order = new Order
                {
                    TotalPrice = totalPrice,
                    BookId = getBook.Id,
                    Book = getBook,
                    Status = Models.Enum.Status.Delivered,
                    CreatedDate = DateTime.Now,
                    CustomerId = getCurrentUser.Id,
                    Customer = getCurrentUser,
                };

                var OrderBook = new OrderBook
                {
                    BookId = getBook.Id,
                    Book = getBook,
                    OrderId = order.Id,
                    Order = order,
                };


                _orderRepository.CreateOrder(order);
                _orderRepository.Save();
            }


            return new BaseResponse
            {
                Message = "Order sucessful",
                IsSucessful = true
            };
        }

        public BaseResponse DeleteOrder(Guid id)
        {
            var getOrder = _orderRepository.GetOrderById(id);
            if(getOrder == null)
            {
                return new BaseResponse
                {
                    Message = "Order not found",
                    IsSucessful = false
                };
            }

            _orderRepository.DeleteOrder(getOrder);

            return new BaseResponse
            {
                Message = "Order deleted sucessful",
                IsSucessful = true
            };
        }

        public BaseResponse<OrderResponseModel> GetOrder(Guid id)
        {
            var getOrder = _orderRepository.GetOrderById(id);
            if(getOrder == null)
            {
                return new BaseResponse<OrderResponseModel>
                {
                    Message = "Order not found",
                    IsSucessful = false
                };
            }

            var order = new OrderResponseModel
            {
                CreatedDate = getOrder.CreatedDate,
                TotalPrice = getOrder.TotalPrice,
                Status = getOrder.Status,
                OrderItemResponseModels = getOrder.OrderItems.Select(x => new OrderItemResponseModel()
                {
                    Quantity = x.Quantity,
                    Price = x.Book.Price,
                    BookName = x.Book.Name,
                }).ToList()
            };

            return new BaseResponse<OrderResponseModel>
            {
                Data = order,
                Message = "Order details",
                IsSucessful = true
            };
        }

        public BaseResponse<ICollection<OrderResponseModel>> GetOrders()
        {
            var getOrders = _orderRepository.GetAllOrders();
            if(getOrders == null)
            {
                return new BaseResponse<ICollection<OrderResponseModel>>
                {
                    Message = "No available order",
                    IsSucessful = false,
                    Data = null
                };
            }

            var orders = getOrders.Select(x => new OrderResponseModel()
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                TotalPrice = x.TotalPrice,
                Status = x.Status,
                OrderItemResponseModels = x.OrderItems.Select(o => new OrderItemResponseModel()
                {
                    BookName = o.Book.Name,
                    Price = o.Book.Price,
                    Quantity = o.Book.Quantity,
                }).ToList()

            }).ToList();

            return new BaseResponse<ICollection<OrderResponseModel>>
            {
                Data = orders,
                Message = "List of orders",
                IsSucessful = true
            };
        }
       
    }
}
