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
  public class Update : BaseAsyncEndpoint.WithRequest<UpdateCategoryRequest>.WithResponse<UpdateCategoryResponse>
  {
    private readonly IRepository<Category.Models.Category> _repository;

    public Update(IRepository<Category.Models.Category> repository)
    {
      _repository = repository;
    }
    
    [HttpPut(Routes.CategoryRoute)]
    [SwaggerOperation(
      Summary = "Updates a Category by its id",
      Description = "Updates a Category on the db by its id",
      OperationId = "Category.Put",
      Tags = new[] { "CategoryEndpoints" })
    ]
    public override async Task<ActionResult<UpdateCategoryResponse>> HandleAsync(UpdateCategoryRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
      var result = await _repository.UpdateAsync(request.Adapt<Category.Models.Category>());
      return result ? NoContent() : StatusCode(500); 
    }
  }
}