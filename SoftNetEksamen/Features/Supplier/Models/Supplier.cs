using System;
using SoftNetEksamen.Core.Interfaces;

namespace SoftNetEksamen.Features.Supplier.Models
{
  public class Supplier : IModel
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int PostCode { get; set; }
    public string Contact { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
  }
}