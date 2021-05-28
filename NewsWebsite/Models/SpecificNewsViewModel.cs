using System.Collections.Generic;

namespace NewsWebsite.Models
{
    public class SpecificNewsViewModel
    {
        public List<NewsViewModel> TenLatestNews { get; set; }
        public List<TagViewModel> RelatedTags { get; set; }
        public NewsViewModel News { get; set; }
    }
}
