using System;
using DG.Polly.Business.Documents.Queries.Get;
using DG.Polly.Business.Documents.Queries.GetMetadata;
using DG.Polly.Business.Documents.Queries.GetStatus;
using DG.Polly.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace DG.Polly.Api
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
            services.AddScoped<IGetDocumentQuery, GetDocumentQuery>();
            services.AddScoped<IGetDocumentMetadateQuery, GetDocumentMetadateQuery>();
            services.AddScoped<IGetDocumentStatusQuery, GetDocumentStatusQuery>();

            services.AddRefitClient<IDocumentsService>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://ow-interview-exercise-dev.azurewebsites.net/documents"));

            services.AddHttpClient();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
