using CloudCollector.Interface;
using CloudCollector.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace CloudCollector.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICateGoryService _category;
        private readonly IArchiver _archiver;
        private readonly IBase _ibase;

        public HomeController(ILogger<HomeController> logger,
            ICateGoryService category,
            IArchiver archiver,
             IBase ibase) : base(ibase)
        {
            _logger = logger;
            _category = category;
            _archiver = archiver;
            _ibase= ibase;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 5)
        {
            base.ViewBag.category = await _category.All();
            ViewData["SearchString"] = searchString;
            ViewData["PageSize"] = pageSize; //每页最多显示的记录数的返回给视图。以便在下次查询和排序的时候，每页也显示相同的记录数。
            ViewData["PageIndex"] = page;
            var list = await _archiver.ToPageList(searchString,page,pageSize);
            return View(list); //执行异步方法；
        }

        public async Task<IActionResult> Cloud()
        {
            var list = await _archiver.All(20);
            return View(list);
        }

        public async Task<IActionResult> Archives()
        {
            var list = await _archiver.All(10);
            return View(list);
        }

        //public async Task<IActionResult> Message(int page = 1, int pageSize = 5)
        //{
        //    ViewData["PageSize"] = pageSize; //每页最多显示的记录数的返回给视图。以便在下次查询和排序的时候，每页也显示相同的记录数。
        //    ViewData["PageIndex"] = page;
        //    var list = _category.ToPageList(page, pageSize);
        //    return View(await list); //执行异步方法；
        //}

        //[HttpPost]
        public async Task<IActionResult> Message([Bind("CreatorName,Context")] Message message,int page = 1, int pageSize = 15)
        {
            if (!string.IsNullOrWhiteSpace(message.CreatorName))
            {
                //if (string.IsNullOrWhiteSpace(message.CreatorName))
                //{
                //    //名称需要填写，此处是为了演示AddModelError方法，实际Name必填应使用Required注释控制
                //    //ModelState.AddModelError("", "名称需要填写");
                //    ModelState.AddModelError(nameof(message.CreatorName), "名称需要填写");
                //    return View(nameof(Message));
                //}

                if (ModelState.IsValid)
                {
                    var msg = await _category.Insert(message);
                    if (msg != "Success")
                    {
                        ModelState.AddModelError(nameof(message.CreatorName), msg);
                    }
                }
            }
                ViewData["PageSize"] = pageSize; //每页最多显示的记录数的返回给视图。以便在下次查询和排序的时候，每页也显示相同的记录数。
                ViewData["PageIndex"] = page;
                var list = _category.ToPageList(page, pageSize,null);
                return View(await list); //执行异步方法；
            //return View();
        }

        public async Task<IActionResult> Detail(int id, [Bind("CloudId,CreatorName,Context")] Message message)
        {
            if (!string.IsNullOrWhiteSpace(message.CreatorName))
            {
                //if (string.IsNullOrWhiteSpace(message.CreatorName))
                //{
                //    //名称需要填写，此处是为了演示AddModelError方法，实际Name必填应使用Required注释控制
                //    //ModelState.AddModelError("", "名称需要填写");
                //    ModelState.AddModelError(nameof(message.CreatorName), "名称需要填写");
                //    return View(nameof(Message));
                //}
                if (ModelState.IsValid)
                {
                    var msg = await _category.Insert(message);
                    if (msg != "Success")
                    {
                        ModelState.AddModelError(nameof(message.CreatorName), msg);
                    }
                }
                //id = message.CloudId.Value;
            }
            else
            {
                if (id > 0)
                   await _archiver.Update(id);
            }
            if (message.CloudId.HasValue)
            {
                id = message.CloudId.Value;
            }
            var messsage= await _category.ToPageList(1, 30, id);
            var detail = await _archiver.Get(id);
            var result = new DetailM
            {
                cloud =  detail,
                message = messsage,
            };
           
            return View(result);
        }

        public async Task<IActionResult> About()
        {
            var list = await _category.Aboutme();
            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
