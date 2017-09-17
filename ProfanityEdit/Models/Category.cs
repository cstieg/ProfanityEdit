using System.ComponentModel.DataAnnotations;

namespace ProfanityEdit.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}