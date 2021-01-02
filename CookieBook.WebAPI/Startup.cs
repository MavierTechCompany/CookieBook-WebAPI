using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using AutoMapper;
using CookieBook.Domain.JWT;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Commands.Admin;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.Category;
using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.Commands.Recipe;
using CookieBook.Infrastructure.Commands.Recipe.Rate;
using CookieBook.Infrastructure.Commands.Statistics;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Extensions.Security;
using CookieBook.Infrastructure.Extensions.Security.Interface;
using CookieBook.Infrastructure.Services;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.Infrastructure.Validators.Admin;
using CookieBook.Infrastructure.Validators.Auth;
using CookieBook.Infrastructure.Validators.Category;
using CookieBook.Infrastructure.Validators.Recipe;
using CookieBook.Infrastructure.Validators.Recipe.Rate;
using CookieBook.Infrastructure.Validators.Statistics;
using CookieBook.Infrastructure.Validators.User;
using CookieBook.Infrastructure.Validators.UserImage;
using CookieBook.WebAPI.Framework;
using CookieBook.WebAPI.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore;

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

            services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "CookieBook WebAPI",
                    Contact = new OpenApiContact
                    {
                        Name = "Mavier Tech Company",
                        Url = new Uri("https://github.com/MavierTechCompany")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Non-Profit Open Software License",
                        Url = new Uri("https://github.com/MavierTechCompany/CookieBook-WebAPI/blob/master/LICENSE")
                    }
                });

                conf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                conf.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                conf.IncludeXmlComments(xmlPath);

                // Get referenced assemblies xml documentations
                // TODO: This needs to be a separate method. Maybe with code for including main assembly XML
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new AssemblyName[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();

                Array.ForEach(xmlDocs, (d) =>
                {
                    conf.IncludeXmlComments(d);
                });
            });

            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
            services.AddAuthorization(x => x.AddPolicy("user", p => p.RequireRole("user")));

            services.AddDbContextPool<CookieContext>(options => options
               .UseSqlServer(GetConnectionString(),
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
            services.AddTransient<IValidator<LoginAccount>, LoginAccountValidator>();
            services.AddTransient<IValidator<UpdateUserData>, UpdateUserValidator>();
            services.AddTransient<IValidator<CreateImage>, CreateUserImageValidator>();
            services.AddTransient<IValidator<UpdateImage>, UpdateUserImageValidator>();
            services.AddTransient<IValidator<UpdatePassword>, UpdatePasswordValidator>();
            services.AddTransient<IValidator<CreateRecipe>, CreateRecipeValidator>();
            services.AddTransient<IValidator<CreateCategory>, CreateCategoryValidator>();
            services.AddTransient<IValidator<UpdateCategory>, UpdateCategoryValidator>();
            services.AddTransient<IValidator<CreateRate>, CreateRateValidator>();
            services.AddTransient<IValidator<CreateAdmin>, CreateAdminValidator>();
            services.AddTransient<IValidator<BlockUser>, BlockUserValidator>();
            services.AddTransient<IValidator<TimePeriod>, TimePeriodValidator>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserImageService, UserImageService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserStatisticsService, UserStatisticsService>();

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
            app.UseSwagger();
            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/v1.0/swagger.json", "CookieBook API v1.0");
                conf.RoutePrefix = "docs";
            });
        }

        /// <summary>
        /// Gets connection string for specyfic runtime/server operation system.
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Configuration.GetConnectionString("CookieBookDatabaseLinux");
            }

            return Configuration.GetConnectionString("CookieBookDatabaseWindows");
        }
    }
}