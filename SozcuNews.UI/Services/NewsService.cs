using HtmlAgilityPack;
using SozcuNews.UI.Entities;
using SozcuNews.UI.Repositories;

namespace SozcuNews.UI.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task AddNewsUrl()
        {
            var web = new HtmlWeb();
            var document = web.Load("https://www.sozcu.com.tr/");
            Console.WriteLine("Sözcü sayfası başarıyla yüklendi");

            var links = document.DocumentNode.SelectNodes("//a[@href]");

            if (links != null)
            {
                foreach (var link in links)
                {
                    var url = link.GetAttributeValue("href", string.Empty);
                    var newUrl = url.StartsWith("http") ? url : "https://www.sozcu.com.tr" + url;

                    if (newUrl != null && newUrl.Contains("www.sozcu.com.tr"))
                    {
                        var documentNewUrl = web.Load(newUrl);
                        var title = documentNewUrl.DocumentNode.SelectSingleNode("//title")?.InnerText;

                        var newsUrl = new NewsUrl
                        {
                            Url = newUrl,
                            Title = title,
                        };

                        await newsRepository.IndexNewsAsync(newsUrl);
                    }
                }
            }
        }

        public async Task<IEnumerable<NewsUrl>> SearchNewsAsync(string query)
        {
            return await newsRepository.SearchNewsAsync(query);
        }

        public async Task<IEnumerable<NewsUrl>> SearchNewsAsync()
        {
            return await newsRepository.SearchNewsAsync();
        }
    }
}
