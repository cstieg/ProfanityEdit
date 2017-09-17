using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfanityEdit.Models
{
    public class Profanity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Word { get; set; }

        [ForeignKey("Category")]
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int Level { get; set; }
    }
}