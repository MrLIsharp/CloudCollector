using System;
using System.Collections.Generic;
using System.Text;

namespace CloudCollector.Models
{
    public partial class Config
    {
        public int Id { get; set; }
        public string Keys { get; set; }
        public string Value { get; set; }
        public int CreatorId { get; set; }
        public DateTime? CreateDate { get; set; }
        public byte? Status { get; set; }
    }
}
