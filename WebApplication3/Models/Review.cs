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
        [Column(Order = 1)]
        public int ReviewId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int UsrId { get; set; }
        public User User { get; set; }
        [Key]
        [Column(Order = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Выберите игру")]

        public int GameId { get; set; }
        public Game Game { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите Username")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите вашу оценку (0-10)")]
        public int ReviewScore { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите ваш отзыв")]
        [MinLength(10, ErrorMessage = "Отзыв должен состоять как минимум из 10 символов")]
        public string ReviewText { get; set; }
    }

}