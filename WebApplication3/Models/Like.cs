using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public bool IsLiked { get; set; }
    }
}