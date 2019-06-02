using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreSouthElmsall.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExploreSouthElmsall.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _dbContext;

        public BlogController(BlogDataContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var posts = _dbContext.Posts.
                OrderByDescending(x => x.Posted)
                .Take(5)
                .ToArray();

            return View(posts);
        }

        [Route("{year:int}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = _dbContext.Posts.FirstOrDefault(X => X.Key == key);

            return View(post);
        }

        [Route("create")]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([Bind("Title", "Body")]BlogPost post)
        {
            if (!ModelState.IsValid) {
                return View();
            }

            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            _dbContext.Add(post);
            _dbContext.SaveChanges();

            return RedirectToAction("Post", "Blog", new {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key
            });
        }
    }
}