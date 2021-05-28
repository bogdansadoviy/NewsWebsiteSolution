using Microsoft.AspNetCore.Http;
using NewsWebsite.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NewsWebsite.Models
{
    public class NewsWithImageViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Текст")]
        public string Content { get; set; }
        [DisplayName("Зображення")]
        public IFormFile File { get; set; }
        [DisplayName("Дата і час виходу")]
        public DateTime DateAdded { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public NewsWithImageViewModel()
        {
            Tags = new List<TagViewModel>();
        }

        public NewsViewModel ToNewsViewModel(string imagePath)
        {
            return new NewsViewModel()
            {
                Id = Id,
                Title = Title,
                Content = Content,
                TitleImagePath = imagePath,
                DateAdded = DateAdded == new DateTime() ? DateTime.Now : DateAdded,
                Tags = Tags
            };
        }
    }
}
