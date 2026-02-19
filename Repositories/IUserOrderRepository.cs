namespace BookShoppingCardUI.Repositories;



public interface IUserOrderRepository
{
    Task<IEnumerable<Order>> GetOrders(bool getAll = false);

    Task ChangeOrderStatus(UpdateOrderStatusModel data);
    Task TogglePaymentStatus(int orderId);
    Task<Order?> GetOrderById(int id);
    Task<IEnumerable<OrderStatus>> GetOrderStatuses();



}