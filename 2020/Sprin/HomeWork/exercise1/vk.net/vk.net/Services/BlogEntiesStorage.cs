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


        // Добавляет новый пост на жесткий диск
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

            File.WriteAllLines(
                Path.Combine(filePath, blogEntry.Id + ".txt"),
                new string[] { blogEntry.Name, blogEntry.Text, fileDirs.ToString() });
        }


        public List<BlogEntry> AllPosts()
        {
            var result = new List<BlogEntry>();
            var fielIds = Directory
                .GetFiles(filePath, "*.txt", SearchOption.AllDirectories)
                .Select(dir => int.Parse(dir.Split('/')[1].Split('.')[0])); // directory format: Files/postId.txt
            foreach(var id in fielIds)
            {
                try
                {
                    var entry = Get(id);
                    result.Add(entry);
                }
                catch { }
            }
            return result;
        }


        public BlogEntry Get(int id)
        {
            var content = File.ReadAllLines(
                    Path.Combine(filePath, id + ".txt"));
            var entry = new BlogEntry()
            {
                Id = id,
                Name = content[0],
                Text = content[1],
                FileDirectories = GetFilesDirs(content),
                Comments = GetPostComments(content, id)
            };
            return entry;
        }


        // Достаем местоположение всех прикрепленных к посту файлов
        private List<string> GetFilesDirs(string[] dirs)
        {
            var fileDirs = new List<string>();
            try
            {
                fileDirs = dirs[2].Split(',').ToList();
            }
            catch { }
            return fileDirs;
        }


        // Достаем все комментария относящиеся к определенному посту
        private List<Comment> GetPostComments(string[] content, int postId)
        {
            var comments = new List<Comment>();
            try
            {
                var commentsIds = content[3].Split(',');
                foreach (var commentId in commentsIds)
                {
                    if (commentId == "") continue;

                    var comment = new Comment
                    {
                        Id = int.Parse(commentId),
                        Content = File.ReadAllLines(
                            Path.Combine(filePath, commentId + ".html"))[1],
                        PostId = postId
                    };
                    comments.Add(comment);
                }
            }
            catch { }
            return comments;
        }

        public void Add(Comment comment)
        {
            comment.Id = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories).Length + 1;

            File.AppendAllLines(
                Path.Combine(filePath, comment.Id + ".html"),
                new string[] { comment.PostId.ToString(), comment.Content });

            File.AppendAllText(
                Path.Combine(filePath, comment.PostId + ".txt"),
                comment.Id.ToString() + ',');
        }
    }
}
