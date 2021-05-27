using System.Collections.Generic;

namespace NewsWebsite.Models
{
    public class NewsIndexViewModel
    {
        public List<NewsViewModel> FiveLatestNews { get; set; }
        public List<NewsViewModel> TenLatestNews { get; set; }
    }
}
