using System.Dynamic;

namespace BSApp.Service.Contracts;

public interface IDataShaper<T> 
{
    IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString);
    ExpandoObject ShapeData(T entities, string fieldsString);

}
