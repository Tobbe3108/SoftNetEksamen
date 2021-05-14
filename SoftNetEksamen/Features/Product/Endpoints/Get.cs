using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using SoftNetEksamen.Core;
using SoftNetEksamen.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftNetEksamen.Features.Product.Endpoints
{
  public class Get : BaseAsyncEndpoint.WithRequest<Guid>.WithResponse<GetProductResponse>
  {
    private readonly IRepository<Product.Models.Product> _repository;

    public Get(IRepository<Product.Models.Product> repository)
    {
      _repository = repository;
    }
    
    [HttpGet(Routes.ProductRoute + "/{id:guid}")]
    [SwaggerOperation(
      Summary = "Gets a Product by id",
      Description = "Get a specific Product from db by its id",
      OperationId = "Product.Get",
      Tags = new[] { "ProductEndpoints" })
    ]
    public override async Task<ActionResult<GetProductResponse>> HandleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
      var product = await _repository.ReadAsync(id);
      return product is null ? NotFound() : Ok(product.Adapt<GetProductResponse>());
    }
  }
}