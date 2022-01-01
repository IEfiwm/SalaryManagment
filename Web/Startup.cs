using Application.Extensions;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using AutoMapper;
using FluentValidation.AspNetCore;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using Web.Abstractions;
using Web.Extensions;
using Web.Permission;
using Web.Services;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddNotyf(o =>
            {
                o.DurationInSeconds = 10;
                o.IsDismissable = true;
                o.HasRippleEffect = true;
                o.Position = NotyfPosition.BottomLeft;
            });

            services.AddApplicationLayer();

            services.AddInfrastructure(_configuration);

            services.AddDependencies();

            services.AddPersistenceContexts(_configuration);

            services.AddRepositories();

            services.AddSharedInfrastructure(_configuration);

            services.AddMultiLingualSupport();

            services.AddControllersWithViews().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());

                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddDistributedMemoryCache();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IViewRenderService, ViewRenderService>();

            //services.AddTransient<IBankAccountRepository, BankAccountRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                //if (DateTime.Now.Date.Month != 12)
                //    context.Response.StatusCode = 403;
                //else
                //{
                    await next();
                //}
            });

            app.UseNotyf();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseMultiLingualFeature();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Dashboard}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}