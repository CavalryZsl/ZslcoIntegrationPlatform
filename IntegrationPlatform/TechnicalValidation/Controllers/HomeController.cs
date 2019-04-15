using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalValidation.Controllers
{
    /// <summary>
    /// 不需要继承IController 
    /// </summary>
    [Route("Home")]
    [Route("v2/[controller]")] //表示本controller 可以加前缀
    public class HomeController
    {
        [Route("")] //为空则表示 访问controller就会到这个action
        [Route("[action]")]
        public string Index ()
        {
            return "this message from Home/Index";
        }
    }
}
