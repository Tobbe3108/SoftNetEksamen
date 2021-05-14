using System;
using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace SoftNetEksamen.Features.Product.Endpoints
{
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
  public class CreateProductRequest
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string Unit { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public int Stock { get; set; }
    public Guid SupplierId { get; set; }
    
    // ReSharper disable once UnusedType.Global
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
      public CreateProductRequestValidator()
      {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Description).NotEmpty();
        RuleFor(request => request.Unit).NotEmpty();
        RuleFor(request => request.Amount).GreaterThanOrEqualTo(0);
        RuleFor(request => request.Price).GreaterThanOrEqualTo(0);
        RuleFor(request => request.CategoryId).NotEmpty();
        RuleFor(request => request.Stock).GreaterThanOrEqualTo(0);
        RuleFor(request => request.SupplierId).NotEmpty();
      }
    }
  }
}