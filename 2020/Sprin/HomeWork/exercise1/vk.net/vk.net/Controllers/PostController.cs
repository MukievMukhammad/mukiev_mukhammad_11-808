using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using vk.net.Models;
using vk.net.Services;

namespace vk.net.Controllers
{
    public class PostController
    {
        private readonly IStorage storage;

        public PostController(IStorage storage)
        {
            this.storage = storage;
        }


        // Отображаем форму для доавления новых постов
        public async Task GetForm(HttpContext context)
        {
            await context.Response.WriteAsync(File
                .ReadAllText("Views/NewPostForm.html")
                .Replace("@action", "/Post/AddNew/"));
        }


        // Отображаем форму для редактирования постов
        public async Task GetEditForm(HttpContext context)
        {
            var postId = context.GetRouteValue("postId");
            await context.Response.WriteAsync(File
                .ReadAllText("Views/NewPostForm.html")
                .Replace("@action", string.Format("/Post/Edit/{0}", postId)));
        }


        // Добавляем новый пост
        public async Task AddNew(HttpContext context)
        {
            var newEntry = new BlogEntry
            {
                Name = context.Request.Form["name"],
                Text = context.Request.Form["text"],
                FileDirectories = await SavePostFilesAsync(context, context.Request.Form["name"])
            };

            storage.Add(newEntry);
                    
            await context.Response.WriteAsync("New Post was added!");
        }

        
        // Отображаем список всех постов
        public async Task AllPostsAsync(HttpContext context)
        {
            var responseContext = new StringBuilder();

            var posts = storage.AllPosts();

            // Формирует ответ
            foreach(var post in posts)
            {
                var postHtml = string.Format(
                    @"<p><a href=""/Post/Detail/{0}"">{1}</a>
                    <a href=""/Post/Delete/{0}"">Delete</a> <a href=""/Post/Edit/{0}"">Edit</a> </p></br> ",
                    post.Id,
                    post.Name
                    );
                responseContext.Append(postHtml);
            }
            
            var response = File
                .ReadAllText("Views/PostsList.html")
                .Replace("@Model", responseContext.ToString());
            await context.Response.WriteAsync(response);
        }


        // Отображаем детали поста
        public async Task PostDetailAsync(HttpContext context)
        {
            var postId = int.Parse(context.GetRouteValue("postId") as string);
            var post = storage.Get(postId);

            var response = File
                .ReadAllText("Views/PostDetail.html")
                .Replace("@Model", post.Text);

            var fileList = new StringBuilder();
            foreach(var fileDir in post.FileDirectories)
                fileList.Append($"<img src=\"{fileDir}\"/></br>");

            response = response.Replace("@Files", fileList.ToString());
            await context.Response.WriteAsync(response);
        }


        // Удаляем указанный пост, а после отображаем список оставшихся постов
        public async Task DeletePost(HttpContext context)
        {
            var postId = int.Parse(context.GetRouteValue("postId") as string);
            storage.Delete(postId);
            
            await AllPostsAsync(context);
        }


        // Редактируем пост
        public async Task EditPost(HttpContext context)
        {
            var name = context.Request.Form["name"];
            var text = context.Request.Form["text"];
            var fileDir = await SavePostFilesAsync(context, context.Request.Form["name"]);

            var postId = int.Parse(context.GetRouteValue("postId") as string);
            var post = storage.Get(postId);
            post.Name = name;
            post.Text = text;
            post.FileDirectories = fileDir.Count != 0 ? fileDir : post.FileDirectories;
            storage.Save(post);

            await AllPostsAsync(context);
        }


        // Сохраняем файлы из контекста
        private async Task<List<string>> SavePostFilesAsync(HttpContext context, string fileName)
        {
            var fileDirs = new List<string>();
            foreach (var formFile in context.Request.Form.Files)
            {
                if (formFile.Length > 0)
                {
                    string newFile = Path.Combine("Files", fileName + Path.GetExtension(formFile.FileName));
                    fileDirs.Add(newFile);
                    using (var inputStream = new FileStream(newFile, FileMode.Create))
                    {
                        await formFile.CopyToAsync(inputStream);
                        byte[] array = new byte[inputStream.Length];
                        inputStream.Seek(0, SeekOrigin.Begin);
                        inputStream.Read(array, 0, array.Length);
                        string fName = formFile.FileName;
                    }
                }
            }

            return fileDirs;
        }
    }
}
