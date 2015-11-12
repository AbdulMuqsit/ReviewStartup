using System.Collections.Generic;
using ReviewStartup.Infrastructure.Entities;

namespace ReviewStartup.Controllers
{
    public class SearchViewMode
    {
        public List<MediaPost> MediaPosts { get; set; }
        public List<User> Users { get; set; }
    }
}