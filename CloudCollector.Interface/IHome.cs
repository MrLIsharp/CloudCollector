using CloudCollector.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudCollector.Interface
{
    public interface ICateGoryService
    {
        //public Task<IEnumerable<Category>> All();

        public Task<IEnumerable<Category>> All();

        public Task<string> Insert(Message message);

        public Task<PaginatedList<Message>> ToPageList(int page, int pageSize,int? CloudId);

        public Task<IEnumerable<Config>> Configs(string[] keys);
        public Task<AboutMe> Aboutme();
    }
}
