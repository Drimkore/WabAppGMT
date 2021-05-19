using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Выберите игру")]
        public int GameId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите вашу оценку (0-10)")]
        public int ReviewScore { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите ваш отзыв")]
        [MinLength(10, ErrorMessage = "Отзыв должен состоять как минимум из 10 символов")]
        public string ReviewText { get; set; }
    }

}