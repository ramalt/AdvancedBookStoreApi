namespace BSApp.Entities.RequestFeatures;

public class BookParameters : RequestParameterBase
{
    public uint MinPrice { get; set; }
    public uint MaxPrice { get; set; }
    public string? SearchTerm { get; set; }
    public bool ValidPriceRange => MaxPrice > MinPrice;

}
