using NewsWebsite.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebsite.Models
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public TagViewModel()
        {

        }

        public TagViewModel(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
        }
    }
}
