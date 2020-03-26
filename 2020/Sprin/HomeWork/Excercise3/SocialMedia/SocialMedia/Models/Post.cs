using System;
namespace SocialMedia.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
