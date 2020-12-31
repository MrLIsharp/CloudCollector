using CloudCollector.Interface;
using CloudCollector.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCollector.Service
{
    public  class BaseServer:IBase
    {
        private readonly CloudCollectorContext _context;

        public BaseServer(CloudCollectorContext context)
        {
            _context = context;
        }
        public Layout CommData()
        {
            Layout layout = new Layout();
            layout.Msgcount = _context.Messages.Where(t => t.Status == 1).Count();
            layout.Piccount = _context.Clouds.Count();
            layout.Seecount = _context.Clouds.Sum(t => t.SeeCount);
            return layout;
        }
    }
}
