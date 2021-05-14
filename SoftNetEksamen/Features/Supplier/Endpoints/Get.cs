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
  public class Get : BaseAsyncEndpoint.WithRequest<Guid>.WithResponse<GetSupplierResponse>
  {
    private readonly IRepository<Models.Supplier> _repository;

    public Get(IRepository<Models.Supplier> repository)
    {
      _repository = repository;
    }
    
    [HttpGet(Routes.SupplierRoute + "/{id:guid}")]
    [SwaggerOperation(
      Summary = "Gets a Supplier by id",
      Description = "Get a specific Supplier from db by its id",
      OperationId = "Supplier.Get",
      Tags = new[] { "SupplierEndpoints" })
    ]
    public override async Task<ActionResult<GetSupplierResponse>> HandleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
      var supplier = await _repository.ReadAsync(id);
      return supplier is null ? NotFound() : Ok(supplier.Adapt<GetSupplierResponse>());
    }
  }
}