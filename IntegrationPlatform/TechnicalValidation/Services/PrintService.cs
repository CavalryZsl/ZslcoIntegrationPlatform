using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalValidation.IServices;

namespace TechnicalValidation.Services
{
    public class PrintService : IPrintService

    {
        public string GetMessge()
        {
            return $"Hello World from {nameof(PrintService)}";
        }
    }
}
