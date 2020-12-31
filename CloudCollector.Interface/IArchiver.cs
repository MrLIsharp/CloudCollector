using CloudCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCollector.Interface
{
    public interface IArchiver
    {
        public Task<IEnumerable<Cloud>> All(int? count = null);

        public Task<PaginatedList<Cloud>> ToPageList(string searchText, int page, int pageSize);

        public Task<Cloud> Get(int id);

        public Task<string> Insert(Cloud cloud);

        public Task<string> Update(int cloud);
    }
}
