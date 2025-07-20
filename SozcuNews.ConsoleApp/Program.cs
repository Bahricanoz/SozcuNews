// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;
using Nest;
using SozcuNews.ConsoleApp;

Console.WriteLine("Merhaba Dünya");

var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
    .DefaultIndex("sozcu-news");

var client = new ElasticClient(settings);



var web = new HtmlWeb();
var document = web.Load("https://www.sozcu.com.tr/");
Console.WriteLine("Sözcü sayfası başarıyla yüklendi");

var links = document.DocumentNode.SelectNodes("//a[@href]");

if(links != null)
{
    foreach (var link in links)
    {
        var url = link.GetAttributeValue("href", string.Empty);
        var newUrl = url.StartsWith("http") ? url : "https://www.sozcu.com.tr" + url;


        if (newUrl != null && newUrl.Contains("www.sozcu.com.tr"))
        {
            var documentNewUrl = web.Load(newUrl);
            var title = documentNewUrl.DocumentNode.SelectSingleNode("//title")?.InnerText;
            Console.WriteLine($"title : {newUrl} --- {title}");
            var newsUrl = new NewsUrl { 
                Url = newUrl,
                Title = title,
            };
            var indexresponse = client.IndexDocument(newsUrl);
            if (indexresponse.IsValid)
            {
                Console.WriteLine($"url başarıyla eklendi: {newsUrl.Url}");
            }
            else
            {
                Console.WriteLine($"url eklenirken hata oluştu: {indexresponse.OriginalException.Message}");
            }

        }

    }

}
else
{
    Console.WriteLine("Link bulunamadı");
};