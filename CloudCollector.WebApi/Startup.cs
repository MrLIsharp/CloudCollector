using CloudCollector.Interface;
using CloudCollector.Models;
using CloudCollector.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CloudCollector.Common;

namespace CloudCollector.WebApi
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
            services.AddControllers();
            services.AddTransient<IBase, BaseServer>();
            services.AddTransient<ICateGoryService, CloudCollector.Service.Category>();
            services.AddTransient<IArchiver, ArchiverServer>();
            //升级到.net core 3.0 以后，不在默认包含 NewtonsoftJson，而是默认使用System.Text.Json. 
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    //配置时间序列化格式
                    options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter("yyyy-MM-dd"));
                });
            services.AddDbContext<CloudCollectorContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection")
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
