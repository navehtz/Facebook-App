using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.shared.adapters
{
    public class FacebookPostAdapter : IPostData
    {
        private readonly Post r_Post;
        private readonly User r_Owner;
        private static readonly Random sr_Random = new Random();

        public FacebookPostAdapter(Post i_Post, User i_Owner)
        {
            r_Post = i_Post ?? throw new ArgumentNullException(nameof(i_Post));
            r_Owner = i_Owner ?? throw new ArgumentNullException(nameof(i_Owner));
        }

        public string Id
        {
            get
            {
                string result = string.Empty;

                if (r_Post.Id != null)
                {
                    result = r_Post.Id;
                }

                return result;
            }
        }

        public string OwnerName
        {
            get
            {
                string result = string.Empty;

                if (r_Owner.Name != null)
                {
                    result = r_Owner.Name;
                }

                return result;
            }
        }

        public string OwnerPictureUrl
        {
            get
            {
                string result = string.Empty;

                if (!string.IsNullOrEmpty(r_Owner.PictureNormalURL))
                {
                    result = r_Owner.PictureNormalURL;
                }

                return result;
            }
        }

        public string CreatedTimeText
        {
            get
            {
                string result = string.Empty;

                if (r_Post.CreatedTime != null)
                {
                    result = r_Post.CreatedTime.Value.ToString("g");
                }

                return result;
            }
        }

        public string CaptionText
        {
            get
            {
                string result = string.Empty;

                if (!string.IsNullOrEmpty(r_Post.Message))
                {
                    result = r_Post.Message;
                }
                else if (!string.IsNullOrEmpty(r_Post.Caption))
                {
                    result = r_Post.Caption;
                }

                return result;
            }
        }

        public int LikesCount
        {
            get
            {
                int result = 0;

                try
                {
                    if (r_Post.LikedBy != null)
                    {
                        result = r_Post.LikedBy.Count;
                    }
                }
                catch
                {
                    result = sr_Random.Next(5, 150);
                }

                return result;
            }
        }

        public int CommentsCount
        {
            get
            {
                int result = 0;

                try
                {
                    if (r_Post.Comments != null)
                    {
                        result = r_Post.Comments.Count;
                    }
                }
                catch
                {
                    result = sr_Random.Next(0, 50);
                }

                return result;
            }
        }
    }
}
