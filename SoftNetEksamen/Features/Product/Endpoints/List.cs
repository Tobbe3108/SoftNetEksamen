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

namespace SoftNetEksamen.Features.Product.Endpoints
{
  public class List : BaseAsyncEndpoint.WithoutRequest.WithResponse<IEnumerable<ListProductResponse>>
  {
    private readonly IRepository<Product.Models.Product> _repository;

    public List(IRepository<Product.Models.Product> repository)
    {
      _repository = repository;
    }
    
    [HttpGet(Routes.ProductRoute)]
    [SwaggerOperation(
      Summary = "Gets all Products",
      Description = "Get all Products from db",
      OperationId = "Product.List",
      Tags = new[] { "ProductEndpoints" })
    ]
    public override async Task<ActionResult<IEnumerable<ListProductResponse>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      var products = await _repository.ListAsync();
      return products.Any() ? Ok(products.Adapt<IEnumerable<ListProductResponse>>()) : Ok(Array.Empty<ListProductResponse>());
    }
  }
}