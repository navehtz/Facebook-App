using System;

namespace FacebookMini.shared.adapters
{
    public class PostDataSnapshot : IPostData
    {
        private int m_LikesCount;
        public string Id { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPictureUrl { get; set; }
        public string CreatedTimeText { get; set; }
        public string CaptionText { get; set; }

        public int LikesCount
        {
            get => m_LikesCount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                m_LikesCount = value;
            }
        }

        public int CommentsCount { get; set; }

        public string LikesText => $"{LikesCount} Likes";

        public string CommentsText => $"{CommentsCount} Comments";
    }
}
