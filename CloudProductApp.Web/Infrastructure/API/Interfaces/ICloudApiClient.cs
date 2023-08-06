using CloudProductApp.Web.Domain.Entities;

namespace CloudProductApp.Web.Infrastructure.API.Interfaces
{
  public interface ICloudApiClient
  {
    Task<List<Product>> GetAllProductsAsync(string apiKey);
  }
}
