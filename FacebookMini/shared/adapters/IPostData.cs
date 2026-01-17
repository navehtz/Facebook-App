using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.shared.adapters
{
    public interface IPostData
    {
        string Id { get; }
        string OwnerName { get; }
        string OwnerPictureUrl { get; }
        string CreatedTimeText { get; }
        string CaptionText { get; }
        int LikesCount { get; }
        int CommentsCount { get; }
    }
}
