using System.Data.Entity;
using FAQ.Models;

namespace FAQ.DAL
{
    public class FAQContext : DbContext
    {
        public DbSet<FAQEntry> FAQs { get; set; }
    }
}