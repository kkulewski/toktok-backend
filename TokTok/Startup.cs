using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TokTok.Database;
using TokTok.Repositories;
using TokTok.Repositories.Sqlite;
using TokTok.Services.Authentication;

namespace TokTok
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
            // Configure dependency injection container here.
            services.AddTransient<IMessageRepository, SqliteMessageRepository>();
            services.AddTransient<IUserRepository, SqliteUserRepository>();
            services.AddTransient<IChannelRepository, SqliteChannelRepository>();
            services.AddTransient<IUserInChannelRepository, SqliteUserInChannelRepository>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            // Add DB context
            services.AddDbContext<SqliteDbContext>();
            services.AddEntityFrameworkSqlite();

            // Swagger is used to auto-generate interactive API description.
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "TokTok API", Version = "v1" }));

            // Add MVC features
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Use detailed exception pages in development mode.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable CORS
            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
            );

            // Enable Swagger UI.
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TokTok API V1"));

            // Configure default URL route.
            app.UseMvc(c => c.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}
