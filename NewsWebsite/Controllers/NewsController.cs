using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsWebsite.DataAccess;
using NewsWebsite.Models;
using NewsWebsite.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebsite.Controllers
{
    public class NewsController : Controller
    {
        private int _allowedSymbol = 280;

        private readonly ApplicationDbContext _context;
        private readonly TagService _tagService;

        public NewsController(ApplicationDbContext context, TagService tagService)
        {
            _tagService = tagService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new NewsIndexViewModel();
            var tenLatestNews = await GetTenLatestNews();
            vm.TenLatestNews = tenLatestNews.ToList();
            vm.FiveLatestNews = tenLatestNews.Take(5).ToList();
            vm.FiveTags = (await _tagService.GetTags()).Take(5).ToList();

            return View(vm);
        }

        public async Task<IActionResult> News(int newsId)
        {
            var vm = new SpecificNewsViewModel();
            vm.News = new NewsViewModel(await _context.News.FirstOrDefaultAsync(_ => _.Id == newsId));
            vm.TenLatestNews = await GetTenLatestNews();
            await _tagService.MapName(vm.News.Tags);
            vm.RelatedTags = vm.News.Tags.Where(_ => _.IsSelected).ToList();

            return View(vm);
        }
        
        public async Task<IActionResult> AllNews(int tagId)
        {
            var allNewsViewModel = await GetAllNews();
            if (tagId != default)
            {
                allNewsViewModel = allNewsViewModel.Where(_ => _.Tags.Any(_ => _.Id == tagId)).ToList();
            }

            foreach (var item in allNewsViewModel)
            {
                if (item.Content.Length > _allowedSymbol)
                {
                    item.Content = item.Content.Substring(0, _allowedSymbol) + " ...";
                }
            }

            return View(allNewsViewModel);
        }

        private async Task<List<NewsViewModel>> GetTenLatestNews()
        {
            return (await _context.News
                            .OrderBy(_ => _.DateAdded)
                            .Take(10)
                            .ToListAsync()).Select(_ => new NewsViewModel(_)).ToList();
        }

        private async Task<List<NewsViewModel>> GetAllNews()
        {
            return (await _context.News
                            .OrderBy(_ => _.DateAdded)
                            .ToListAsync()).Select(_ => new NewsViewModel(_)).ToList();
        }
    }
}
