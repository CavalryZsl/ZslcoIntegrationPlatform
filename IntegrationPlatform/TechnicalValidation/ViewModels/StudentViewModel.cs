using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalValidation.Model;

namespace TechnicalValidation.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender gender { get; set; }
    }
}
