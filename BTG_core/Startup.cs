using BTG_core.Models.Commons;
using BTG_core.Repositorie;
using BTG_core.Repositorie.Core.Interfaces;
using BTG_core.Repositorie.Core;
using BTG_core.Services;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using BTG_core.Services.Core.Interfaces;
using BTG_core.Services.Core;
using BTG_core.Middleware;

namespace BTG_core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<DBSettings>(Configuration.GetSection(nameof(DBSettings)));

            services.AddRepositoryExtension(Configuration);
            services.AddBusinnesServiceExtension(Configuration);
            services.AddSingleton<IDBSettings>(sp =>
                sp.GetRequiredService<IOptions<DBSettings>>().Value
            );
            services.AddCors(o => o.AddPolicy("PolicyTotalCustom", builder =>
            {
                builder.AllowAnyOrigin()
                .WithExposedHeaders("X-Total")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "I Manage Core Api", Version = "V1" });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "I Manage Core Api");
            });

            app.UseExceptionHandler("/Error");
            app.UseHsts();
            app.UseStaticFiles();
            app.UseMiddleware(typeof(GlobalExceptionMiddleware));
            app.UseCors("PolicyTotalCustom");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
