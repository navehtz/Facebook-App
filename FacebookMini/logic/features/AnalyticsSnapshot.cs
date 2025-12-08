using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookWinFormsApp.Logic
{
    internal sealed class AnalyticsSnapshot
    {
        public Post MostLikedPost {  get; set; }
        public Photo MostLikedPhoto {  get; set; }
        public Post MostCommentedPost {  get; set; }
        public Photo MostCommentedPhoto {  get; set; }
        public User TopLiker {  get; set; }
        public User TopCommenter {  get; set; }
        public DayOfWeek FavoritePostingDayOfWeek {  get; set; }
        public int FavoritePostingHour {  get; set; }
    }
}
