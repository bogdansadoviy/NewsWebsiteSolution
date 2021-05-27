using Microsoft.EntityFrameworkCore;
using NewsWebsite.DataAccess;
using NewsWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebsite.Services
{
    public class TagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TagViewModel>> GetTags()
        {
            var tags = await _context.Tags.ToListAsync();
            return tags.Select(_ => new TagViewModel(_)).ToList();
        }

        public async Task MapName(List<TagViewModel> tagViewModels)
        {
            var tags = await _context.Tags.ToListAsync();
            foreach (var tag in tags)
            {
                var tagViewModel = tagViewModels.FirstOrDefault(_ => _.Id == tag.Id);
                if (tagViewModel == null)
                {
                    tagViewModels.Add(new TagViewModel(tag));
                    continue;
                }

                tagViewModel.Name = tag.Name;
            }
        }
    }
}
