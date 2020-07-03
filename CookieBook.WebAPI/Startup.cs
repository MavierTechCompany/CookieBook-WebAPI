using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Domain.JWT;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.Category;
using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.Commands.Recipe;
using CookieBook.Infrastructure.Commands.Recipe.Rate;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Extensions.Security;
using CookieBook.Infrastructure.Extensions.Security.Interface;
using CookieBook.Infrastructure.Services;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.Infrastructure.Validators.Auth;
using CookieBook.Infrastructure.Validators.Category;
using CookieBook.Infrastructure.Validators.Recipe;
using CookieBook.Infrastructure.Validators.Recipe.Rate;
using CookieBook.Infrastructure.Validators.User;
using CookieBook.Infrastructure.Validators.UserImage;
using CookieBook.WebAPI.Framework;
using CookieBook.WebAPI.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace CookieBook.WebAPI
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
            services.AddAutoMapper(Assembly.Load("CookieBook.Infrastructure"));

            services.AddMvc(conf => conf.Filters.Add(typeof(ValidationFilter)))
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json
                        .ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                });

            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
            services.AddAuthorization(x => x.AddPolicy("user", p => p.RequireRole("user")));

            services.AddDbContextPool<CookieContext>(options => options
               .UseSqlServer(Configuration.GetConnectionString("CookieBookDatabase"),
                   c => c.MigrationsAssembly("CookieBook.WebAPI")).EnableSensitiveDataLogging(false), 64);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration.GetSection("JwtSettings:Issuer").Value,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(Configuration.GetSection("JwtSettings:Key").Value))
                    };
                });

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.AddScoped<IJwtHandler, JwtHandler>();

            services.AddTransient<IValidator<CreateUser>, CreateUserValidator>();
            services.AddTransient<IValidator<LoginAccount>, LoginUserValidator>();
            services.AddTransient<IValidator<UpdateUserData>, UpdateUserValidator>();
            services.AddTransient<IValidator<CreateImage>, CreateUserImageValidator>();
            services.AddTransient<IValidator<UpdateImage>, UpdateUserImageValidator>();
            services.AddTransient<IValidator<UpdatePassword>, UpdatePasswordValidator>();
            services.AddTransient<IValidator<CreateRecipe>, CreateRecipeValidator>();
            services.AddTransient<IValidator<CreateCategory>, CreateCategoryValidator>();
            services.AddTransient<IValidator<UpdateCategory>, UpdateCategoryValidator>();
            services.AddTransient<IValidator<CreateRate>, CreateRateValidator>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserImageService, UserImageService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRateService, RateService>();

            services.AddScoped<IDataHashManager, DataHashManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseErrorHandler();
            app.UseMvc();
        }
    }
}