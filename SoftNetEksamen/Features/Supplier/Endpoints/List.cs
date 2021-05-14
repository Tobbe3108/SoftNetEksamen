using System;
using System.Collections.Generic;
using System.Linq;
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
  public class List : BaseAsyncEndpoint.WithoutRequest.WithResponse<IEnumerable<ListSupplierResponse>>
  {
    private readonly IRepository<Models.Supplier> _repository;

    public List(IRepository<Models.Supplier> repository)
    {
      _repository = repository;
    }
    
    [HttpGet(Routes.SupplierRoute)]
    [SwaggerOperation(
      Summary = "Gets all Suppliers",
      Description = "Get all Suppliers from db",
      OperationId = "Supplier.List",
      Tags = new[] { "SupplierEndpoints" })
    ]
    public override async Task<ActionResult<IEnumerable<ListSupplierResponse>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      var suppliers = await _repository.ListAsync();
      return suppliers.Any() ? Ok(suppliers.Adapt<IEnumerable<ListSupplierResponse>>()) : Ok(Array.Empty<ListSupplierResponse>());
    }
  }
}