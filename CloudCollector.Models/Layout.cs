using System;
using System.Collections.Generic;
using System.Text;

namespace CloudCollector.Models
{
    public class Layout
    {
        public int Piccount { get; set; }
        public int? Msgcount { get; set; }
        public int? Seecount { get; set; }
    }

    public class AboutMe
    {
        public string Name { get; set; }

        public string Sign { get; set; }

        public string HeadImg { get; set; }

        public string About { get; set; }
    }

    public class DetailM
    {
        public Cloud cloud { get; set; }

        public PaginatedList<Message> message { get; set; }
    }
}
