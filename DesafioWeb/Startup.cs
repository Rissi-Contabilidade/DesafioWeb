using DesafioWeb.Controllers;
using DesafioWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DesafioWeb
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",true, true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RissiAPIDatabaseSettings>(
                Configuration.GetSection(nameof(RissiAPIDatabaseSettings)));

            services.AddSingleton<IRissiAPIDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<RissiAPIDatabaseSettings>>().Value);

            services.AddSingleton<UserService>();
            services.AddSingleton<CepService>();
            services.AddSingleton<PeopleService>();

            services.AddControllers().AddNewtonsoftJson((o) => o.UseMemberCasing());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}