using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SurveyApp.Contexts;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using SurveyApp.Interfaces.SurveyInterfaces;
using SurveyApp.Repositories;
using SurveyApp.Repositories.SurveyRepositories;
using SurveyApp.Services;

namespace SurveyApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<SurveyContext>();

            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISurveyPollsterRepository, SurveyPollsterRepository>();

            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ITestPollsterRepository, TestPollsterRepository>();
            services.AddScoped<ITestQuestionRepository, TestQuestionRepository>();
            services.AddScoped<ITestAnswerRepository, TestAnswerRepository>();

            services.AddControllersWithViews();

            services.AddSingleton<ITestRepository, TestRepository>();
            services.AddSingleton<ITestPollsterRepository, TestPollsterRepository>();
            services.AddHostedService<Worker>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/Home/NotFound","?code={0}");

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{Controller=Admin}/{Action=AdminPage}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller=Home}/{Action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "survey",
                    pattern: "{Controller=Survey}/{Action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "test",
                    pattern: "{Controller=Test}/{Action=Index}/{id?}/{pid?}"
                    );

                endpoints.MapControllerRoute(
                   name: "login",
                   pattern: "{Controller=Login}/{Action=AdminLogin}"
                   );
            });
        }
    }
}
