
using Ecommerce.BLL;
using Ecommerce.Common.Auth;
using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Persistence;
using Ecommerce.DAL.UOW;
using Ecommerce.Helper;
using Ecommerce.IBLL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce1
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
           services.AddDbContext<IdentityAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"),
                    x => x.MigrationsAssembly("Ecommerce.DAL")));
           services.AddDbContext<EcommerceContext>(x => x.UseSqlServer(Configuration.GetConnectionString("SqlConnection"),
                x => x.MigrationsAssembly("Ecommerce.DAL")));

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                            .AddEntityFrameworkStores<IdentityAppContext>();
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce1", Version = "v1" });
            });
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<UnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.ConfigureAuthentication(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

       


    }
}
