using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int ReviewScore { get; set; }
        public string ReviewText { get; set; }
    }
}