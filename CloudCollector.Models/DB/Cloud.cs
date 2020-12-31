using System;
using System.Collections.Generic;

#nullable disable

namespace CloudCollector.Models
{
    public partial class Cloud
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Memo { get; set; }
        public int? TypeId { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }
        public DateTime? CreateDate { get; set; }

        public int? SeeCount { get; set; }
        public string Date { 
            get{
                if (CreateDate.HasValue)
                    return CreateDate.Value.ToString("yyyy-MM-dd");
                else return string.Empty;
                } 
        }
        public byte? Status { get; set; }
        public decimal? OrderNo { get; set; }
        public string Pic { get; set; }
    }
}
