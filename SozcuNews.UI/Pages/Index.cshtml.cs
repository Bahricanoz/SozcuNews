using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SozcuNews.UI.Services;
using SozcuNews.UI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SozcuNews.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INewsService _newsService;

        public List<NewsUrl> NewsList { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Query { get; set; }

        public IndexModel(ILogger<IndexModel> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrWhiteSpace(Query))
            {
                NewsList = (await _newsService.SearchNewsAsync()).ToList();
            }
            else
            {
                NewsList = (await _newsService.SearchNewsAsync(Query)).ToList();
            }
        }
    }
}
