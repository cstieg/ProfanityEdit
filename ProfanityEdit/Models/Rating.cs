using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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