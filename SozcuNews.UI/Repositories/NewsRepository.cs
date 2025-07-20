using Nest;
using SozcuNews.UI.Entities;

namespace SozcuNews.UI.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly ElasticClient _client;

        public NewsRepository()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("sozcu-news");

            _client = new ElasticClient(settings);
        }

        public async Task IndexNewsAsync(NewsUrl news)
        {
            var response = await _client.IndexDocumentAsync(news);

            if (!response.IsValid)
            {
                // Hataları loglamak ya da fırlatmak tercih edilebilir
                throw new Exception($"Elasticsearch indexleme hatası: {response.ServerError?.Error.Reason}");
            }
        }

        public async Task<IEnumerable<NewsUrl>> SearchNewsAsync(string query)
        {
            var response = await _client.SearchAsync<NewsUrl>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Title)
                        .Query(query)
                    )
                )
            );

            if (!response.IsValid)
            {
                throw new Exception($"Elasticsearch arama hatası: {response.ServerError?.Error.Reason}");
            }

            return response.Documents;
        }

        public async Task<IEnumerable<NewsUrl>> SearchNewsAsync()
        {
            var response = await _client.SearchAsync<NewsUrl>(s => s
                .Query(q => q.MatchAll())
                .Size(100) // İstersen sınır koy
            );

            if (!response.IsValid)
            {
                throw new Exception($"Elasticsearch tümünü getirme hatası: {response.ServerError?.Error.Reason}");
            }

            return response.Documents;
        }
    }
}
