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
  public class Create : BaseAsyncEndpoint.WithRequest<CreateCategoryRequest>.WithResponse<CreateCategoryResponse>
  {
    private readonly IRepository<Models.Category> _repository;

    public Create(IRepository<Models.Category> repository)
    {
      _repository = repository;
    }
    
    [HttpPost(Routes.CategoryRoute)]
    [SwaggerOperation(
      Summary = "Creates a new Category",
      Description = "Creates a new Category",
      OperationId = "Category.Create",
      Tags = new[] { "CategoryEndpoints" })
    ]
    public override async Task<ActionResult<CreateCategoryResponse>> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
      var supplier = await _repository.CreateAsync(request.Adapt<Models.Category>());
      var response = supplier?.Adapt<CreateCategoryResponse>();
      return response is null ? StatusCode(500) : Created($"{Routes.CategoryRoute}/{response.Id}", response);
    }
  }
}