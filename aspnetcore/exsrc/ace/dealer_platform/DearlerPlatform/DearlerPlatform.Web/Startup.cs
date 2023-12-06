using DearlerPlatform.Common.TokenModule;
using DearlerPlatform.Core.Core;
using DearlerPlatform.Extensions;
using DearlerPlatform.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace DearlerPlatform.Web
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
            //��������������
            services.AddCors(options =>
            {
                options.AddPolicy("any", cp =>
                {
                    cp.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });


            var token = Configuration.GetSection("Jwt").Get<JwtTokenModel>();

            #region Jwt��֤

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    //�Ƿ���https��Ĭ��Ϊtrue
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Security)),
                        ValidIssuer = token.Issuer,
                        ValidAudience = token.Audience,
                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            //�˴���ֹ����
                            context.HandleResponse();
                            var res = "{\"code\":401,\"err\":\"��Ȩ��\"}";
                            context.Response.ContentType = MediaTypeNames.Application.Json;
                            //���״̬�뷵�ص���4��ͷ�ģ����һ�����Ӧ��Ϣ��ContentType������JSON�ſ��ԣ����򷵻�״̬��200��ͷ��
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return context.Response.WriteAsync(res);
                        }
                    };
                });

            #endregion Jwt��֤

            var con = Configuration.GetConnectionString("Default");
            services.AddDbContext<DearlerPlarformDbContext>(opt =>
            {
                opt.UseSqlServer(con);
            });
            //ע��AutoMapper
            services.AddAutoMapper(typeof(DearlerPlatformProfile));

            //services.AddTransient<ICustomerService, CustomerService>();
            //var rep = typeof(IRepository<>);

            services.RepositoryRegister();
            services.ServicesRegister();

            services.AddControllers();
            //services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DearlerPlatform.Web", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "��ʽ��Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header, //��������ͷ��
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                //��Ӱ�ȫҪ��,OpenApiSecurityRequirement�̳���Dictionary
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }
                        ,new string[]{ }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DearlerPlatform.Web v1"));
            }
            app.UseCors("any");
            app.UseAuthentication();
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}