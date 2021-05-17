using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class User
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Необходим UserId")]
        public int UsrId { get; set; }
        [Display (Name = "Имя пользователя")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо имя пользователя")]
        public string Username { get; set; }
        [Display (Name = "Email адрес")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Необходим Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display (Name = "Пароль")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Пароль должен иметь минимум 6 символов")]
        public string Password { get; set; }
        //[Display (Name = "SteamId пользователя")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Введите SteamId")]
        //public string SteamId { get; set; }
    }
}