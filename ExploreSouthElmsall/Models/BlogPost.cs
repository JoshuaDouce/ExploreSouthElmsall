using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExploreSouthElmsall.Models
{
    public class BlogPost
    {
        public long Id { get; set; }
        private string _key;
        public string key {
            get {
                if (_key == null)
                {
                    _key = Regex.Replace(Title.ToLower(), "[^a-z0-9]", "-");
                }
                return _key;
            }
        }
        [Display(Name = "Post Title")]
        [DataType(DataType.Text)]
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Must be between 5 and 100 characters")]
        public string Title { get; set; }
        public string Author { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Must be at least 10 characters")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public DateTime Posted { get; set; }
    }
}
