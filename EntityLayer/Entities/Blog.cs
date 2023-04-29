using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Blog
    {
        [Key]
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public string BlogImage { get; set; }
        public DateTime BlogCreatedDate { get; set; }
        public bool BlogStatus { get; set; }


        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }


        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
