using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using vk.net.Validation;

namespace vk.net.Models
{
    public class BlogEntry
    {
        public int Id { get; set; }

        [NotEmpty]
        [TextStartUppercase]
        public string Name { get; set; }

        [NotEmpty]
        [TextStartUppercase]
        public string Text { get; set; }

        public List<string> FileDirectories { get; set; }

        public List<Comment> Comments { get; set; }

        public BlogEntry() { }

        public BlogEntry(HttpContext context)
        {
            Name = context.Request.Form["name"];
            Text = context.Request.Form["text"];
            FileDirectories = SavePostFilesAsync(context, context.Request.Form["name"]).Result;
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
