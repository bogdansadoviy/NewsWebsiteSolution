using NewsWebsite.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NewsWebsite.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string TitleImagePath { get; set; }
        public DateTime DateAdded { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public NewsViewModel()
        {
            Tags = new List<TagViewModel>();
        }

        public NewsViewModel(News news)
        {
            Id = news.Id;
            Title = news.Title;
            Content = news.Content;
            TitleImagePath = news.TitleImagePath;
            DateAdded = news.DateAdded;
            if (!string.IsNullOrEmpty(news.TagIds))
            {
                Tags = news.TagIds
                .Split(',')
                .Select(n => new TagViewModel()
                {
                    Id = Convert.ToInt32(n),
                    IsSelected = true
                })
                .ToList();
            }
            else
            {
                Tags = new List<TagViewModel>();
            }
        }

        public News ToEntity()
        {
            return new News()
            {
                Id = Id,
                Title = Title,
                Content = Content,
                TitleImagePath = TitleImagePath,
                DateAdded = DateAdded == new DateTime() ? DateTime.Now : DateAdded,
                TagIds = string.Join(',', Tags.Where(_ => _.IsSelected).Select(_ => _.Id))
            };
        }
    }
}
