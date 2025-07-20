using SozcuNews.UI.Entities;

namespace SozcuNews.UI.Services
{
    public interface INewsService
    {
        Task AddNewsUrl();

        Task<IEnumerable<NewsUrl>> SearchNewsAsync(string query);

        Task<IEnumerable<NewsUrl>> SearchNewsAsync();

    }
}
