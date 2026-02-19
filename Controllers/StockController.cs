using BookShoppingCardUI.Constants;
using BookShoppingCartUI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookShoppingCardUI.Controllers;

[Authorize(Roles = nameof(RoleNames.Admin))]
public class StockController : Controller
{
    private readonly IStockRepository _stockRepo;

    public StockController(IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
    }

    public async Task<IActionResult> Index(string sTerm = "")
    {
        var stocks = await _stockRepo.GetStocks(sTerm);
        return View(stocks);
    }



    public async Task<IActionResult> ManageStock(int bookId)
    {
        var existingStock = await _stockRepo.GetStockByBookId(bookId);
        var stock = new StockDTO
        {
            BookId = bookId,
            Quantity = existingStock != null
        ? existingStock.Quantity : 0
        };
        return View(stock);
    }

    [HttpPost]
    public async Task<IActionResult> ManangeStock(StockDTO stock)
    {
        if (!ModelState.IsValid)
            return View(stock);
        try
        {
            await _stockRepo.ManageStock(stock);
            TempData["successMessage"] = "Stock is updated successfully.";
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Something went wrong!!";
        }

        return RedirectToAction(nameof(Index));
    }
}


