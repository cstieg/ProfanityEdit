using System.ComponentModel.DataAnnotations;

namespace ProfanityEdit.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string Name { get; set; }

        public int? AgeRating { get; set; }
    }
}