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
            var files = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories).ToList();
            if (!files.Contains(blogEntry.Id + ".txt"))
                throw new ArgumentNullException("Такого поста не существует; вы пытаетесь изменить не существующий пост!");

            var fileDirs = new StringBuilder();
            foreach (var dir in blogEntry.FileDirectories)
                fileDirs.Append(dir + ',');

            File.WriteAllText(
                Path.Combine(filePath, blogEntry.Id + ".txt"),
                blogEntry.Name + '\n' + blogEntry.Text + '\n' + fileDirs);
        }

        public List<BlogEntry> AllPosts()
        {
            throw new System.NotImplementedException();
        }

        public BlogEntry Get(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
