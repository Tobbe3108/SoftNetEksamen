using System;
using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace SoftNetEksamen.Features.Category.Endpoints
{
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
  public class UpdateCategoryRequest
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    // ReSharper disable once UnusedType.Global
    public class UpdateSupplierRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
      public UpdateSupplierRequestValidator()
      {
        RuleFor(request => request.Id).NotEmpty();
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Description).NotEmpty();
      }
    }
  }
}