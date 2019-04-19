using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechnicalValidation.IServices;
using TechnicalValidation.Model;
using TechnicalValidation.ViewModels;

namespace TechnicalValidation.Controllers
{
    /// <summary>
    /// 集成了Controller的控制器
    /// </summary>
    public class MainController : Controller
    {
        private readonly IRepository<Student> _repository;
        public MainController(IRepository<Student> repository)
        {
            _repository = repository; //注入
        }
        public IActionResult Index()
        {
            var list = _repository.GetAll();
            return View(list);
        }
        public async  Task<IActionResult> About()
        {
           return await Task.Factory.StartNew<IActionResult>(()=>  new ContentResult { Content = "this message from Task" });

        }
        public async Task<IActionResult> Test()
        {
            var st = new Student { Id = 1, FirstName = "Green", LastName = "Jim" };
            return await Task.Factory.StartNew<IActionResult>(() => new OkObjectResult(st));//OkObjectResult根据管道的设置和请求头的信息 返回JSON或者XML

        }
        public ActionResult Info()
        {
            var list= _repository.GetAll();
            var vms = list.Select(a => new StudentViewModel
            {
                Id = a.Id,
                Name = $"{a.FirstName}+ {a.LastName}",
                Age = DateTime.Now.Subtract(a.Birthday).Days / 365,
                gender=a.gender
            });
            var dto = new MainInfoViewModel { students = vms }; //输出的model
            return  View(dto);
        }
        public ActionResult Details(int id)
        {
            Student st = _repository.GetById(id);
            if (st == null) RedirectToAction("Info");
            return View(st);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  //防止跨站请求伪造  即前端页面的__RequestVerificationToken 验证
        public ActionResult Create(StudentInPutViewModel student)
        {
            if (!ModelState.IsValid) return View();
            var stusdent = new Student {
                FirstName=student.FirstName,
                LastName=student.LastName,
                Birthday=student.Birthday,
                gender=student.gender
            };
            var newmodel=_repository.Add(stusdent);
            //return View("Details", stusdent); //防止保存成功后刷新 重新提交重复数据 使用下面的 POST-REDIRACT-GET
            return RedirectToAction(nameof(Details), new { id = newmodel.Id });
        }
    }
}