using System.Dynamic;
using System.Reflection;
using BSApp.Service.Contracts;

namespace BSApp.Service;
public class DataShaper<T> : IDataShaper<T> where T: class
{
    public PropertyInfo[] Properties { get; set; }

    public DataShaper()
    {
        Properties = typeof(T).GetProperties(); 
    }
    public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
    {
        var requiredProperties = GetRequiredObjectProperties(fieldsString);
        return FetchDataForEntityList(entities, requiredProperties);
    }

    public ExpandoObject ShapeData(T entity, string fieldsString)
    {
        var requiredProperties = GetRequiredObjectProperties(fieldsString);// ["id","title"]
        return FetchDataForEntity(entity, requiredProperties);
    }

    // books?fields=id, title -> ["id","title"]
    private IEnumerable<PropertyInfo> GetRequiredObjectProperties(string fieldString)
    {
        var objectProps = new List<PropertyInfo>();

        if (!string.IsNullOrWhiteSpace(fieldString))
        {
            var fields = fieldString.Split(',');//["id", " title"]
            foreach(var field in fields)
            {
                var property = Properties.FirstOrDefault(p => p.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
                if (property is null)
                    continue;

                objectProps.Add(property);//["id","title"]
            }
        }
        else
        {
            objectProps = Properties.ToList();
        }

        return objectProps;
    }

    /// <summary>
    /// Fetch data for specified T entity.
    /// </summary>
    /// <param name="entity">A model for fetch prop.</param>
    /// <param name="properties">request required fields property list.</param>
    /// <returns>Return key-value object with propery name and value.</returns>
    private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> properties)
    {
        var shapedObject = new ExpandoObject();

        foreach(var prop in properties)
        {
            object? propertyValue = prop.GetValue(entity);
            shapedObject.TryAdd(prop.Name, propertyValue); //[Id = 4, Title="..."] dictionary
        }
        return shapedObject;

    } 

    /// <summary>
    /// Fetch data for specified T entity.
    /// </summary>
    /// <param name="entity">A model for fetch prop.</param>
    /// <param name="properties">request required fields property list.</param>
    /// <returns>Return key-value object with propery name and value.</returns>
    private IEnumerable<ExpandoObject> FetchDataForEntityList(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapedData = new List<ExpandoObject>();
        foreach(var entity in entities){
            var shapedObject = FetchDataForEntity(entity, requiredProperties); 
            shapedData.Add(shapedObject);
        }
        return shapedData;
    }
}
