namespace BSApp.Entities.RequestFeatures;

public class BookParameters : RequestParameterBase
{
    public uint MinPrice { get; set; }
    public uint MaxPrice { get; set; } = 1000;
    public string? SearchTerm { get; set; }
    public bool ValidPriceRange => MaxPrice > MinPrice;

}
