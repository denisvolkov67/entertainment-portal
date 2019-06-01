﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediatR;
using EP.WordsMaker.Logic.Queries;
using AutoMapper;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Extensions;
using EP.WordsMaker.Logic.Profiles;
using NJsonSchema;

namespace EP.WordsMaker.Web
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
            services.AddSwaggerDocument(cfg => cfg.SchemaType = SchemaType.OpenApi3);
            services.AddMediatR(typeof(GetAllPlayers).Assembly);
            services.AddAutoMapper(cfg => cfg.AddProfile(new PlayerProfile()));
            services.AddPlayerServices();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMediator mediator)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseSwagger().UseSwaggerUi3();
            app.UseMvc();
		}
	}
}
