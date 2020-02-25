using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using vk.net.Models;

namespace vk.net.Services
{
    public class BlogEntiesStorage : IStorage
    {
        public IEnumerable<BlogEntry> BlogEntries { get; set; }
        private static string filePath = "Files";

        public BlogEntiesStorage()
        {
        }

        // Добавляет новый элемент на жесткий диск
        public void Add(BlogEntry blogEntry)
        {
            blogEntry.Id = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories).Length + 1;

            var fileDirs = new StringBuilder();
            foreach (var dir in blogEntry.FileDirectories)
                fileDirs.Append(dir + ',');

            File.AppendAllLines(
                Path.Combine(filePath, blogEntry.Id + ".txt"),
                new string[] { blogEntry.Name, blogEntry.Text, fileDirs.ToString() });
        }

        public void Delete(int id)
        {
            File.Delete(Path.Combine(filePath, id + ".txt"));
            File.Delete(Path.Combine(filePath, id + ".png"));
            File.Delete(Path.Combine(filePath, id + ".jpg"));
            File.Delete(Path.Combine(filePath, id + ".jpeg"));
        }

        public void Save(BlogEntry blogEntry)
        {
            var files = Directory.GetFiles(filePath, "*.txt", SearchOption.AllDirectories).ToList();
            if (!files.Contains(Path.Combine(filePath, blogEntry.Id + ".txt")))
                throw new ArgumentNullException("Такого поста не существует; вы пытаетесь изменить не существующий пост!");

            var fileDirs = new StringBuilder();
            foreach (var dir in blogEntry.FileDirectories)
                fileDirs.Append(dir + ',');

            File.WriteAllText(
                Path.Combine(filePath, blogEntry.Id + ".txt"),
                blogEntry.Name + '\n' + blogEntry.Text + '\n' + fileDirs + '\n');
        }

        public List<BlogEntry> AllPosts()
        {
            var result = new List<BlogEntry>();
            var fielIds = Directory
                .GetFiles(filePath, "*.txt", SearchOption.AllDirectories)
                .Select(dir => int.Parse(dir.Split('/')[1].Split('.')[0]));
            foreach(var id in fielIds)
            {
                var entry = Get(id);
                result.Add(entry);
            }
            return result;
        }

        public BlogEntry Get(int id)
        {
            var comments = new List<Comment>();
            var content = File.ReadAllLines(
                    Path.Combine(filePath, id + ".txt"));
            var fileDirs = new List<string>();
            try
            {
                fileDirs = content[2].Split(',').ToList();
            }
            catch { }
            try
            {
                var commentsId = content[3].Split(',');
                foreach (var commentId in commentsId)
                {
                    if (commentId == "") continue;

                    var comment = new Comment
                    {
                        Id = int.Parse(commentId),
                        Content = File.ReadAllLines(
                            Path.Combine(filePath, commentId + ".txt"))[1],
                        PostId = id
                    };
                    comments.Add(comment);
                }
            }
            catch { }

            var entry = new BlogEntry
            {
                Id = id,
                Name = content[0],
                Text = content[1],
                FileDirectories = fileDirs,
                Comments = comments
            };
            return entry;
        }

        public void Add(Comment comment)
        {
            comment.Id = Directory.GetFiles(filePath, "*.txt", SearchOption.AllDirectories).Length + 1;

            File.AppendAllLines(
                Path.Combine(filePath, comment.Id + ".txt"),
                new string[] { comment.PostId.ToString(), comment.Content });

            File.AppendAllText(
                Path.Combine(filePath, comment.PostId + ".txt"),
                comment.Id.ToString() + ',');
        }
    }
}
