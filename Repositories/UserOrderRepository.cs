using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BookShoppingCardUI.Repositories;


public class UserOrderRepository : IUserOrderRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;

    public UserOrderRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;



    }

    public async Task ChangeOrderStatus(UpdateOrderStatusModel data)
    {

        var order = await _context.Orders.FindAsync(data.OrderId);

        if (order == null)
        {

            throw new InvalidOperationException($"order withi id:{data.OrderId} does not found");
        }
        order.OrderStatusId = data.OrderStatusId;
        await _context.SaveChangesAsync();


    }


    public async Task<Order?> GetOrderById(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<IEnumerable<OrderStatus>> GetOrderStatuses()
    {
        return await _context.orderStatuses.ToListAsync();
    }

    public async Task TogglePaymentStatus(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            throw new InvalidOperationException($"order withi id:{orderId} does not found");
        }
        order.IsPaid = !order.IsPaid;
        await _context.SaveChangesAsync();

    }


    public async Task<IEnumerable<Order>> GetOrders(bool getAll = false)
    {
        var orders = _context.Orders
                       .Include(x => x.OrderStatus)
                       .Include(x => x.OrderDetail)
                       .ThenInclude(x => x.Book)
                       .ThenInclude(x => x.Genre).AsQueryable();
        if (!getAll)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            orders = orders.Where(a => a.UserId == userId);
            //                return await orders.ToListAsync();
        }

        return await orders.ToListAsync();
    }

    private string GetUserId()
    {
        var principal = _httpContextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(principal);
        return userId;
    }
}









