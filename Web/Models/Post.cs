using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string IconSrc { get; set; }

        public PostType PostType { get; set; } = PostType.News;

        public string Body { get; set; } = "";

        public DateTime Created { get; set; } = DateTime.Now;

        public PostCategory PostCategory { get; set; }
    }

    public class PostCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Post> Posts { get; set; }
    }

    public enum PostType
    {
        News,
        Product,
        AboutUs,
        ContactUs,
        Display,
        Support
    }
}