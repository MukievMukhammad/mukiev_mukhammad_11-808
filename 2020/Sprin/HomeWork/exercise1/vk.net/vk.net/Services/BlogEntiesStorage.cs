using System.Collections.Generic;
using vk.net.Models;

namespace vk.net.Services
{
    public class BlogEntiesStorage : IStorage
    {
        public IEnumerable<BlogEntry> BlogEntries { get; set; }

        public BlogEntiesStorage()
        {
        }
    }
}
