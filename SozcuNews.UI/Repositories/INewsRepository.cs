using SozcuNews.UI.Entities;

public interface INewsRepository
{
    Task IndexNewsAsync(NewsUrl news);
    Task<IEnumerable<NewsUrl>> SearchNewsAsync(string query);

    Task<IEnumerable<NewsUrl>> SearchNewsAsync();
}