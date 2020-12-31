using CloudCollector.Interface;
using CloudCollector.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace CloudCollector.Service
{
    public class ArchiverServer : IArchiver
    {
        private readonly CloudCollectorContext _context;

        public ArchiverServer(CloudCollectorContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cloud>> All(int? count = null)
        {
            var list = _context.Clouds.Where(t => t.Status == 1);
            return count == null ? await list.ToListAsync() : await list.Take(count.Value).ToListAsync();
        }

        public async Task<Cloud> Get(int id)
        {
            var result = _context.Clouds.Where(t => t.Status == 1 && t.Id == id).FirstOrDefaultAsync();
            return await result;
        }

        public async Task<string> Insert(Cloud cloud)
        {

            if (cloud.CreatorName == null || cloud.CreatorName == "")
                return "用户名不能为空";
            if (cloud.Memo == null || cloud.Memo == "")
                return "内容不能为空";
            if (cloud.Pic == null || cloud.Pic == "")
                return "云彩图片不能为空";
            _context.Clouds.Add(new Cloud
            {
                CreatorName = cloud.CreatorName,
                Memo = cloud.Memo,
                Pic = cloud.Pic,
                CreatorId = new Random().Next(0, 1000),
                Status = 1,
                Name = cloud.Name,
                TypeId = cloud.TypeId
            });
            await _context.SaveChangesAsync();
            return "Success";

        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<PaginatedList<Cloud>> ToPageList(string searchText, int page, int pageSize)
        {

            var list = _context.Clouds.Where(t => (searchText != null && searchText != "") ? t.Name.Contains(searchText) : t.Status == 1);
            //return await list.AsNoTracking().ToPagedListAsync(page, pageSize);
            var result = PaginatedList<Cloud>.CreateAsync(list.AsNoTracking(), page, pageSize);
            return await result;
        }

        public async Task<string> Update(int cloud)
        {

            var result = _context.Clouds.FirstOrDefault(t => t.Id == cloud);
            if (result == null)
            {
                return "Error";
            }
            result.SeeCount = result.SeeCount.HasValue ? result.SeeCount.Value + 1 : 1;
            await _context.SaveChangesAsync();
            return "Success";

        }
    }
}
