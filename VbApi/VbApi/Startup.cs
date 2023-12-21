using FluentValidation;
using FluentValidation.AspNetCore;
using VbApi.Controllers;

namespace VbApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
    services.AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssemblyContaining<EmployeeValidator>();
            fv.RegisterValidatorsFromAssemblyContaining<StaffValidator>();
        });
    services.AddTransient<IValidator<Employee>, EmployeeValidator>();
    services.AddTransient<IValidator<Staff>, StaffValidator>();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(x => { x.MapControllers(); });
        }
    }
}
