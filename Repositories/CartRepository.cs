using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace BookShoppingCardUI.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)


    {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;

    }



    public async Task<int> GetCartItemCount(string userId = "")
    {
        if (!string.IsNullOrEmpty(userId))
        {
            userId = GetUserId();


        }
        var data = await (from cart in _context.ShoppingCarts join cartDetail in _context.CartDetails on cart.Id equals cartDetail.ShoppingCartId select new { cartDetail.Id }).ToListAsync();

        return data.Count;


    }



    public async Task<int> AddItem(int bookId, int qty)
    {
        string userId = GetUserId();
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("user in not logged-in");
            }
            var cart = await GetCart(userId);

            if (cart is null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId
                };

                _context.ShoppingCarts.Add(cart);
                _context.SaveChanges(); // cart.Id oluşması için ŞART
            }

            //cartdetails
            var cartItem = _context.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);

            if (cartItem is not null)
            {
                cartItem.Quantity += qty;
            }
            else
            {
                var book = _context.Books.Find(bookId)
                               ?? throw new ArgumentException("book not found");
                cartItem = new CartDetail
                {
                    BookId = bookId,
                    ShoppingCartId = cart.Id,
                    Quantity = qty,
                    UnitPrice = book.Price
                };
                _context.CartDetails.Add(cartItem);

            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();




        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
        var getCartItemCount = await GetCartItemCount(userId);
        return getCartItemCount;





    }


    public async Task<int> RemoveItem(int bookId)
    {
        string userId = GetUserId();
        // using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("user in not logged-in");
            }
            var cart = await GetCart(userId);

            if (cart is null)
            {
                throw new Exception(" User has not cart.");


            }

            //cartdetails
            var cartItem = await _context.CartDetails.FirstOrDefaultAsync(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);

            if (cartItem is null)
            {
                throw new Exception("cart has not item to decrease.");
            }
            else if (cartItem.Quantity == 1)
            {
                _context.CartDetails.Remove(cartItem);

            }
            else
            {
                cartItem.Quantity = cartItem.Quantity - 1;
            }

            await _context.SaveChangesAsync();
            // await transaction.CommitAsync();


        }
        catch (Exception ex)
        {
            throw new Exception("RemoveItem failed", ex);
        }
        var getCartItemCount = await GetCartItemCount(userId);
        return getCartItemCount;





    }

    public async Task<ShoppingCart> GetCart(string userId)
    {
        var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
        return cart;

    }

    private string GetUserId()
    {
        var principal = _httpContextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(principal);

        return userId;

    }


    public async Task<IEnumerable<ShoppingCart>> GetUserCart()
    {
        var userId = GetUserId();
        if (userId == null)
        {
            throw new InvalidOperationException("Invalid userid");

        }

        var ShoppingCart = await _context.ShoppingCarts.Include(a => a.CartDetails).ThenInclude(a => a.Book).ThenInclude(a => a.Genre).Where(a => a.UserId == userId).ToListAsync();
        return ShoppingCart;
    }


    public async Task<bool> DoCheckout(CheckoutModel model)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // logic
            // move data from cartDetail to order and order detail then we will remove cart detail

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User is not logged-in");
            }


            var cart = await GetCart(userId);
            if (cart is null)
            {
                throw new InvalidOperationException("Invalid cart");
            }

            var cartDetail = _context.CartDetails.Where(a => a.ShoppingCartId == cart.Id).ToList();

            if (cartDetail.Count == 0)
            {
                throw new InvalidOperationException("Cart is Empty");

            }
            // 🔽 İŞTE PENDING RECORD BURADA
            var pendingRecord = _context.orderStatuses
                .FirstOrDefault(s => s.StatusName == "Pending");
            if (pendingRecord is null)
                throw new InvalidOperationException("Pending status not found");

            var order = new Order
            {
                UserId = userId,
                CreateDate = DateTime.UtcNow,
                Name = model.Name,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                PaymentMethod = model.PaymentMethod,
                Address = model.Address,
                IsPaid = false,
                // ❗ BURASI
                OrderStatusId = pendingRecord.Id

            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in cartDetail)
            {
                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Order = order


                };

                _context.OrderDetails.Add(orderDetail);

                //Update Stock Here

                var stock = await _context.Stocks.FirstOrDefaultAsync(a => a.BookId == item.BookId);

                if (stock == null)
                {
                    throw new InvalidOperationException("Stock is null");
                }

                if (item.Quantity > stock.Quantity)
                {
                    throw new InvalidOperationException($"Only {stock.Quantity} items(s) are available in the stock");
                }
                // decrease the number of quantity from the stock table
                stock.Quantity -= item.Quantity;
            }

            // removing the cartdetails
            _context.CartDetails.RemoveRange(cartDetail);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return true;

        }
        catch
        {
            throw;
        }


    }
}


