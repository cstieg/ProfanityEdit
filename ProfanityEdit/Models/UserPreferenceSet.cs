using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfanityEdit.Models
{
    public class UserPreferenceSet
    {
        [Key]
        public int Id { get; set; }

        [InverseProperty("UserPreferenceSet")]
        public virtual List<UserPreferenceItem> UserPreferenceItems { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public bool Preset { get; set; }

        public bool SkipAudio { get; set; }

        public bool SkipVideo { get; set; }

        public void InitializeUserPreferenceItems(List<Category> categories)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                if (UserPreferenceItems == null)
                {
                    UserPreferenceItems = new List<UserPreferenceItem>();
                }

                if (!UserPreferenceItems.Exists(u => u.CategoryId == categories[i].Id))
                {
                    UserPreferenceItem upi = new UserPreferenceItem()
                    {
                        Category = categories[i],
                        CategoryId = categories[i].Id
                    };
                    UserPreferenceItems.Add(upi);
                }
            }
        }
    }
}