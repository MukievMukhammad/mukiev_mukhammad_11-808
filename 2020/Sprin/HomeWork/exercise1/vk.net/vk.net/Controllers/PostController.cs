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


        // Отображаем форму для добавления новых постов
        public async Task GetNewPostForm(HttpContext context)
        {
            await context.Response.WriteAsync(File
                .ReadAllText("Views/NewPostForm.html")
                .Replace("@action", "/Post/AddNew/"));
        }


        // Отображаем форму для редактирования постов
        public async Task GetEditForm(HttpContext context)
        {
            var postId = int.Parse(context.GetRouteValue("postId") as string);
            var post = storage.Get(postId);
            await context.Response.WriteAsync(File
                .ReadAllText("Views/NewPostForm.html")
                .Replace("@action", string.Format("/Post/Edit/{0}", postId))
                .Replace("NAME", post.Name)
                .Replace("TEXT", post.Text));
        }


        // Добавляем новый пост
        public async Task AddNew(HttpContext context)
        {
            //var newEntry = new BlogEntry
            //{
            //    Name = context.Request.Form["name"],
            //    Text = context.Request.Form["text"],
            //    FileDirectories = await SavePostFilesAsync(context, context.Request.Form["name"])
            //};

            var newEntry = new BlogEntry(context);

            var validationResult = Validation.Validation.Validate(newEntry);

            if (validationResult.IsValid)
            {
                storage.Add(newEntry);

                await context.Response.WriteAsync("New Post was added!");
                return;
            }

            if (validationResult.ErrorMessage == "Fileld should not be empty")
                await context.Response.WriteAsync(File
                .ReadAllText("Views/NewPostForm.html")
                .Replace("@action", "/Post/AddNew/")
                .Replace("<!-- name_error_msg -->", validationResult.ErrorMessage));
            else if (validationResult.ErrorMessage == "Name should start with Uppercase letter")
                await context.Response.WriteAsync(File
                .ReadAllText("Views/NewPostForm.html")
                .Replace("@action", "/Post/AddNew/")
                .Replace("<!-- name_error_msg -->", validationResult.ErrorMessage));
        }

        
        // Отображаем список всех постов
        public async Task AllPostsAsync(HttpContext context)
        {
            var responseContext = new StringBuilder();

            var posts = storage.AllPosts();

            // Формирует ответ
            foreach (var post in posts)
                responseContext.Append
                    (
                    string.Format(
                        @"<tr>
                        <td><a href=""/Post/Detail/{0}"">{1}</a></td>
                        <td><a href=""/Post/Delete/{0}"">Delete</a></td>
                        <td><a href=""/Post/Edit/{0}"">Edit</a></td>
                        </tr>",
                        post.Id,
                        post.Name)
                    );
            
            var response = File
                .ReadAllText("Views/PostsList.html")
                .Replace("@Model", responseContext.ToString());
            await context.Response.WriteAsync(response);
        }


        // Отображаем детали поста
        public async Task PostDetailAsync(HttpContext context, string commentError = "")
        {
            var postId = int.Parse(context.GetRouteValue("postId") as string);
            var post = storage.Get(postId);

            var response = File
                .ReadAllText("Views/PostDetail.html")
                .Replace("@Model", post.Text);

            var fileList = new StringBuilder();
            foreach(var fileDir in post.FileDirectories)
                fileList.Append($"<img src=\"{fileDir}\"/></br>");
            var comments = new StringBuilder();
            foreach (var comment in post.Comments)
                comments.Append($"<p>{comment.Content}</p>");

            response = response
                .Replace("@Files", fileList.ToString())
                .Replace("@modelId", post.Id.ToString())
                .Replace("@Comments", comments.ToString()
                .Replace("<!-- text_error_msg -->", commentError));
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


        public async Task AddComment(HttpContext context)
        {
            var content = context.Request.Form["content"];
            var postId = int.Parse(context.Request.Form["postId"]);
            var newComment = new Comment
            {
                PostId = postId,
                Content = content
            };

            var validateResult = Validation.Validation.Validate(newComment);

            if (!validateResult.IsValid)
                await PostDetailAsync(context, validateResult.ErrorMessage);
            else
            {
                storage.Add(newComment);
                await context.Response.WriteAsync("New comment was added!");
            }
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
