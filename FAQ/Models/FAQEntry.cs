using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAQ.Models
{
    public class FAQEntry
    {
        public int FAQEntryId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}