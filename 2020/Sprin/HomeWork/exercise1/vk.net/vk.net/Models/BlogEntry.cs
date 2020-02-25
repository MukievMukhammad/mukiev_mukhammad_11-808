using System;
using System.Collections.Generic;

namespace vk.net.Models
{
    public class BlogEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public List<string> FileDirectories { get; set; }
    }
}
