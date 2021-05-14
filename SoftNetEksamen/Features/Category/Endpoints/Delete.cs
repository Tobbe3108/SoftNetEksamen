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
  public class Delete : BaseAsyncEndpoint.WithRequest<Guid>.WithResponse<DeleteCategoryResponse>
  {
    private readonly IRepository<Models.Category> _repository;

    public Delete(IRepository<Category.Models.Category> repository)
    {
      _repository = repository;
    }
    
    [HttpDelete(Routes.CategoryRoute + "/{id:guid}")]
    [SwaggerOperation(
      Summary = "Deletes a Category by id",
      Description = "Deletes a specific Category from db by its id",
      OperationId = "Category.Delete",
      Tags = new[] { "CategoryEndpoints" })
    ]
    public override async Task<ActionResult<DeleteCategoryResponse>> HandleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
      var category = await _repository.DeleteAsync(id);
      return category is null ? StatusCode(500) : Ok(category.Adapt<DeleteCategoryResponse>());
    }
  }
}