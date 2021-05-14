using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace SoftNetEksamen.Features.Category.Endpoints
{
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
  public class CreateCategoryRequest
  {
    public string Name { get; set; }
    public string Description { get; set; }
    
    // ReSharper disable once UnusedType.Global
    public class CreateSupplierRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
      public CreateSupplierRequestValidator()
      {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Description).NotEmpty();
      }
    }
  }
}