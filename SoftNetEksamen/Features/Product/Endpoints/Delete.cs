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
  public class Delete : BaseAsyncEndpoint.WithRequest<Guid>.WithResponse<DeleteProductResponse>
  {
    private readonly IRepository<Models.Product> _repository;

    public Delete(IRepository<Models.Product> repository)
    {
      _repository = repository;
    }
    
    [HttpDelete(Routes.ProductRoute + "/{id:guid}")]
    [SwaggerOperation(
      Summary = "Deletes a Product by id",
      Description = "Deletes a specific Product from db by its id",
      OperationId = "Product.Delete",
      Tags = new[] { "ProductEndpoints" })
    ]
    public override async Task<ActionResult<DeleteProductResponse>> HandleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
      var product = await _repository.DeleteAsync(id);
      return product is null ? StatusCode(500) : Ok(product.Adapt<DeleteProductResponse>());
    }
  }
}