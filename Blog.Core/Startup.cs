using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Blog.IServices;
using Blog.Repository;
using Blog.Services.Blog.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Repository.sugar;

namespace Blog.Core
{
    public class Startup
    {
        public string ApiName { get; set; } = "Blog.Core";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //ConfigureServices������������services(���ַ���, ����identity, ef, mvc�ȵȰ�����������, �����Լ�д��)����(register)��container(asp.net core������)��ȥ, ��������Щservices. ���container����������dependency injection��(����ע��). ����ע���services(���⻹����һЩ����Ѿ�ע��õ�services) ���Ժ�д�����ʱ��, �����Խ�����ע��(inject)��ȥ. 
        public void ConfigureServices(IServiceCollection services)
        {

            BaseDBConfig.ConnectionString = Configuration.GetSection("AppSettings:SqlServerConnection").Value;

            var basePath = PlatformServices.Default.Application.ApplicationBasePath;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    // {ApiName} �����ȫ�ֱ����������޸�
                    Version = "V1",
                    Title = $"{ApiName} �ӿ��ĵ�����Netcore 3.1",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                c.OrderActionsBy(o => o.RelativePath);


                //�����������������������
                var xmlPath = Path.Combine(basePath, "Blog.Core.xml");//������Ǹո����õ�xml�ļ���
                c.IncludeXmlComments(xmlPath, true);//Ĭ�ϵĵڶ���������false�������controller��ע�ͣ��ǵ��޸�


                var xmlModelPath = Path.Combine(basePath, "Blog.Model.xml");
                c.IncludeXmlComments(xmlModelPath);

            });

            services.AddControllers();
        }



        public void ConfigureContainer(ContainerBuilder builder)
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;

            //ֱ��ע��ĳһ���ӿں���
            //�����ʵ���࣬�ұߵ�As�ǽӿ�
            builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();

            //ע��Ҫͨ�����䴴�������

            var servicesDllFile = Path.Combine(basePath, "Blog.Services.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);

            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableClassInterceptors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Configure���������ڷ���֮ǰ������������
        //Configure ������asp.net core������������ָ����δ���ÿ��http�����
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //���ݰ汾���Ƶ��� ����չʾ
                var ApiName = "Blog.Core";//��������Դ�appsettings.json�л�ȡ�������ҷ�װ��һ����Appsettings.cs������鿴�ҵ�Դ����
                var version = "V1";//���Swagger��SwaggerDoc�е�NAME����һ��
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");

                // ��swagger��ҳ�����ó������Զ����ҳ�棬�ǵ�����ַ�����д�������������.index.html
                //c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Blog.Core.index.html");//���������MiniProfiler�������ܼ�صģ������£���������AOP�Ľӿ����ܷ�����������㲻��Ҫ��������ʱ��ע�͵�����Ӱ���֡�
                //c.RoutePrefix = ""; //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //��һ����·�м������ʾ http ����������Ͳ���������. 
                endpoints.MapControllers();
            });
        }
    }
}
