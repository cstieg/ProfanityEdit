using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfanityEdit.Models
{
    public class UserPreferenceItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int AllowLevel { get; set; }
    }
}