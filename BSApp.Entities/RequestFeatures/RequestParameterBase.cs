namespace BSApp.Entities.RequestFeatures;

public abstract class RequestParameterBase
{
    public int PageNumber { get; set; } = 1;   
    public string? Sort { get; set; }
    public string? Fields { get; set; }
    private int _pageSize;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > 50 ? 50 : value;}
    }
    
}
