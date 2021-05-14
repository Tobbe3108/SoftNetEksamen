using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using PhoneNumbers;
using SoftNetEksamen.Core;
using SoftNetEksamen.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftNetEksamen.Features.Product.Endpoints
{
  public class Update : BaseAsyncEndpoint.WithRequest<UpdateProductRequest>.WithResponse<UpdateProductResponse>
  {
    private readonly IRepository<Product.Models.Product> _repository;

    public Update(IRepository<Product.Models.Product> repository)
    {
      _repository = repository;
    }
    
    [HttpPut(Routes.ProductRoute)]
    [SwaggerOperation(
      Summary = "Updates a Product by its id",
      Description = "Updates a Product on the db by its id",
      OperationId = "Product.Put",
      Tags = new[] { "ProductEndpoints" })
    ]
    public override async Task<ActionResult<UpdateProductResponse>> HandleAsync(UpdateProductRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
      var result = await _repository.UpdateAsync(request.Adapt<Product.Models.Product>());
      return result ? NoContent() : StatusCode(500); 
    }
  }
}