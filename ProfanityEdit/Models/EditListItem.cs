using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfanityEdit.Models
{
    public class EditListItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("EditList")]
        public virtual int EditListId { get; set; }
        public virtual EditList EditList { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public float StartTime { get; set; }

        [Required]
        public float EndTime { get; set; }

        public bool Audio { get; set; }

        public bool Video { get; set; }

        [ForeignKey("Profanity")]
        public virtual int? ProfanityId { get; set; }
        public virtual Profanity Profanity { get; set; }

        [ForeignKey("ObjectionableScene")]
        public virtual int? ObjectionableSceneId { get; set; }
        public virtual ObjectionableScene ObjectionableScene { get; set; }
    }
}