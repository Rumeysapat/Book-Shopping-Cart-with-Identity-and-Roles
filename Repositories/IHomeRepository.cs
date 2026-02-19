namespace BookShoppingCardUI.Repositories;



public interface IHomeRepository
{
    Task<IEnumerable<Book>> GetBooks(string sTerm = "", int categoryId = 0);
    Task<IEnumerable<Genre>> GetGenres();
}