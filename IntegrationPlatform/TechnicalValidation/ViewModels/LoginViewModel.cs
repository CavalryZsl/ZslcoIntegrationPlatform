using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalValidation.ViewModels
{
    public class LoginViewModel
    {
        [Required,Display(Name ="用户名")]
        public string UserName { get; set; }
        [Required,Display(Name="密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
