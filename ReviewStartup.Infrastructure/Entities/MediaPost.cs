using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewStartup.Infrastructure.Entities
{
    public class MediaPost
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MinLength(1, ErrorMessage = "Atleast 1 characters is required for Title")]
        [MaxLength(40, ErrorMessage = "Tiltle can be 40 characters maximum")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Specify your media type")]
        public MediaType? Type { get; set; }

        [Required(ErrorMessage = "Specify a summary fo media")]
        [MinLength(20, ErrorMessage = "Summary needs atleast 20 characters")]
        [MaxLength(150, ErrorMessage = "Summary be 150 characters maximum")]
        public string Summary { get; set; }

      

        [ForeignKey("UserId")]
        public User User { get; set; }

        public byte[] Thumbnail { get; set; }
        [Range(0.0,10.0)]
        public double AverageScore { get; set; }

        public string UserId { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}