using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(option => option.EnableEndpointRouting = false)
                .AddAuthorization();

            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", option =>
            {
                option.Authority = "https://localhost:44348/";
                //option.RequireHttpsMetadata = false;
                option.Audience = "consoleapi";
            });
            services.AddControllersWithViews();
            //services.AddAuthorization();
            //services.AddIdentityServer()
            //        .AddDeveloperSigningCredential()
            //        .AddInMemoryApiResources(BasicConfig.GetApiResources())
            //        .AddInMemoryClients(BasicConfig.GetClients());
            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseIdentityServer();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
