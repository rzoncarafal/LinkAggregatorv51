using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LinkAggregatorv5.Models
{
    public class UserGenreViewModel
    {
        public List<User> Users { get; set; }
        public SelectList Genres { get; set; }
        public string UserGenre { get; set; }
        public string SearchString { get; set; }
    }
}