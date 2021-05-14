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
  public class Create : BaseAsyncEndpoint.WithRequest<CreateProductRequest>.WithResponse<CreateProductResponse>
  {
    private readonly IRepository<Models.Product> _repository;

    public Create(IRepository<Models.Product> repository)
    {
      _repository = repository;
    }
    
    [HttpPost(Routes.ProductRoute)]
    [SwaggerOperation(
      Summary = "Creates a new Product",
      Description = "Creates a new Product",
      OperationId = "Product.Create",
      Tags = new[] { "ProductEndpoints" })
    ]
    public override async Task<ActionResult<CreateProductResponse>> HandleAsync(CreateProductRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
      var product = await _repository.CreateAsync(request.Adapt<Models.Product>());
      var response = product?.Adapt<CreateProductResponse>();
      return response is null ? StatusCode(500) : Created($"{Routes.ProductRoute}/{response.Id}", response);
    }
  }
}