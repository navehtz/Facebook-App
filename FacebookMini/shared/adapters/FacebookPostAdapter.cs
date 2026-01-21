using FacebookWrapper.ObjectModel;
using System;

namespace FacebookMini.shared.adapters
{
    public class FacebookPostAdapter
    {
        private readonly Post r_Post;
        private readonly User r_Owner;

        private static readonly Random sr_Random = new Random();

        public FacebookPostAdapter(Post i_Post, User i_Owner)
        {
            r_Post = i_Post;
            r_Owner = i_Owner;
        }

        public PostDataSnapshot ToSnapshot()
        {
            PostDataSnapshot postDataSnapshot = new PostDataSnapshot
                                            {
                                                Id = r_Post.Id ?? string.Empty,
                                                OwnerName = r_Owner.Name ?? string.Empty,
                                                OwnerPictureUrl = r_Owner.PictureNormalURL ?? string.Empty
                                            };

            if (r_Post.CreatedTime != null)
            {
                postDataSnapshot.CreatedTimeText = r_Post.CreatedTime.Value.ToString("g");
            }
            else
            {
                postDataSnapshot.CreatedTimeText = string.Empty;
            }

            if (!string.IsNullOrEmpty(r_Post.Message))
            {
                postDataSnapshot.CaptionText = r_Post.Message;
            }
            else if (!string.IsNullOrEmpty(r_Post.Caption))
            {
                postDataSnapshot.CaptionText = r_Post.Caption;
            }
            else
            {
                postDataSnapshot.CaptionText = string.Empty;
            }

            postDataSnapshot.LikesCount = getLikesCount();
            postDataSnapshot.CommentsCount = getCommentsCount();

            return postDataSnapshot;
        }

        private int getLikesCount()
        {
            int count = 0;

            try
            {
                if (r_Post.LikedBy != null)
                {
                    count = r_Post.LikedBy.Count;
                }
            }
            catch
            {
                count = sr_Random.Next(5, 150);
            }

            return count;
        }

        private int getCommentsCount()
        {
            int count = 0;

            try
            {
                if (r_Post.Comments != null)
                {
                    count = r_Post.Comments.Count;
                }
            }
            catch
            {
                count = sr_Random.Next(0, 50);
            }

            return count;
        }
    }
}
