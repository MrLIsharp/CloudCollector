using CloudCollector.Interface;
using CloudCollector.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudCollector.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;
        private readonly ICateGoryService _category;
        private readonly IArchiver _archiver;
        private readonly IBase _ibase;

        public BaseController(
            ILogger<BaseController> logger,
            ICateGoryService category,
            IArchiver archiver,
            IBase ibase)
        {
            _logger = logger;
            _category = category;
            _archiver = archiver;
            _ibase = ibase;

        }
        /// <summary>
        /// 获取云彩主分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> GetCatage()
        {
            var result= await _category.All();
            return Ok(result);
        }
        /// <summary>
        /// 获取公共数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic Get()
        {
            var result = _ibase.CommData();
            return Ok(result);
        }
        /// <summary>
        /// 分页获取云彩
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> GetCloud(string searchString, int page = 1, int pageSize = 5)
        {
            var list = await _archiver.ToPageList(searchString, page, pageSize);
            return Ok(list);
        }
        /// <summary>
        /// 天假云彩
        /// </summary>
        /// <param name="cloud"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<dynamic> PostCloud([FromBody]Cloud cloud)
        {
            var msg=await _archiver.Insert(cloud);
            if (msg == "Succeed")
                return Ok();
            else
                return new{
                    State = 200,
                    Msg = msg,
                };
        }
    }
}
