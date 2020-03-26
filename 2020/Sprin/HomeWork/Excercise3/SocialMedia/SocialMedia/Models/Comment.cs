using System;
namespace SocialMedia.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
