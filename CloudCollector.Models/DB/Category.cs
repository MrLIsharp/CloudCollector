using System;
using System.Collections.Generic;

#nullable disable

namespace CloudCollector.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Name { get; set; }

        public string Img { get; set; }
        public string Memo { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }
        public DateTime? CreateDate { get; set; }
        public byte? Status { get; set; }
        public decimal? OrderNo { get; set; }
    }
}
