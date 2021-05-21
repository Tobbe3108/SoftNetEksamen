using System;
using SoftNetEksamen.Core.Interfaces;

namespace SoftNetEksamen.Features.Category.Models
{
  public class Category : IModel
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }
}