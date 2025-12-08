using System;
using System.Collections.Generic;
using System.Linq;
using Facebook;
using FacebookWinFormsApp.Logic;
using FacebookWrapper.ObjectModel;

namespace FacebookMini.Logic
{
    internal sealed class AnalyticsManager
    {
        private readonly AnalyticsSnapshot r_AnalyticsSnapshot = new AnalyticsSnapshot();
        private static readonly Random sr_Random = new Random();

        public AnalyticsSnapshot AnalyticsSnapshot
        {
            get { return r_AnalyticsSnapshot; }
        }

        public AnalyticsManager(List<Post> i_Posts, List<Photo> i_Photos)
        {
            Dictionary<User, int> likerCounts = new Dictionary<User, int>();
            Dictionary<User, int> commenterCounts = new Dictionary<User, int>();
            Dictionary<DayOfWeek, int> dayCounter = new Dictionary<DayOfWeek, int>();
            Dictionary<int, int> hourCounter = new Dictionary<int, int>();

            //calculatePostsAnalytics(i_Posts, likerCounts, commenterCounts, dayCounter, hourCounter);
            //calculatePhotos(i_Photos, likerCounts, commenterCounts, dayCounter, hourCounter);

            //r_AnalyticsSnapshot.TopLiker = selectMaxByValue(likerCounts);
            //r_AnalyticsSnapshot.TopCommenter = selectMaxByValue(commenterCounts);
            //r_AnalyticsSnapshot.FavoritePostingDayOfWeek = maxKey(dayCounter);
            //r_AnalyticsSnapshot.FavoritePostingHour = maxKey(hourCounter);
        }
        
        //private void calculatePostsAnalytics(List<Post> i_Posts, Dictionary<User, int> i_LikerCounts,
        //                                     Dictionary<User, int> i_CommenterCounts, Dictionary<DayOfWeek, int> i_DayCounter,
        //                                     Dictionary<int, int> i_HourCounter)
        //{
        //    int maximumLikes = -1;
        //    int maximumComments = -1;

        //    foreach (Post currentPost in i_Posts)
        //    {
        //        int likesCount;

        //        try
        //        {
        //            likesCount = currentPost.LikedBy != null ? currentPost.LikedBy.Count : 0;
        //        }
        //        catch (FacebookOAuthException)
        //        {
        //            likesCount = sr_Random.Next(0, 150);
        //        }

        //        if (likesCount > maximumLikes)
        //        {
        //            maximumLikes = likesCount;
        //            r_AnalyticsSnapshot.MostLikedPost = currentPost;
        //        }

        //        try
        //        {
        //            foreach(User currentUser in currentPost.LikedBy)
        //            {
        //                addOneToCountForUser(i_LikerCounts, currentUser);
        //            }
        //        }
        //        catch
        //        {
        //            //TODO
        //        }

        //        int commentsCount = currentPost.Comments.Count;
        //        if (commentsCount > maximumComments)
        //        {
        //            maximumComments = commentsCount;
        //            r_AnalyticsSnapshot.MostCommentedPost = currentPost;
        //        }

        //        foreach (Comment currentComment in currentPost.Comments)
        //        {
        //            if (currentComment.From != null)
        //            {
        //                addOneToCountForUser(i_CommenterCounts, currentComment.From);
        //            }
        //        }

        //        DateTime? createdTime = currentPost.CreatedTime;
        //        if (createdTime.HasValue)
        //        {
        //            DayOfWeek day = createdTime.Value.DayOfWeek;
        //            int hour = createdTime.Value.Hour;

        //            if (i_DayCounter.ContainsKey(day) == false)
        //            {
        //                i_DayCounter[day] = 0;
        //            }
        //            i_DayCounter[day]++;

        //            if (i_HourCounter.ContainsKey(hour) == false)
        //            {
        //                i_HourCounter[hour] = 0;
        //            }
        //            i_HourCounter[hour]++;
        //        }
        //    }
        //}

        //private void calculatePhotos(List<Photo> i_Photos, Dictionary<User, int> i_LikerCounts, Dictionary<User, int> i_CommenterCounts, Dictionary<DayOfWeek, int> i_DayCounter, Dictionary<int, int> i_HourCounter)
        //{
        //    int maximumLikes = -1;
        //    int maximumComments = -1;

        //    foreach (Photo currentPhoto in i_Photos)
        //    {
        //        int likesCount = currentPhoto.LikedBy.Count;
        //        if (likesCount > maximumLikes)
        //        {
        //            maximumLikes = likesCount;
        //            r_AnalyticsSnapshot.MostLikedPhoto = currentPhoto;
        //        }

        //        try
        //        {
        //            foreach (User currentUser in currentPhoto.LikedBy)
        //            {
        //                addOneToCountForUser(i_LikerCounts, currentUser);
        //            }
        //        }
        //        catch
        //        {
        //            //TODO
        //        }

        //        int commentsCount = currentPhoto.Comments.Count;
        //        if (commentsCount > maximumComments)
        //        {
        //            maximumComments = commentsCount;
        //            r_AnalyticsSnapshot.MostCommentedPhoto = currentPhoto;
        //        }

        //        foreach (Comment currentComment in currentPhoto.Comments)
        //        {
        //            addOneToCountForUser(i_CommenterCounts, currentComment.From);
        //        }

        //        DateTime? createdTime = currentPhoto.CreatedTime;
        //        if (createdTime.HasValue)
        //        {
        //            DayOfWeek day = createdTime.Value.DayOfWeek;
        //            int hour = createdTime.Value.Hour;

        //            if (!i_DayCounter.ContainsKey(day))
        //            {
        //                i_DayCounter[day] = 0;
        //            }
        //            i_DayCounter[day]++;

        //            if (!i_HourCounter.ContainsKey(hour))
        //            {
        //                i_HourCounter[hour] = 0;
        //            }
        //            i_HourCounter[hour]++;
        //        }
        //    }
        //}
        //private void addOneToCountForUser(Dictionary<User, int> i_Map, User i_User)
        //{
        //    if (i_Map.TryGetValue(i_User, out int current))
        //    {
        //        i_Map[i_User] = current + 1;
        //    }
        //    else
        //    {
        //        i_Map[i_User] = 1;
        //    }
        //}
        //private TKey maxKey<TKey>(Dictionary<TKey, int> i_Map)
        //{
        //    KeyValuePair<TKey, int> best = default;
        //    bool first = true;

        //    foreach (KeyValuePair<TKey, int> pair in i_Map)
        //    {
        //        if (first || pair.Value > best.Value)
        //        {
        //            best = pair;
        //            first = false;
        //        }
        //    }

        //    return best.Key;
        //}
        //private User selectMaxByValue(Dictionary<User, int> i_Map)
        //{
        //    if (i_Map.Count == 0)
        //    {
        //        return null;
        //    }

        //    KeyValuePair<User, int> best = default;
        //    bool first = true;

        //    foreach (KeyValuePair<User, int> pair in i_Map)
        //    {
        //        if (first || pair.Value > best.Value)
        //        {
        //            best = pair;
        //            first = false;
        //        }
        //    }

        //    return best.Key;
        //}
    }
}

