using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfanityEdit.Models
{
    public class EditListItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("EditList")]
        public virtual int EditListId { get; set; }
        public virtual EditList EditList { get; set; }

        [Required]
        public float StartTime { get; set; }

        [Required]
        public float EndTime { get; set; }

        [Required]
        public string Context { get; set; }

        [ForeignKey("Profanity")]
        public virtual int ProfanityId { get; set; }
        public virtual Profanity Profanity { get; set; }
    }
}