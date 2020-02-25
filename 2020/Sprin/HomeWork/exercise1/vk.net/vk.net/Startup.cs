using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vk.net.Controllers;

namespace vk.net
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapGet("Post/Detail/{postId}", new PostController().PostDetailAsync);
            routeBuilder.MapGet("Post/Delete/{postId}", new PostController().DeletePost);
            routeBuilder.MapGet("Post/Edit/{postId}", new PostController().GetEditForm);
            routeBuilder.MapPost("Post/Edit/{postId}", new PostController().EditPost);
            app.UseRouter(routeBuilder.Build());
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Post/AddNew", new PostController().GetForm);
                endpoints.MapGet("/Post/AllPosts", new PostController().AllPostsAsync);
                //endpoints.MapPost("Post/Detail/{postId}", new PostController().PostDetailAsync);
                endpoints.MapPost("/Post/AddNew", new PostController().AddNew);
            });
        }
    }
}
