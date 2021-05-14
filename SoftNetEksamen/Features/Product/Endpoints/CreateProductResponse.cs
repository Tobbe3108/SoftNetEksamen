using System;

namespace SoftNetEksamen.Features.Product.Endpoints
{
  // ReSharper disable once ClassNeverInstantiated.Global
  public class CreateProductResponse
  {
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Unit { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public int Stock { get; set; }
    public Guid SupplierId { get; set; }
  }
}