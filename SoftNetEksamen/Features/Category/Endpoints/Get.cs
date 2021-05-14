using System;
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
  public class Get : BaseAsyncEndpoint.WithRequest<Guid>.WithResponse<GetCategoryResponse>
  {
    private readonly IRepository<Models.Category> _repository;

    public Get(IRepository<Models.Category> repository)
    {
      _repository = repository;
    }
    
    [HttpGet(Routes.CategoryRoute + "/{id:guid}")]
    [SwaggerOperation(
      Summary = "Gets a Category by id",
      Description = "Get a specific Category from db by its id",
      OperationId = "Category.Get",
      Tags = new[] { "CategoryEndpoints" })
    ]
    public override async Task<ActionResult<GetCategoryResponse>> HandleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
      var category = await _repository.ReadAsync(id);
      return category is null ? NotFound() : Ok(category.Adapt<GetCategoryResponse>());
    }
  }
}