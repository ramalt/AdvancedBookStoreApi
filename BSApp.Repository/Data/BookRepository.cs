using BSApp.Entities.Models;
using BSApp.Entities.RequestFeatures;
using BSApp.Repository.Contracts;
using BSApp.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BSApp.Repository.Data;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(ApplicationContext context) : base(context)
    {
    }
    public void CreateOneBook(Book book) => Create(book);
    public void DeleteOneBook(Book book) => Delete(book);
    public void UpdateOneBook(Book book) => Update(book);
    public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParams, bool trackChanges) 
    {  
        var books = FindByCondition(b => ((b.Price >= bookParams.MinPrice) && (b.Price <= bookParams.MaxPrice)), trackChanges).Search(bookParams).ToList();

        return PagedList<Book>.ToPagedList(books, bookParams.PageNumber, bookParams.PageSize);
    }
    public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges) => await FindByCondition(b => b.Id == id, trackChanges).SingleOrDefaultAsync();
}
