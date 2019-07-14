﻿using AutoMapper;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Profiles;
using EP.Balda.Logic.Services;
using EP.Balda.Web.Filters;
using EP.Balda.Web.Validators;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;

namespace EP.Balda.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddGoogle("Google", opt =>
                 {
                     var googleAuthNSection =
                         Configuration.GetSection("Authentication:Google");

                     opt.ClientId = googleAuthNSection["ClientId"];
                     opt.ClientSecret = googleAuthNSection["ClientSecret"];
                     opt.CallbackPath = new PathString("/signin-google");
                 })
                .AddFacebook("Facebook", opt =>
                {
                    var facebookAuthNSection =
                        Configuration.GetSection("Authentication:Facebook");

                    opt.ClientId = facebookAuthNSection["ClientId"];
                    opt.ClientSecret = facebookAuthNSection["ClientSecret"];
                    opt.CallbackPath = new PathString("/signin-google");
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("google",
                      cfg => cfg.AddAuthenticationSchemes("Google")
                          .RequireAuthenticatedUser());
                opt.AddPolicy("facebook",
                    cfg => cfg.AddAuthenticationSchemes("Facebook")
                    .RequireAuthenticatedUser());
            });

            services.AddSession();

            services.AddSwaggerDocument(cfg =>
            {
                cfg.SchemaType = SchemaType.OpenApi3;
            });

            services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddMediatR(typeof(CreateNewGameCommand).Assembly);
            
            services.AddBaldaGameServices();

            services.AddCors();
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ModelValidationFilter));
                opt.Filters.Add(typeof(GlobalExceptionFilter));
            })

                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<UserLoginValidator>();
                cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            }); ;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                              IMediator mediator)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseCors(opt =>
                opt.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200")
                .AllowCredentials());

            app.UseAuthentication();

            mediator.Send(new CreateDatabaseCommand()).Wait();

            app.UseOpenApi().UseSwaggerUi3();

            app.UseMvc();

        }
    }
}