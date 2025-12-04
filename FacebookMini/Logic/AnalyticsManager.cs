using System;
using System.Collections.Generic;
using System.Linq;
using FacebookWrapper.ObjectModel;

namespace FacebookMini.Logic
{
    internal class AnalyticsManager
    {
        private readonly User r_LoggedInUser;

        // Stored analytics
        public List<Post> PostsByLikes { get; private set; } = new List<Post>();
        public List<Photo> PhotosByLikes { get; private set; } = new List<Photo>();
        public Post MostLikedPost { get; private set; }
        public Photo MostLikedPhoto { get; private set; }
        public Post LatestPostLikedByUser { get; private set; }
        public Photo LatestPhotoLikedByUser { get; private set; }
        public AnalyticsManager(User i_LoggedInUser)
        {
            r_LoggedInUser = i_LoggedInUser;

            BuildAnalytics();
        }

        private void BuildAnalytics()
        {
            // Build posts list
            PostsByLikes = GetAllPosts()
                .OrderByDescending(p => SafeLikes(p))
                .ToList();

            MostLikedPost = PostsByLikes.FirstOrDefault();

            // Build photos list
            PhotosByLikes = GetAllPhotos()
                .OrderByDescending(ph => SafeLikes(ph))
                .ToList();

            MostLikedPhoto = PhotosByLikes.FirstOrDefault();

            // Find "last post the user liked"
            LatestPostLikedByUser = GetAllPosts()
                .Where(p => UserLiked(p))
                .OrderByDescending(p => SafeCreated(p))
                .FirstOrDefault();

            // Find "last photo the user liked"
            LatestPhotoLikedByUser = GetAllPhotos()
                .Where(ph => UserLiked(ph))
                .OrderByDescending(ph => SafeCreated(ph))
                .FirstOrDefault();
        }

        private List<Post> GetAllPosts()
        {
            var result = new List<Post>();

            foreach (var post in r_LoggedInUser.Posts)
            {
                if (post != null)
                    result.Add(post);
            }

            return result;
        }

        private List<Photo> GetAllPhotos()
        {
            var result = new List<Photo>();

            foreach (var album in r_LoggedInUser.Albums)
            {
                if (album?.Photos == null)
                    continue;

                foreach (var photo in album.Photos)
                {
                    if (photo != null)
                        result.Add(photo);
                }
            }

            return new List<Photo>(result);
        }
        private int SafeLikes(Post p)
        {
            try { return p?.LikedBy?.Count ?? 0; }
            catch { return 0; }
        }

        private int SafeLikes(Photo p)
        {
            try { return p?.LikedBy?.Count ?? 0; }
            catch { return 0; }
        }

        private DateTime SafeCreated(Post p)
        {
            try { return p?.CreatedTime ?? DateTime.MinValue; }
            catch { return DateTime.MinValue; }
        }

        private DateTime SafeCreated(Photo p)
        {
            try { return p?.CreatedTime ?? DateTime.MinValue; }
            catch { return DateTime.MinValue; }
        }

        private bool UserLiked(Post p)
        {
            try
            {
                string myId = r_LoggedInUser?.Id;
                return p?.LikedBy?.Any(u => u?.Id == myId) == true;
            }
            catch { return false; }
        }

        private bool UserLiked(Photo p)
        {
            try
            {
                string myId = r_LoggedInUser?.Id;
                return p?.LikedBy?.Any(u => u?.Id == myId) == true;
            }
            catch { return false; }
        }
    }
}
