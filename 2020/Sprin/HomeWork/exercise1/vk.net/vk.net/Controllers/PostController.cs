using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace vk.net.Controllers
{
    public class PostController
    {
        public async Task GetForm(HttpContext context)
        {
            await context.Response.WriteAsync(File.ReadAllText("Views/NewPostForm.html").Replace("@action", "/Post/AddNew/"));
        }

        public async Task AddNew(HttpContext context)
        {
            var filePath = "Files";

            var fileCount = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories).Length;
            fileCount++;

            await SavePostFilesAsync(context, filePath, fileCount);

            SavePostContent(context, filePath, fileCount);
                    
            await context.Response.WriteAsync("New Post was added!");
        }

        private async Task SavePostFilesAsync(HttpContext context, string filePath, int fileCount)
        {
            foreach (var formFile in context.Request.Form.Files)
            {
                if (formFile.Length > 0)
                {
                    string newFile = Path.Combine(filePath, fileCount + Path.GetExtension(formFile.FileName));
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
        }

        private void SavePostContent(HttpContext context, string filePath, int fileCount)
        {
            var name = context.Request.Form["name"];
            var text = context.Request.Form["text"];
            var txtFileName = Path.Combine(filePath, fileCount + ".txt");
            File.AppendAllLines(txtFileName, new string[] { name, text });
        }

        public async Task AllPostsAsync(HttpContext context)
        {
            var filePath = "Files";

            var files = Directory.GetFiles(filePath, "*.txt");

            var postList = new StringBuilder();

            foreach(var file in files)
            {
                var refToPost = string.Format(
                    @"<p><a href=""/Post/Detail/{0}"">{1}</a> <a href=""/Post/Delete/{0}"">Delete</a> <a href=""/Post/Edit/{0}"">Edit</a> </p></br> ",
                    file.Split('.').First().Split('/')[1],
                    File.ReadLines(file).First());

                postList.Append(refToPost);
                var content = string.Format(@"<div>{0}</div>", File.ReadAllText(file));
                postList.Append(content);
            }
            
            var response = File.ReadAllText("Views/PostsList.html").Replace("@Model", postList.ToString());

            await context.Response.WriteAsync(response);
        }

        public async Task PostDetailAsync(HttpContext context)
        {
            var fileName = context.GetRouteValue("postId") as string;
            //var file = Directory.GetFiles(fileName + ".txt");
            var _context = File.ReadAllText("Files/" + fileName + ".txt");
            var response = File.ReadAllText("Views/PostDetail.html").Replace("@Model", _context);
            response = response.Replace("#", string.Format("Files/{0}.png", fileName));

            await context.Response.WriteAsync(response);
        }

        public async Task DeletePost(HttpContext context)
        {
            var fileName = context.GetRouteValue("postId") as string;
            try
            {
                File.Delete("Files/" + fileName + ".txt");
                File.Delete("Files/" + fileName + ".png");
            }
            catch { }
            await AllPostsAsync(context);
        }

        public async Task EditPost(HttpContext context)
        {
            var filePath = "Files";
            var fileName = context.GetRouteValue("postId") as string;
            var name = context.Request.Form["name"];
            var text = context.Request.Form["text"];
            var txtFileName = Path.Combine(filePath, fileName + ".txt");
            File.WriteAllText(txtFileName, name + '\n' + text);

            try
            {
                File.Delete(filePath + fileName + ".png");
            }
            catch { }
            await SavePostFilesAsync(context, filePath, int.Parse(fileName));

            await AllPostsAsync(context);
        }

        public async Task GetEditForm(HttpContext context)
        {
            var postId = context.GetRouteValue("postId");
            await context.Response.WriteAsync(File
                .ReadAllText("Views/NewPostForm.html")
                .Replace("@action", string.Format("/Post/Edit/{0}", postId)));
        }
    }
}
