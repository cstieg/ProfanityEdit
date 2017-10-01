using System.Collections.Generic;

namespace ProfanityEdit.Models
{
    public class MakeXspfViewModel
    {
        public int UserPreferenceSetId { get; set; }

        public virtual List<UserPreferenceItem> UserPreferenceItems { get; set; }

        public bool SkipAudio { get; set; }

        public bool SkipVideo { get; set; }

        public int EditListId { get; set; }

    }
}