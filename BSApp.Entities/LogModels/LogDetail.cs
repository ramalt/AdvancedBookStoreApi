using System.Text.Json;

namespace BSApp.Entities.LogModels;

public class LogDetail
{
    public Object? Controller { get; set; }
    public object? Action { get; set; }
    public object? ModelName { get; set; }
    public object? CreatedDate { get; set; }

    public LogDetail() => CreatedDate = DateTime.UtcNow;

    public override string ToString() => JsonSerializer.Serialize(this);
    

}
