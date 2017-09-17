using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProfanityEdit.Models
{
    public class Profanity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Word { get; set; }

        public virtual int CategoryId { get; set; }
        public Category Category { get; set; }

        public int Level { get; set; }
    }
}