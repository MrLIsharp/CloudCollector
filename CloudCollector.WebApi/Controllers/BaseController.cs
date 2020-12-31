using CloudCollector.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudCollector.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IBase _ibase;

        public BaseController(IBase ibase)
        {
            _ibase = ibase;

        }
        public dynamic Get()
        {
            var result=_ibase.CommData();
            return Ok(result);
        }
    }
}
