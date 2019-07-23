using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LinkAggregatorv5.Models
{
    public class LinkGenreViewModel
    {
        public List<Link> Links { get; set; }
        public SelectList Genres { get; set; }
        public string LinkGenre { get; set; }
        public string SearchString { get; set; }
    }
}