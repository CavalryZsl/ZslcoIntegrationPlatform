using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using TechnicalValidation.Data;
using TechnicalValidation.IServices;
using TechnicalValidation.Model;
using TechnicalValidation.Services;

namespace TechnicalValidation
{
    /// <summary>
    /// 按照约定 asp.net core使用StartUp这个类 配置webserver
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration; //此服务在program类的bulider方法中已经注册了  这里可以使用了
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// 通过这个方法来注册服务，将我们需要的服务注册到依赖注入的容器中，方便我们后续在整个应用中使用  例如我们经常使用的操作数据库服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //注册自定义和内置的
            services.AddMvc();
            services.AddDbContext<DBContext>(options => {
                //options.UseSqlServer(_configuration["ConnectionStrings:Default"].ToString());
                options.UseSqlServer(_configuration.GetConnectionString("Default"));
            });
            services.AddSingleton<IPrintService, PrintService>(); //单例模式
                                                                  // services.AddTransient<IPrintService, PrintService>();//每次有代码请求 都会重新创建实例
                                                                  //services.AddScoped<IPrintService, PrintService>();//每次web请求期间 (多次请求该实例) 创建一个实例


            services.AddSingleton<IRepository<Student>, InMemoryRepository>();

            services.AddScoped<IRepository<Student>, EfCoreRepositoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 使用app来配置使用请求管道的中间件 如何响应http请求
        /// </summary>
        /// <param name="app">配置中间件</param>
        /// <param name="env">Host环境:应用程序名,根路径,环境变量...   ASPNETCORE_ENVIRONMENT 在launchSettings.json文件或者环境变量中</param>
        /// <param name="configuration">此参数是自己加的,是实现了IConfiguration接口的对象,因为asp.net core使用依赖注入,
        /// 整个系统的所有地方都可以使用依赖注入,当系统调用Configure方法的时候,系统会分析这个方法的参数,如果asp.net core能理解这些参数,
        /// 那么就会传进来实现了这些接口的对象/服务,即注入进来了</param>
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            IPrintService PrintService, //使用自定义的服务 在ConfigureServices方法中进行注册
            ILogger<Startup> logger) //使用内置日志的服务 
        {
            if (env.IsDevelopment())//ASPNETCORE_ENVIRONMENT 在launchSettings.json  在VS的启动项里面也可以设置
            {
                app.UseDeveloperExceptionPage(); // 针对开发人员 让开发人员看到错误的详细信息 必须放在前面 配置中间件的顺序很重要
            }
            else
            {
                app.UseExceptionHandler();//非开发环境
            }
            //真实项目通常不使用Run 通常使用Use
            app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/WELCOME" //进入欢迎页的路径
            }); //欢迎页


            app.Use(next=> { //next实际上是下一个中间件
                #region 此部分代码每次http请求 能够触发执行的 会多次执行  外部的Use方法只会执行一次 用来定义中间件的执行
                return async HttpContext => {
                    logger.LogInformation("something....");
                    if (HttpContext.Request.Path.StartsWithSegments("/ok"))
                        await HttpContext.Response.WriteAsync("well done"); //本次执行的代码
                    await next.Invoke(HttpContext); //下一个中间件执行的代码 如果不执行 管道短路 下个中间件将不执行
                };
                #endregion
            });

            #region 静态文件
            //app.UseDefaultFiles();//先改变文件的请求路径
            app.UseStaticFiles();//然后返回静态文件  wwwroot 目录

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules"))
            }); //使用其他目录处理静态文件

            //app.UseFileServer();//虽然包含上面两个中间件的功能,但是还包含其他功能:目录浏览等
            #endregion
            #region MVC 需要在上面的方法中注册服务
            //app.UseMvcWithDefaultRoute();//包含默认路由配置的MVC   
            app.UseMvc(bulider=> {
                //按约定配置路由
                bulider.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");//id可选的  可以加默认值

            });//不包含任何信息 需要配置
            #endregion
            app.Run(async (context) =>
            {
                //await context.Response.WriteAsync("Hello World!");
                //await context.Response.WriteAsync(_configuration["welcome"]); //从appsetting.json中获取
                await context.Response.WriteAsync(PrintService.GetMessge());
               
            });
        }
    }
}
