using CloudCollector.Interface;
using CloudCollector.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudCollector.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ICateGoryService _category;
        private readonly IArchiver _archiver;
        private readonly IBase _ibase;
        public AdminController(ILogger<AdminController> logger,
            ICateGoryService category,
            IArchiver archiver, CloudCollectorContext context, IBase ibase) : base(ibase)
        {
            _logger = logger;
            _category = category;
            _archiver = archiver;
            _ibase = ibase;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Cloud([Bind("Name,Memo,TypeId,CreatorName,Pic")] Cloud cloud)
        {
            if (string.IsNullOrWhiteSpace(cloud.CreatorName))
            {
                //名称需要填写，此处是为了演示AddModelError方法，实际Name必填应使用Required注释控制
                //ModelState.AddModelError("", "名称需要填写");
                ModelState.AddModelError(nameof(cloud.Name), "名称需要填写");
                return View(nameof(Cloud));
            }

            if (ModelState.IsValid)
            {
                var msg = await _archiver.Insert(cloud);
                if (msg != "Success")
                {
                    ModelState.AddModelError(nameof(cloud.CreatorName), msg);
                }
            }
            return View();
        }
    }
}
