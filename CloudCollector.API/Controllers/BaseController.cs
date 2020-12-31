using CloudCollector.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudCollector.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;
        private readonly IBase _ibase;

        public BaseController(ILogger<BaseController> logger,IBase ibase)
        {
            _logger = logger;
            _ibase = ibase;

        }
        /// <summary>
        /// 获取公共数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic Get()
        {
            var result=_ibase.CommData();
            return Ok(result);
        }
    }
}
