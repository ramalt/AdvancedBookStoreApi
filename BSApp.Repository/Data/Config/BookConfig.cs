using BSApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BSApp.Repository.Data.Config;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> builder)
    {
        builder.HasData(new List<Book>{
            new Book{ Id= 1,Title="Küçük Prens", Price= 16.99M},
            new Book{ Id= 2,Title="Beyaz Diş", Price= 23.99M},
            new Book{ Id= 3,Title="Sait Faik Abasıyanık'tan Hikayeler", Price= 32.99M},
            new Book{ Id= 4,Title="Daha Adil Bir Dünya Mümkün", Price= 12.99M},
        });
    }
}
