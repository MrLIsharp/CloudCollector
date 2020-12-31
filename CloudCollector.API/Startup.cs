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
            //������.net core 3.0 �Ժ󣬲���Ĭ�ϰ��� NewtonsoftJson������Ĭ��ʹ��System.Text.Json. 
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    //����ʱ�����л���ʽ
                    options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter("yyyy-MM-dd"));
                });
            // ע��Swagger����
            services.AddSwaggerGen(c =>
            {
                // ����ĵ���Ϣ
                c.SwaggerDoc("v1", new OpenApiInfo
                { 
                    Title = "CloudCollector.API", 
                    Version = "v1" ,
                    Description="����ä��",
                    Contact=new OpenApiContact
                    {
                        Name="LJJ",
                        Email="673905504@qq.com"
                    }
                });
                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼
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
            // ����Swagger�м��
            app.UseSwagger();
            // ����SwaggerUI
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
