using System;
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
  public class Delete : BaseAsyncEndpoint.WithRequest<Guid>.WithResponse<DeleteSupplierResponse>
  {
    private readonly IRepository<Models.Supplier> _repository;

    public Delete(IRepository<Models.Supplier> repository)
    {
      _repository = repository;
    }
    
    [HttpDelete(Routes.SupplierRoute + "/{id:guid}")]
    [SwaggerOperation(
      Summary = "Deletes a Supplier by id",
      Description = "Deletes a specific Supplier from db by its id",
      OperationId = "Supplier.Delete",
      Tags = new[] { "SupplierEndpoints" })
    ]
    public override async Task<ActionResult<DeleteSupplierResponse>> HandleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
      var supplier = await _repository.DeleteAsync(id);
      return supplier is null ? StatusCode(500) : Ok(supplier.Adapt<DeleteSupplierResponse>());
    }
  }
}