using System.Linq.Dynamic.Core;
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
    // /books?sort=title,price desc, id asc
    public static IQueryable<Book> SortBy(this IQueryable<Book> books, string queryString)
    {
        if(string.IsNullOrWhiteSpace(queryString))
            return books.OrderBy(b => b.Id);

        var sortingQuery = SortingQueryBuilder.CreateSortingQuery<Book>(queryString);
        if (sortingQuery is null)
            return books.OrderBy(b => b.Id);

        return books.OrderBy(sortingQuery);
    }
}
