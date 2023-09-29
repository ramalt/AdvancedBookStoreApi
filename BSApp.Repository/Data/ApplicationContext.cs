using BSApp.Entities.Models;
using BSApp.Repository.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace BSApp.Repository.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BookConfig());
    }

    public DbSet<Book> Books { get; set; }
}
