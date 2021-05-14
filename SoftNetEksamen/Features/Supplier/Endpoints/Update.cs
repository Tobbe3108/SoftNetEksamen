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
  public class Update : BaseAsyncEndpoint.WithRequest<UpdateSupplierRequest>.WithResponse<UpdateSupplierResponse>
  {
    private readonly IRepository<Models.Supplier> _repository;

    public Update(IRepository<Models.Supplier> repository)
    {
      _repository = repository;
    }
    
    [HttpPut(Routes.SupplierRoute)]
    [SwaggerOperation(
      Summary = "Updates a Supplier by its id",
      Description = "Updates a Supplier on the db by its id",
      OperationId = "Supplier.Put",
      Tags = new[] { "SupplierEndpoints" })
    ]
    public override async Task<ActionResult<UpdateSupplierResponse>> HandleAsync(UpdateSupplierRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
      var result = await _repository.UpdateAsync(request.Adapt<Models.Supplier>());
      return result ? NoContent() : StatusCode(500); 
    }
  }
}