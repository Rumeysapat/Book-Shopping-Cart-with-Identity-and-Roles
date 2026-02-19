using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCardUI.Repositories;




public class HomeRepository : IHomeRepository
{
    private readonly ApplicationDbContext _context;

    public HomeRepository(ApplicationDbContext context)
    {
        _context = context;

    }


    public async Task<IEnumerable<Book>> GetBooks(string sTerm = "", int categoryId = 0)

    {
        return await _context.Books
            .Where(b => string.IsNullOrWhiteSpace(sTerm) || (b.BookName != null && b.BookName.StartsWith(sTerm)))
            .Where(b => categoryId == 0 || b.GenreId == categoryId)
            .Select(b => new Book
            {
                Id = b.Id,
                BookName = b.BookName,
                AuthorName = b.AuthorName,
                Price = b.Price,
                Image = b.Image,
                GenreId = b.GenreId,

                // navigation property
                Genre = b.Genre,

                // NotMapped alanlar
                GenreName = b.Genre.GenreName,
                Quantity = 0
            })
            .ToListAsync();
    }


    public async Task<IEnumerable<Genre>> GetGenres()
    {
        return await _context.Genres.ToListAsync();
    }




}

