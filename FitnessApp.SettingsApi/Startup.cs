using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using FitnessApp.SettingsApi.Services.Settings;
using Swashbuckle.AspNetCore.Filters;
using FitnessApp.SettingsApi.Data;
using FitnessApp.SettingsApi.Models.Output;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Data.Entities;
using FitnessApp.Common.Abstractions.Services.Configuration;
using FitnessApp.Common.Abstractions.Services.Cache;
using FitnessApp.Common.Abstractions.Db.Configuration;
using FitnessApp.Common.Serializer.JsonSerializer;
using AutoMapper;
using FitnessApp.SettingsApi.DependencyInjection;
using FitnessApp.ServiceBus.AzzureServiceBus.Configuration;

namespace FitnessApp.SettingsApi
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
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IJsonSerializer, JsonSerializer>();

            services.Configure<CosmosDbSettings>(Configuration.GetSection("CosmosConnection"));

            services.Configure<AzzureServiceBusSettings>(Configuration.GetSection("AzzureServiceBusSettings"));

            services.Configure<CacheSettings>(Configuration.GetSection("Cache"));

            services.AddTransient<ICacheService<SettingsModel>, CacheService<SettingsModel>>();
            
            services.AddTransient<ISettingsRepository<Settings, SettingsModel, CreateSettingsModel, UpdateSettingsModel>, SettingsRepository<Settings, SettingsModel, CreateSettingsModel, UpdateSettingsModel>>();

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration["Redis:Configuration"];
            });

            services.AddTransient<ISettingsService<Settings, SettingsModel, GetUsersSettingsModel, CreateSettingsModel, UpdateSettingsModel>, SettingsService<Settings, SettingsModel, GetUsersSettingsModel, CreateSettingsModel, UpdateSettingsModel>>();

            services.AddSettingsMessageTopicSubscribersService();

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.Authority = Configuration["JWT:Issuer"];
                cfg.Audience = Configuration["JWT:Audience"];
            });

            services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FitnessApp.SettingsApi",
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddFile("Logs/FitnessApp.SettingsApi-{Date}.txt");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc();            

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger XML Api Demo v1");
            });
        }
    }
}