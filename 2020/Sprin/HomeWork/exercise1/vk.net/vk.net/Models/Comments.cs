using System;
using vk.net.Validation;

namespace vk.net.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        [TextStartUppercase]
        public string Content { get; set; }
        
    }
}
