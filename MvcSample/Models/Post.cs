using System;

namespace MvcSample.Models
{
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
    }
}