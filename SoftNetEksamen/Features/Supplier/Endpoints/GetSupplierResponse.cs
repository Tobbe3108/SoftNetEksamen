using System;

namespace SoftNetEksamen.Features.Supplier.Endpoints
{
  // ReSharper disable once ClassNeverInstantiated.Global
  public class GetSupplierResponse
  {
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int PostCode { get; set; }
    public string Contact { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
  }
}