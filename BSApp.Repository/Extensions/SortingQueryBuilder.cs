using System.Text;

namespace BSApp.Repository.Extensions;

public static class SortingQueryBuilder
{
    public static string CreateSortingQuery<T>(string queryString)
    {
        var queryParams = queryString.Trim().Split(",");// ["title", "price desc", "id asc"]

        var propertyInfos = typeof(T).GetProperties(); // ["Id", "Title", "Price"]

        var queryBuilder = new StringBuilder();
         // title ascending, price descending, id ascending
        foreach(var param in queryParams)
        {
            if(string.IsNullOrWhiteSpace(param))
                continue;

            var propNamefromQueryString = param.Split(" ")[0]; // ["title", "price", "id"]

            var bookProp = propertyInfos.FirstOrDefault(p => p.Name.Equals(propNamefromQueryString, StringComparison.InvariantCultureIgnoreCase));

            if(bookProp is null)
                continue;

            var sortBy = param.EndsWith(" desc") ? "descending" :  "ascending";

            queryBuilder.Append($"{bookProp.Name.ToString()} {sortBy}, ");
        }

        return queryBuilder.ToString().TrimEnd(',', ' ');
    }
}
