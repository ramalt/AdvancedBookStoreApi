using BSApp.Entities.Models;
using BSApp.Entities.RequestFeatures;

namespace BSApp.Repository.Extensions;

public static class BookRepositoryExtesions
{
    public static IQueryable<Book> Search(this IQueryable<Book> books, BookParameters bookParams)
    {
        if(bookParams.SearchTerm is null)
            return books;

         return books.Where(b => b.Title.ToLower().Contains(bookParams.SearchTerm));
    }
}
