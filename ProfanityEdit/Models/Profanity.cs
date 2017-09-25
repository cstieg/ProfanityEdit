using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfanityEdit.Models
{
    public class Profanity : IObjectionable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Word { get; set; }

        [ForeignKey("Category")]
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int Level { get; set; }

        [Display(Description = "Check if the subtitle processor should ask if this word is profanity in its context")]
        public bool Ask { get; set; }
    }
}