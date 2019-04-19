using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechnicalValidation.Model;

namespace TechnicalValidation.ViewModels
{
    public class StudentInPutViewModel
    {
        [Display(Name = "名字")]
        [MaxLength(10), Required(ErrorMessage ="不能为空")]
        public string FirstName { get; set; }
        [Display(Name = "姓")]
        [MaxLength(10), Required]
        public string LastName { get; set; }
        [Display(Name = "出生日期")]
        [DataType(DataType.Date), Required]
        public DateTime Birthday { get; set; }
        [Display(Name = "性别"), Required]
        public Gender gender { get; set; }

    }
}
