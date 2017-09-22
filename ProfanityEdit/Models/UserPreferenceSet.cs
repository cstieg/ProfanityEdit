using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfanityEdit.Models
{
    public class UserPreferenceSet
    {
        [Key]
        public int Id { get; set; }

        public List<UserPreferenceItem> UserPreferenceItems { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public bool Preset { get; set; }

        public bool SkipAudio { get; set; }

        public bool SkipVideo { get; set; }
    }
}