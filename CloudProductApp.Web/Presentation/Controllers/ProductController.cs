using CloudProductApp.Web.Application.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudProductApp.Web.Presentation.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductController : ControllerBase
  {
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
      var query = new GetProductsQuery();
      var productDtos = await _mediator.Send(query);

      return Ok(productDtos);
    }
  }
}
