namespace BookShoppingCardUI.Models.Dto;

public class BookDisplayModel
{
    public IEnumerable<Book> Books { get; set; }
    public IEnumerable<Genre> Genres { get; set; }

    public string? Sterm { get; set; }   // 👈 ARAMA METNİ
    public int GenreId { get; set; }     // 👈 SEÇİLEN GENRE

}