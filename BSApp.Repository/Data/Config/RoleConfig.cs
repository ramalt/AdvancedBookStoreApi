using BSApp.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BSApp.Repository.Data.Config;

public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(new List<IdentityRole>{
            new IdentityRole{Name = "DefaultAppUser", NormalizedName = "DEFAULTAPPUSER"},
            new IdentityRole{Name = "Editor", NormalizedName = "EDITOR"},
            new IdentityRole{Name = "Admin", NormalizedName = "ADMIN"},
        });
    }
}
