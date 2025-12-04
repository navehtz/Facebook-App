using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookMini.Logic
{
    internal class AnalyticsManager
    {
        private readonly User r_LoggedInUser;

        public AnalyticsManager(User i_LoggedInUser)
        {
            r_LoggedInUser = i_LoggedInUser ?? throw new ArgumentNullException(nameof(i_LoggedInUser));
        }

        public Post GetMostLikedPost()
        {
            Post mostLikedPost = null;
            int maxLikesCount = -1;

            foreach (Post post in r_LoggedInUser.Posts)
            {
                if (post == null)
                {
                    continue;
                }

                int likesCount = post.LikedBy?.Count ?? 0;

                if (likesCount > maxLikesCount)
                {
                    maxLikesCount = likesCount;
                    mostLikedPost = post;
                }
            }

            return mostLikedPost;
        }

        public Photo GetMostLikedPhoto()
        {
            Photo mostLikedPhoto = null;
            int maxLikesCount = -1;

            foreach (Album album in r_LoggedInUser.Albums)
            {
                if (album == null)
                {
                    continue;
                }

                foreach (Photo photo in album.Photos)
                {
                    if (photo == null)
                    {
                        continue;
                    }

                    int likesCount = photo.LikedBy?.Count ?? 0;

                    if (likesCount > maxLikesCount)
                    {
                        maxLikesCount = likesCount;
                        mostLikedPhoto = photo;
                    }
                }
            }

            return mostLikedPhoto;
        }

        public List<Post> GetTopPostsByLikes(int i_MaxCount)
        {
            return r_LoggedInUser.Posts
                .Where(post => post != null)
                .OrderByDescending(post => post.LikedBy?.Count ?? 0)
                .Take(i_MaxCount)
                .ToList();
        }
    }
}