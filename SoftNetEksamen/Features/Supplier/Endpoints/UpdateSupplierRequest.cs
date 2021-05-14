using System;
using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using PhoneNumbers;

namespace SoftNetEksamen.Features.Supplier.Endpoints
{
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
  public class UpdateSupplierRequest
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int PostCode { get; set; }
    public string Contact { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    // ReSharper disable once UnusedType.Global
    public class UpdateSupplierRequestValidator : AbstractValidator<UpdateSupplierRequest>
    {
      public UpdateSupplierRequestValidator()
      {
        RuleFor(request => request.Id).NotEmpty();
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Address).NotEmpty();
        RuleFor(request => request.PostCode).InclusiveBetween(1050, 9990);
        RuleFor(request => request.Contact).NotEmpty();
        RuleFor(request => request.Email).EmailAddress();
        RuleFor(request => request.PhoneNumber)
          .Custom((s,context) => 
          {
            try
            {
              if (!PhoneNumberUtil.GetInstance().IsValidNumber(PhoneNumberUtil.GetInstance().Parse(s, "DK")))
              {
                context.AddFailure($"'PhoneNumber' is not a valid number");
              }
            }
            catch
            {
              context.AddFailure($"'PhoneNumber' is not a valid number");
            }
          });
      }
    }
  }
}