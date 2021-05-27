using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.DataAccess;
using NewsWebsite.Models;
using NewsWebsite.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebsite.Controllers
{
    [Authorize]
    public class AdminPanelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly TagService _tagService;

        public AdminPanelController(ApplicationDbContext context, TagService tagService)
        {
            _tagService = tagService;
            _context = context;
        }

        // GET: AdminPanel
        public async Task<IActionResult> Index()
        {
            var news = await _context.News.ToListAsync();
            var newsViewModels = news.Select(_ => new NewsViewModel(_)).ToList();
            await MapTags(newsViewModels);

            return View(newsViewModels);
        }

        // GET: AdminPanel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            var vm = new NewsViewModel(news);
            await _tagService.MapName(vm.Tags);

            return View(vm);
        }

        // GET: AdminPanel/Create
        public async Task<IActionResult> Create()
        {
            var vm = new NewsViewModel();
            await _tagService.MapName(vm.Tags);

            return View(vm);
        }

        // POST: AdminPanel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,TitleImagePath,DateAdded,TagIds")] NewsViewModel news)
        {
            if (ModelState.IsValid)
            {
                _context.Add(news.ToEntity());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: AdminPanel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            var vm = new NewsViewModel(news);
            await _tagService.MapName(vm.Tags);

            return View(vm);
        }

        // POST: AdminPanel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,TitleImagePath,DateAdded,Tags")] NewsViewModel news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news.ToEntity());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // POST: AdminPanel/DeleteConfirmed/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }

        private async Task MapTags(List<NewsViewModel> newsViewModels)
        {
            foreach (var newsViewModel in newsViewModels)
            {
                await _tagService.MapName(newsViewModel.Tags);
            }
        }
    }
}
