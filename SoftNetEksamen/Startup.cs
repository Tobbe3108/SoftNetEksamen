using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SoftNetEksamen.Core.Interfaces;
using SoftNetEksamen.Core.Middleware;
using SoftNetEksamen.Features.Category.Data;
using SoftNetEksamen.Features.Category.Models;
using SoftNetEksamen.Features.Product.Data;
using SoftNetEksamen.Features.Product.Models;
using SoftNetEksamen.Features.Supplier.Data;
using SoftNetEksamen.Features.Supplier.Models;

namespace SoftNetEksamen
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton(typeof(IRepository<Supplier>), typeof(SupplierRepository));
      services.AddSingleton(typeof(IRepository<Product>), typeof(ProductRepository));
      services.AddSingleton(typeof(IRepository<Category>), typeof(CategoryRepository));
      
      services.AddControllers()
        .AddFluentValidation(s =>
        {
          s.RegisterValidatorsFromAssemblyContaining<Startup>();
          s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
        });
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo {Title = "SoftNetEksamen", Version = "v1"});
        c.EnableAnnotations();
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoftNetEksamen v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseMiddleware<ErrorHandlerMiddleware>();
      
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}