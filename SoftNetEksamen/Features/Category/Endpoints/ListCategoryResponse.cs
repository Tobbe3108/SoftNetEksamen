using System;

namespace SoftNetEksamen.Features.Category.Endpoints
{
  // ReSharper disable once ClassNeverInstantiated.Global
  public class ListCategoryResponse
  {
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }
}