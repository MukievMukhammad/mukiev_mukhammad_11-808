using System;
using System.Collections.Generic;
using vk.net.Models;

namespace vk.net.Services
{
    public interface IStorage
    {
        public void Add(BlogEntry blogEntry);
        public void Delete(string fileName);
        public List<BlogEntry> AllPosts();
        public BlogEntry Get(string fileName);
    }
}
