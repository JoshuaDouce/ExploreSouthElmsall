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
        public IActionResult Index()
        {
            return View();
        }

        [Route("{year:int}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = new BlogPost
            {
                Title = "My Blog Post",
                Posted = DateTime.Now,
                Author = "Joshua Douce",
                Body = "What a great blog post"
            };

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

            return View();
        }
    }
}