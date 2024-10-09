using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Xiezn.Core.Common.Extensions;
using Xiezn.Core.Common.Filters;
using Xiezn.Core.Common.Helpers;
using Xiezn.Core.Models.ConfModel;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Xiezn.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ConfigHelper._configuration = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                #region
                opt.UseCentralRoutePrefix(new RouteAttribute(Configuration["SchemaName"]));
                #endregion

            }).AddJsonOptions(options =>
            {
                #region
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                #endregion
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MemoryBufferThreshold = int.MaxValue;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 209715200 * 5;
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            // #region Swagger
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new Info
            //     {
            //         Version = "v1.0.0",
            //         Title = "Xiezn.Core WebAPI",
            //         Description = "框架集合",
            //         TermsOfService = "None",
            //         Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Xiezn", Email = "2521136840@qq.com", Url = "#" }
            //     });
            //     c.OperationFilter<SwaggerFileUploadFilter>();
            //     //添加读取注释服务
            //     var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            //     var xmlPath = Path.Combine(basePath, "APIHelp.xml");
            //     c.IncludeXmlComments(xmlPath, true);

            //     //添加header验证信息
            //     //c.OperationFilter<SwaggerHeader>();
            //     var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
            //     c.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
            //     c.AddSecurityDefinition("Bearer", new ApiKeyScheme
            //     {
            //         //Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
            //         //Name = "Authorization",//jwt默认的参数名称
            //         Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Token: Bearer {token}\"",
            //         Name = "Token",//jwt默认的参数名称
            //         In = "header",//jwt默认存放Authorization信息的位置(请求头中)
            //         Type = "apiKey"
            //     });
            // });
            // #endregion

            #region 认证
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                JwtAuthConfig jwtAuthConfig = ConfigHelper.GetConfig<JwtAuthConfig>("JwtAuth");
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "Xiezn.Core",
                    ValidAudience = "MyAudience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAuthConfig.SecurityKey)),

                    /*TokenValidationParameters的参数默认值*/
                    RequireSignedTokens = true,
                    // SaveSigninToken = false,
                    // ValidateActor = false,
                    // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    // 是否要求Token的Claims中必须包含 Expires
                    RequireExpirationTime = true,
                    // 允许的服务器时间偏移量
                    // ClockSkew = TimeSpan.FromSeconds(300),
                    // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true
                };
                o.Events = new JwtBearerEvents
                {
                    //此处为权限验证失败后触发的事件
                    OnChallenge = context =>
                    {
                        //此处代码为终止.Net Core默认的返回类型和数据结果，这个很重要哦，必须
                        context.HandleResponse();

                        //自定义自己想要返回的数据结果，我这里要返回的是Json对象，通过引用Newtonsoft.Json库进行转换
                        //var payload = JsonConvert.SerializeObject(new { Code = "401", Msg = "很抱歉，您无权访问该接口！" });
                        var payload = JsonConvert.SerializeObject(new { code = "401", msg = "很抱歉，您无权访问该接口！" });
                        //自定义返回的数据类型
                        context.Response.ContentType = "application/json";
                        //自定义返回状态码，默认为401 我这里改成 200
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //输出Json数据结果
                        context.Response.WriteAsync(payload);
                        return Task.FromResult(0);
                    }
                };
            });
            #endregion

            #region 授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireClient", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("RequireAdminOrClient", policy => policy.RequireRole("Admin,Client").Build());
            });
            #endregion

            #region CORS
            // 先注入服务，声明策略，然后再下边app中配置开启中间件
            services.AddCors(c =>
            {
                // ↓↓↓↓↓↓↓注意正式环境不要使用这种全开放的处理↓↓↓↓↓↓↓↓↓↓
                c.AddPolicy("AllRequests", policy =>
                {
                    policy
                    .AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie
                });
                // ↑↑↑↑↑↑↑注意正式环境不要使用这种全开放的处理↑↑↑↑↑↑↑↑↑↑

                // 一般采用这种方法
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins("http://127.0.0.1:1818", "http://localhost:8080", "http://localhost:8021", "http://localhost:8081", "http://localhost:1818") // 支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    .AllowAnyHeader() // Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });
            #endregion

            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    Server = "smtp.qq.com",
                    Port = 465,
                    SenderName = "",
                    SenderEmail = "yclw9@qq.com",
                    Account = "yclw9@qq.com",
                    Password = "mhbrkuayvkkgbijd",
                    Security = true
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMiddleware<JwtAuthorizationFilter>();
            app.UseStaticFiles();

            app.UseFileServer();

            app.UseCors("AllRequests");

            app.UseMvc();

            // #region Swagger
            // app.UseSwagger();
            // app.UseSwaggerUI(c =>
            // {
            //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            // });
            // #endregion
        }
    }
}
