using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfanityEdit.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [Display(Name = "Run Time", Description = "Run time in minutes")]
        public int RunTime { get; set; }

        [ForeignKey("Rating")]
        public virtual int? RatingId { get; set; }
        public virtual Rating Rating { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1048576)]
        public string SubtitleText { get; set; }

        [InverseProperty("Movie")]
        public virtual List<EditList> EditLists { get; set; }
    }
}