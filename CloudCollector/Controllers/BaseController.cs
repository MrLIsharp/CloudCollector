using CloudCollector.Interface;
using CloudCollector.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudCollector.Controllers
{
    public class BaseController : Controller
    {
        private readonly IBase _ibase;

        public BaseController(IBase ibase)
        {
            _ibase = ibase;
            
        }

        /// <summary>
        /// 执行Action之前就行请求拦截
        /// </summary>
        /// <param name="filterContext">action拦截器上下文</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            base.ViewBag.CommData = _ibase.CommData();
        }
    }
}
