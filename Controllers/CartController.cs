using System.Net.Quic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCardUI.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICartRepository _cartrepository;
    public CartController(ICartRepository cartRepository)
    {
        _cartrepository = cartRepository;

    }
    [Authorize]
    public async Task<IActionResult> AddItem(int bookId, int qty = 1, int redirect = 0)
    {
        var cartCount = await _cartrepository.AddItem(bookId, qty);
        if (redirect == 0)
        {
            return Ok(cartCount);

        }
        return RedirectToAction("GetUserCart");
    }
    public async Task<IActionResult> RemoveItem(int bookId)
    {
        var cart = await _cartrepository.RemoveItem(bookId);

        return RedirectToAction("GetUserCart");
    }

    public async Task<IActionResult> GetUserCart()
    {
        var carts = await _cartrepository.GetUserCart();
        var cart = carts.FirstOrDefault();

        return View(cart);
    }
    [Authorize]
    public async Task<IActionResult> GetTotalItemCount(string userId = "")
    {
        int cartItem = await _cartrepository.GetCartItemCount();
        return Ok(cartItem);
    }
    [Authorize]
    [HttpGet]
    public IActionResult CheckOut()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckOut(CheckoutModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        bool isCheckedOut = await _cartrepository.DoCheckout(model);

        if (!isCheckedOut)
        {
            ModelState.AddModelError("", "Checkout failed. Please try again.");
            return View(model);
        }

        return RedirectToAction("OrderSuccess");
    }

    [Authorize]
    [HttpGet]
    public IActionResult OrderSuccess()
    {
        return View();
    }



}