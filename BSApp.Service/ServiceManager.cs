using AutoMapper;
using BSApp.Entities.Dtos;
using BSApp.Entities.Models;
using BSApp.Repository.Data;
using BSApp.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BSApp.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IBookService> _bookService;
    private readonly Lazy<IAuthenticationService> _authenticationService;
    public ServiceManager(IRepositoryManager repositoryManager,
                          ILoggerService logger,
                          IMapper mapper,
                          IDataShaper<BookDto> shaper,
                          UserManager<User> userManager,
                          IConfiguration configuration
                          )
    {
        _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager,
                                                                    logger,
                                                                    mapper,
                                                                    shaper));
                                                                    
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(logger,
                                                                                                  mapper,
                                                                                                  userManager,
                                                                                                  configuration));
    }
    public IBookService BookService => _bookService.Value;

    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}
