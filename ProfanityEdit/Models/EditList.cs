﻿using ProfanityEdit.Modules.Srt;
using StringHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfanityEdit.Models
{
    public class EditList
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Movie")]
        public virtual int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [ForeignKey("Editor")]
        public string EditorId { get; set; }
        public virtual ApplicationUser Editor { get; set; }

        public DateTime EditDate { get; set; }

        public int GenerateMethod { get; set; }
        
        [InverseProperty("EditList")]
        public virtual List<EditListItem> EditListItems { get; set; }
        
        public EditList()
        {
            EditDate = DateTime.Now;
            EditListItems = new List<EditListItem>();
            GenerateMethod = (int)(GenerateMethodEnum.AutogeneratedProfanityEdit);
        }

        public EditList(Movie movie, Srt srt, List<Profanity> profanities)
        {
            EditDate = DateTime.Now;
            EditListItems = new List<EditListItem>();
            GenerateMethod = (int)(GenerateMethodEnum.AutogeneratedProfanityEdit);
            Movie = movie;
            MovieId = movie.Id;

            for (int i = 0; i < profanities.Count; i++)
            {
                var profanity = profanities[i];

                for (int j = 0; j < srt.SrtLines.Count; j++)
                {
                    var srtLine = srt.SrtLines[j];

                    var matches = srtLine.Text.FindWildcardMatches(profanity.Word);
                    for (int k = 0; k < matches.Count; k++)
                    {
                        var match = matches[k];

                        // add new editlistitem
                        var editListItem = new EditListItem()
                        {
                            StartTime = srtLine.StartTime,
                            EndTime = srtLine.EndTime,
                            Audio = true,
                            Video = false,
                            Profanity = profanity,
                            ProfanityId = profanity.Id,
                            Description = srtLine.Text
                        };
                        EditListItems.Add(editListItem);
                    }
                }

            }
        }

        public bool Equals(EditList editList)
        {
            if (EditListItems.Count != editList.EditListItems.Count)
            {
                return false;
            }
            for (int i = 0; i < EditListItems.Count; i++)
            {
                if (!EditListItems[i].Equals(editList.EditListItems[i]))
                {
                    return false;
                }
            }
            return Name == editList.Name &&
                MovieId == editList.MovieId &&
                GenerateMethod == editList.GenerateMethod;

        }


    }

}