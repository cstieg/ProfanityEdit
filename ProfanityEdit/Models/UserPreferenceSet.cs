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
        public virtual List<ApplicationUser> Owner { get; set; }

        [InverseProperty("SelectedPreferenceSet")]
        public virtual List<ApplicationUser> Users { get; set; }

        [InverseProperty("UserPreferenceSet")]
        public virtual List<UserPreferenceItem> UserPreferenceItems { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public bool Preset { get; set; }

        public bool SkipAudio { get; set; }

        public bool SkipVideo { get; set; }

        public UserPreferenceSet() { }

        public UserPreferenceSet(List<Category> categories)
        {
            InitializeUserPreferenceItems(categories);
        }

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

        public void CopyUserPreferenceItems(UserPreferenceSet preferenceSet)
        {
            for (int i = 0; i < UserPreferenceItems.Count; i++)
            {
                var pSetPreferenceItem = preferenceSet.UserPreferenceItems.Find(p => p.CategoryId == UserPreferenceItems[i].CategoryId);
                pSetPreferenceItem.AllowLevel = UserPreferenceItems[i].AllowLevel;
            }
        }

        public void CopyUserPreferenceSet(UserPreferenceSet preferenceSet)
        {
            SkipAudio = preferenceSet.SkipAudio;
            SkipVideo = preferenceSet.SkipVideo;
            CopyUserPreferenceItems(preferenceSet);
        }
    }
}