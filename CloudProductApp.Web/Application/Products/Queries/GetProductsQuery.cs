using CloudProductApp.Web.Application.Products.Commands;
using CloudProductApp.Web.Infrastructure.API.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using static CloudProductApp.Web.Startup;

namespace CloudProductApp.Web.Application.Products.Queries
{

  public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
  {
    private readonly ICloudApiClient _cloudApiClient;
    private readonly string _apiKey;

    public GetProductsQueryHandler(ICloudApiClient cloudApiClient, IOptions<AppSettings> appSettings)
    {
      _cloudApiClient = cloudApiClient;
      _apiKey = appSettings.Value.ApiKey;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
      var products = await _cloudApiClient.GetAllProductsAsync(_apiKey);

      var productDtos = products.Select(p => new ProductDto
      {
        Name = p.Name,
        Description = p.Description,
        BasePrice = p.BasePrice,
        Price = p.Price
      }).ToList();

      return productDtos;
    }
  }

}

