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
            var filePath = "Files";

            var fileCount = Directory
                .GetFiles(filePath, "*.*", SearchOption.AllDirectories)
                .Length;
            fileCount++;

            await SavePostFilesAsync(context, filePath, fileCount);
            SavePostContent(context, filePath, fileCount);
                    
            await context.Response.WriteAsync("New Post was added!");
        }

        // Отображаем список всех постов
        public async Task AllPostsAsync(HttpContext context)
        {
            var postList = new StringBuilder();

            var filePath = "Files";
            var files = Directory.GetFiles(filePath, "*.txt");

            foreach(var file in files)
            {
                var refToPost = string.Format(
                    @"<p><a href=""/Post/Detail/{0}"">{1}</a>
                    <a href=""/Post/Delete/{0}"">Delete</a> <a href=""/Post/Edit/{0}"">Edit</a> </p></br> ",
                    file
                    .Split('.')
                    .First()
                    .Split('/')[1],
                    File.ReadLines(file).First()
                    );
                postList.Append(refToPost);

                //var content = string.Format(@"<div>{0}</div>", File.ReadAllText(file));
                //postList.Append(content);
            }
            
            var response = File
                .ReadAllText("Views/PostsList.html")
                .Replace("@Model", postList.ToString());
            await context.Response.WriteAsync(response);
        }

        // Отображаем детали поста
        public async Task PostDetailAsync(HttpContext context)
        {
            var filePath = "Files";

            var fileName = context.GetRouteValue("postId") as string;
            var _context = File.ReadAllText(filePath + '/' + fileName + ".txt");

            var response = File
                .ReadAllText("Views/PostDetail.html")
                .Replace("@Model", _context);
            response = response.Replace("#", string.Format("Files/{0}.png", fileName));
            await context.Response.WriteAsync(response);
        }

        // Удаляем указанный пост, а после отображаем список оставшихся постов
        public async Task DeletePost(HttpContext context)
        {
            var fileName = context.GetRouteValue("postId") as string;

            File.Delete("Files/" + fileName + ".txt");
            File.Delete("Files/" + fileName + ".png");
            File.Delete("Files/" + fileName + ".jpg");
            File.Delete("Files/" + fileName + ".jpeg");

            await AllPostsAsync(context);
        }

        // Редактируем пост
        public async Task EditPost(HttpContext context)
        {
            var filePath = "Files";
            var fileName = context.GetRouteValue("postId") as string;

            try
            {
                File.Delete(filePath + fileName + ".png");
            }
            catch { }
            await SavePostFilesAsync(context, filePath, int.Parse(fileName));
            SavePostContent(context, filePath, int.Parse(fileName));

            await AllPostsAsync(context);
        }

        // Сохраняем файлы из контекста
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

        // Записываем содержание, текст поста на жесткий диск
        private void SavePostContent(HttpContext context, string filePath, int fileCount)
        {
            var name = context.Request.Form["name"];
            var text = context.Request.Form["text"];
            var txtFileName = Path.Combine(filePath, fileCount + ".txt");
            File.WriteAllText(txtFileName, name + '\n' + text);
        }

    }
}
