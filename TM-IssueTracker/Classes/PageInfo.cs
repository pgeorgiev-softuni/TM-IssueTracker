using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TM_IssueTracker.Classes
{
    public class PageInfo
    {
        public int? State { get; set; }
        public string Search { get; set; }
        public int? Page { get; set; }
    }
}