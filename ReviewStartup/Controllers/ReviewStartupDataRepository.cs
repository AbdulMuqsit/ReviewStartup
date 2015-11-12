using ReviewStartup.EntityFramework;
using ReviewStartup.Infrastructure.Entities;

namespace ReviewStartup.Controllers
{
    class ReviewStartupDataRepository : IReviewStartupDataRepository
    {

        public ReviewStartupDataRepository(ReviewsStartupDbContext context)
        {

            MediaPosts = new Repository<MediaPost>(context);
        }

        public IRepository<MediaPost> MediaPosts { get; set; }

    }
}