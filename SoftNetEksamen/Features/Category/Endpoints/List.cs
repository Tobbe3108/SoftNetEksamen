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

namespace SoftNetEksamen.Features.Category.Endpoints
{
  public class List : BaseAsyncEndpoint.WithoutRequest.WithResponse<IEnumerable<ListCategoryResponse>>
  {
    private readonly IRepository<Models.Category> _repository;

    public List(IRepository<Models.Category> repository)
    {
      _repository = repository;
    }
    
    [HttpGet(Routes.CategoryRoute)]
    [SwaggerOperation(
      Summary = "Gets all Categories",
      Description = "Get all Categories from db",
      OperationId = "Category.List",
      Tags = new[] { "CategoryEndpoints" })
    ]
    public override async Task<ActionResult<IEnumerable<ListCategoryResponse>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      var categories = await _repository.ListAsync();
      return categories.Any() ? Ok(categories.Adapt<IEnumerable<ListCategoryResponse>>()) : Ok(Array.Empty<ListCategoryResponse>());
    }
  }
}