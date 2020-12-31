using System;
using System.Collections.Generic;

#nullable disable

namespace CloudCollector.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Context { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }
        public DateTime? CreateDate { get; set; }
        public byte? Status { get; set; }
        public string HeadImg { get; set; }
        public int? CloudId { get; set; }
    }
}
