using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class UserLogin
    {
        [Display(Name = "Email адрес")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Необходим Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Необходим пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Запомнить вход?")]
        public bool RememberMe { get; set; }
    }
}