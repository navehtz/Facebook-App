using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookWinFormsApp.ui.viewModels
{
    internal class FeedPostViewModel
    {
        public string Id { get; set; }

        // author
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPictureUrl { get; set; }

        // post content
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }

        // stats
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
    }
}
