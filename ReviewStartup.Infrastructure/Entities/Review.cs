using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewStartup.Infrastructure.Entities
{
    public class Review
    {
        [Key]

        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int MediaPostId { get; set; }
        [ForeignKey("MediaPostId")]
        public MediaPost MediaPost { get; set; }
        [Required(ErrorMessage = "Specify a summary fo media")]
        [MinLength(1, ErrorMessage = "Summary needs atleast 20 characters")]
        [MaxLength(150, ErrorMessage = "Summary be 150 characters maximum")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Specify a summary fo media")]
        [MinLength(1, ErrorMessage = "Summary needs atleast 20 characters")]
        [MaxLength(30, ErrorMessage = "Summary be 30 characters maximum")]
        public string Title { get; set; }
        [Range(1, 10)]
        public double Ratings { get; set; }
    }
}