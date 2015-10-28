using System.ComponentModel.DataAnnotations;

namespace ReviewStartup.Infrastructure.Entities
{
    public enum MediaType
    {
        [Display(Name = "Movie")]
        Movie,
        [Display(Name = "Video Game")]
        VideoGame,
        [Display(Name = "Tv Show")]
        TvShow,
        [Display(Name = "Music")]
        Music
    }
}