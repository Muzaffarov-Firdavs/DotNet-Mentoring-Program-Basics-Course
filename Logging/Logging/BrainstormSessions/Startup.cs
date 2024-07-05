using System;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using BrainstormSessions.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace BrainstormSessions
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemoryDb"));

            services.AddControllersWithViews();

            services.AddScoped<IBrainstormSessionRepository, EFStormSessionRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                var repository = serviceProvider.GetRequiredService<IBrainstormSessionRepository>();

                InitializeDatabaseAsync(repository).Wait();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public async Task InitializeDatabaseAsync(IBrainstormSessionRepository repo)
        {
            List<BrainstormSession> sessionList = await repo.ListAsync();
            if (!sessionList.Any())
            {
                await repo.AddAsync(GetTestSession());
            }
        }

        public static BrainstormSession GetTestSession()
        {
            var session = new BrainstormSession()
            {
                Name = "Test Session 1",
                DateCreated = new DateTime(2016, 8, 1)
            };
            var idea = new Idea()
            {
                DateCreated = new DateTime(2016, 8, 1),
                Description = "Totally awesome idea",
                Name = "Awesome idea"
            };
            session.AddIdea(idea);
            return session;
        }
    }
}
