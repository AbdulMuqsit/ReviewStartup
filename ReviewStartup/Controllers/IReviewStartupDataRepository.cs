using ReviewStartup.Infrastructure.Entities;

namespace ReviewStartup.Controllers
{
    public interface IReviewStartupDataRepository
    {
        IRepository<MediaPost> MediaPosts { get; set; }
    }
}