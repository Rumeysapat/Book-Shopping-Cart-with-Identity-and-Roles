using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCardUI.Controllers;

[Authorize]
public class UserOrderController : Controller
{
    private readonly IUserOrderRepository _userOrderRepo;
    private readonly UserManager<IdentityUser> _userManager;

    public UserOrderController(
        IUserOrderRepository userOrderRepo,
        UserManager<IdentityUser> userManager)
    {
        _userOrderRepo = userOrderRepo;
        _userManager = userManager;
    }

    // SADECE kullanıcıya ait siparişler
    public async Task<IActionResult> UserOrders()
    {
        var orders = await _userOrderRepo.GetOrders();
        return View(orders);
    }
}






