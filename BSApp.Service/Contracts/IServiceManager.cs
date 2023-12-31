
namespace BSApp.Service.Contracts;

public interface IServiceManager
{
    public IBookService BookService { get; }
    public IAuthenticationService AuthenticationService { get; }
}
