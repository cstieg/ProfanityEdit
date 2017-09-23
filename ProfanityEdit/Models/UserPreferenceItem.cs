using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfanityEdit.Models
{
    public class UserPreferenceItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserPreferenceSet")]
        public virtual int UserPreferenceSetId { get; set; }
        public virtual UserPreferenceSet UserPreferenceSet { get; set; }

        [ForeignKey("Category")]
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Range(0, 10)]
        public int AllowLevel { get; set; }
    }
}