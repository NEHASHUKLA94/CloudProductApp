using CloudProductApp.Web.Domain.Entities;
using CloudProductApp.Web.Infrastructure.API.Interfaces;
using Newtonsoft.Json;

namespace Infrastructure.API
{
  public class CloudApiClient: ICloudApiClient
  {
    private readonly HttpClient _httpClient;

    public CloudApiClient(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<List<Product>> GetAllProductsAsync(string apiKey)
    {
      _httpClient.DefaultRequestHeaders.Add("api-key", apiKey);

      var response = await _httpClient.GetAsync("api/products");

      if (response.IsSuccessStatusCode)
      {
        var content = await response.Content.ReadAsStringAsync();
        var products = JsonConvert.DeserializeObject<List<Product>>(content);

        foreach (var product in products)
        {
          // Apply the 20% markup
          product.Price = product.BasePrice * 1.20m;
        }

        return products;
      }
      else
      {
        throw new Exception($"Failed to retrieve products. Status code: {response.StatusCode}");
      }
    }
  }
}


