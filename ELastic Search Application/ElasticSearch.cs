using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ELastic_Search_Application
{
    public class ElasticSearch
    {
        ElasticClient client = null;
        string IndexName = "user_blogs";
        public ElasticSearch()
        {
            var uri = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(uri);
            client = new ElasticClient(settings);
            settings.DefaultIndex(IndexName);
        }
        public void AddNewIndex(Post model)
        {
            client.IndexAsync<Post>(model, null);
        }
        public void CreateDummyData()
        {
            Post post2, post1, post3,post4;

            post1 = new Post
            {
                UserId = 1,
                PostDate = DateTime.Now,
                PostText = "This is the first blog post for 90's kid"
            };
            post2 = new Post
            {
                UserId = 2,
                PostDate = DateTime.Now,
                PostText = "This is the blog post for How to Write an Awesome Blog Post in 5 Steps Writing a blog post is a little like driving; you can study the highway code (or read articles telling you how to write a blog post) for months, but nothing can prepare you for the real thing like getting behind the wheel and hitting the open road. Or something."
            };
            post3 = new Post
            {
                UserId = 3,
                PostDate = DateTime.Now,
                PostText = "Techcrunch Techcrunch began in 2005 as a blog about dotcom start - ups in Silicon Valley, but has quickly become one of the most influential news websites across the entire technology industry."
            };
            post4 = new Post
            {
                UserId = 3,
                PostDate = DateTime.Now,
                PostText = "Kottke One of the early wave of blogging pioneers, web designer Jason Kottke started keeping track of interesting things on the internet as far back as 1998.The site took off, boosted partly through close links to popular blog - building website Blogger(he later married one of the founders)."
            };
            AddNewIndex(post1);
            AddNewIndex(post2);
            AddNewIndex(post3);
            AddNewIndex(post4);
        }
        public void PerformTermQuery()
        {
            var result = client.Search<Post>(s => s
                 .Query(p => p.Term(q => q.PostText, "first")));
            Console.WriteLine("Number of Hits : " + result.Hits.Count);
            Console.WriteLine("Search Result : " + result.Documents.ElementAtOrDefault(0).PostText);
        }
        public void PerformMatchPhrase()
        {
            var result = client.Search<Post>(s => s
                .Query(q => q.MatchPhrase(m => m.Field("postText")
                .Query("is the Third blog"))));
            Console.WriteLine("Number of Hits : " + result.Hits.Count);
            Console.WriteLine("Search Result : " + result.Documents.ElementAtOrDefault(0).PostText);
        }
        public void PerormFilter()
        {
            var result = client.Search<Post>(s => s
                 .Query(p => p.Term(q => q.PostText, "blog"))
                 .PostFilter(f => f.Range(r => r.Field("userId").LessThanOrEquals(2))));
            Console.WriteLine("Number of Hits : " + result.Hits.Count);
        }

    }

}
