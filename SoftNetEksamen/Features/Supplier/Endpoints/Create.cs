using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using SoftNetEksamen.Core;
using SoftNetEksamen.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftNetEksamen.Features.Supplier.Endpoints
{
  public class Create : BaseAsyncEndpoint.WithRequest<CreateSupplierRequest>.WithResponse<CreateSupplierResponse>
  {
    private readonly IRepository<Models.Supplier> _repository;

    public Create(IRepository<Models.Supplier> repository)
    {
      _repository = repository;
    }
    
    [HttpPost(Routes.SupplierRoute)]
    [SwaggerOperation(
      Summary = "Creates a new Supplier",
      Description = "Creates a new Supplier",
      OperationId = "Supplier.Create",
      Tags = new[] { "SupplierEndpoints" })
    ]
    public override async Task<ActionResult<CreateSupplierResponse>> HandleAsync(CreateSupplierRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
      var supplier = await _repository.CreateAsync(request.Adapt<Models.Supplier>());
      var response = supplier?.Adapt<CreateSupplierResponse>();
      return response is null ? StatusCode(500) : Created($"{Routes.SupplierRoute}/{response.Id}", response);
    }
  }
}