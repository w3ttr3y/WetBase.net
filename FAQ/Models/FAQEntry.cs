using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAQ.Models
{
    public class FAQEntry
    {
        public int FAQEntryId { get; set; }
        public string Question { get; set; }

        [UIHint("MultiLineText")]
        public string Answer { get; set; }
    }
}