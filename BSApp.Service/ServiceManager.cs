using AutoMapper;
using BSApp.Repository.Data;
using BSApp.Service.Contracts;

namespace BSApp.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IBookService> _bookService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerService logger, IMapper mapper)
    {
        _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager, logger, mapper));
    }
    public IBookService BookService => _bookService.Value;
}
