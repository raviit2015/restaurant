using System;
using ECommerce.ApplicationFacade;
using ECommerce.Models;
using ECommerce.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce
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
            services.AddControllersWithViews();
            services.AddCors(c =>
            {
                c.AddPolicy("http://localhost:4200", options => options.AllowAnyOrigin());
            });
            services.AddMvc().AddNewtonsoftJson();
            AddTransientServices(services);
            SetupJWTServices(services);
        }

        private void AddTransientServices(IServiceCollection services)
        {
            services.AddTransient<ILoginFacade, LoginFacade>();
            services.AddTransient<ILoginRepository, LoginRepository>();

            services.AddTransient<IProductFacade, ProductFacade>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }

        private void SetupJWTServices(IServiceCollection services)
        {
            string key = "my_secret_key_12345"; //this should be same which is used while creating token      
            var issuer = "http://mysite.com";  //this should be same which is used while creating token  

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = issuer,
                  ValidAudience = issuer,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
              };

              options.Events = new JwtBearerEvents
              {
                  OnAuthenticationFailed = context =>
                  {
                      if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                      {
                          context.Response.Headers.Add("Token-Expired", "true");
                      }
                      return Task.CompletedTask;
                  }
              };
          });
        }
        private void JWTServices(IServiceCollection services)
        {
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(builder =>
            {
                //builder.WithOrigins("https://testplan.toolsgroup.com/");
                builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Test}/{id?}");
            });
        }
    }
}
