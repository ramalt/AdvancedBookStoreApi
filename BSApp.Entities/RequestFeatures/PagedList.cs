namespace BSApp.Entities.RequestFeatures;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set; }
    public PagedList(List<T> items, int count, int pageNum, int pageSize)
    {
        MetaData = new MetaData(){
            TotalCount = count,
            TotalPage = (int)Math.Ceiling(count/(double)pageSize),
            CurrentPage = pageNum,
            PageSize = pageSize,
        };
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNum, int pageSize ){
        
        var count = source.Count();
        var items = source.Skip((pageNum -1) * pageSize).Take(pageSize).ToList();

        return new(items, count, pageNum, pageSize);
    }
}
