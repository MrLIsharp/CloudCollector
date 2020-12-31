using CloudCollector.Models;
using System;
using System.Linq;
using CloudCollector.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CloudCollector.Service
{
    public class Category : ICateGoryService
    {
        private readonly CloudCollectorContext _context;

        public Category(CloudCollectorContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Models.Category>> All()
        {
            var list = _context.Categories.Where(t => t.Pid == 0);
            return await list.ToListAsync();
            //using (var db = new CloudCollectorContext())
            //{
            //    var list = db.Categories.Where(t => t.Pid == 0);
            //    return list.ToList();
            //}
        }

        public async Task<string> Insert(Message message)
        {

            if (message.CreatorName == null || message.CreatorName == "")
                return "用户名不能为空";
            if (message.Context == null || message.Context == "")
                return "内容不能为空";
            _context.Messages.Add(new Message
            {
                CreatorName = message.CreatorName,
                Context = message.Context,
                HeadImg = "https://localhost:44376/images/avatar.png",
                CreatorId = new Random().Next(0, 1000),
                Status = 1,
                CloudId = message.CloudId,
            });
            await _context.SaveChangesAsync();
            return "Success";

        }

        public async Task<PaginatedList<Message>> ToPageList(int page, int pageSize, int? CloudId)
        {

            var list = _context.Messages.Where(t => t.Status == 1 && (CloudId.HasValue ? t.CloudId == CloudId : t.CloudId == null));
            var result = PaginatedList<Message>.CreateAsync(list.AsNoTracking(), page, pageSize);
            return await result;

        }

       

        public async Task<IEnumerable<Config>> Configs(string[] keys)
        {

            var list = _context.Configs.Where(t => keys.Contains(t.Keys)).ToListAsync();
            return await list;

        }

        public async Task<AboutMe> Aboutme()
        {
            string[] keys = { "Name", "Sign", "HeadImg", "About" };
            var list = await Configs(keys);
            var result = new AboutMe
            {
                Name = list.FirstOrDefault(t => t.Keys == "Name").Value,
                Sign = list.FirstOrDefault(t => t.Keys == "Sign").Value,
                HeadImg = list.FirstOrDefault(t => t.Keys == "HeadImg").Value,
                About = list.FirstOrDefault(t => t.Keys == "About").Value,
            };
            return result;
        }

    }
}
