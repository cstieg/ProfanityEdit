using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfanityEdit.Models
{
    public class EditList
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Movie")]
        public virtual int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [ForeignKey("Editor")]
        public string EditorId { get; set; }
        public virtual ApplicationUser Editor { get; set; }

        public DateTime EditDate { get; set; }

        [ForeignKey("GenerateMethod")]
        public virtual int GenerateMethodId { get; set; }
        public virtual GenerateMethod GenerateMethod { get; set; }
    }
}