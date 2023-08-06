using CloudProductApp.Web.Application.Products.Queries;
using Microsoft.EntityFrameworkCore;

namespace CloudProductApp.Web.Infrastructure.Context
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ProductDto> Products { get; set; }
  }

}
