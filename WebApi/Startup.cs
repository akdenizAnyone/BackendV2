using Application;
using Application.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Extensions;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(_config);
            services.AddPersistenceInfrastructure(_config);
            services.AddSharedInfrastructure(_config);

            services.AddCors(options =>
            {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); ;
                });
            }); 
            
            services.AddSwaggerExtension();
            services.AddControllers();
            services.AddApiVersioningExtension();
            services.AddHealthChecks();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            //mines
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            //   app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseSwaggerUI(x=>{
                x.SwaggerEndpoint("/swagger/v1/swagger.json","webapi");
                x.RoutePrefix="";
                });
               

            app.UseErrorHandlingMiddleware();
            app.UseHealthChecks("/health");

           app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
