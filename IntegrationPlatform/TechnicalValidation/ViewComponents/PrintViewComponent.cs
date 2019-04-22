using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalValidation.IServices;

namespace TechnicalValidation.ViewComponents
{
    public class PrintViewComponent: ViewComponent
    {
        private readonly IPrintService printService;
        public PrintViewComponent(IPrintService PrintService)
        {
            printService = PrintService;
        }
        public IViewComponentResult Invoke(object obj)
        {
            return View("Welcome",printService.GetMessge());
        }

    }
}
