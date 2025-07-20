using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozcuNews.ConsoleApp
{
    public class NewsUrl
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Url { get; set; }
        public string? Title { get; set; }
        public DateTime CrawledDate { get; set; } = DateTime.Now;
    }
}
