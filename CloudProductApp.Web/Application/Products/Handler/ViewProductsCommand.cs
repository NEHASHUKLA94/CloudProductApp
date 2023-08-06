using MediatR;
using CloudProductApp.Web.Application.Products.Queries;
using CloudProductApp.Web.Infrastructure.API.Interfaces;

namespace CloudProductApp.Web.Application.Products.Commands
{
  public class GetProductsQuery : IRequest<List<ProductDto>>
  {
  }

}


