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
using System.Reflection;
using System.IO;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;

namespace CloudCollector.API
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
            // 注册Swagger服务
            services.AddSwaggerGen(c =>
            {
                // 添加文档信息
                c.SwaggerDoc("v1", new OpenApiInfo
                { 
                    Title = "CloudCollector.API", 
                    Version = "v1" ,
                    Description="冰河盲鱼",
                    Contact=new OpenApiContact
                    {
                        Name="LJJ",
                        Email="673905504@qq.com"
                    }
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录
                var xmlPath = Path.Combine(basePath, "CloudCollector.API.xml");
                c.IncludeXmlComments(xmlPath);
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
            // 启用Swagger中间件
            app.UseSwagger();
            // 配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.ShowExtensions();
                c.DocExpansion(DocExpansion.None);
                c.RoutePrefix = string.Empty; //"api";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CloudCollector.API Api v1");
                //c.SwaggerEndpoint("/v1/swagger.json", "CloudCollector.API");
                //c.RoutePrefix = string.Empty;
            });
        }
    }
}
