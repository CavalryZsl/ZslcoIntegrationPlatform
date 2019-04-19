using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalValidation.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
    
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
        public Gender gender { get; set; }

    }
}
