using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vk.net.Controllers;
using vk.net.Services;

namespace vk.net
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddTransient<IStorage, BlogEntiesStorage>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStorage storage)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapGet("Post/Detail/{postId}", new PostController(storage).PostDetailAsync);
            routeBuilder.MapGet("Post/Delete/{postId}", new PostController(storage).DeletePost);
            routeBuilder.MapGet("Post/Edit/{postId}", new PostController(storage).GetEditForm);
            routeBuilder.MapPost("Post/Edit/{postId}", new PostController(storage).EditPost);
            app.UseRouter(routeBuilder.Build());
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Post/AddNew", new PostController(storage).GetForm);
                endpoints.MapGet("/Post/AllPosts", new PostController(storage).AllPostsAsync);
                //endpoints.MapPost("Post/Detail/{postId}", new PostController().PostDetailAsync);
                endpoints.MapPost("/Post/AddNew", new PostController(storage).AddNew);
            });
        }
    }
}
