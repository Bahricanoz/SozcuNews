namespace SozcuNews.UI.Entities
{
    public class NewsUrl
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Url { get; set; }
        public string? Title { get; set; }
        public DateTime CrawledDate { get; set; } = DateTime.UtcNow;
    }
}
