using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace FacebookWinFormsApp.CustomComponent
{
    public partial class PostComponent : UserControl
    {
        private Post m_Post;

        public PostComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Binds a Facebook Post object to this UI component.
        /// </summary>
        public void SetPost(Post i_Post)
        {
            if (i_Post == null)
            {
                throw new ArgumentNullException(nameof(i_Post));
            }

            m_Post = i_Post;

            // Who posted
            this.NameLabel.Text = i_Post.From?.Name ?? string.Empty;

            // When
            this.DateTimeLabel.Text = i_Post.CreatedTime?.ToString("g") ?? string.Empty;

            // Caption / text
            string captionText = !string.IsNullOrEmpty(i_Post.Message)
                ? i_Post.Message
                : i_Post.Caption;

            this.CaptionLabel.Text = captionText ?? string.Empty;

            // Likes count
            int likesCount = i_Post.LikedBy != null ? i_Post.LikedBy.Count : 0;
            this.LikesLabel.Text = $"{likesCount} Likes";

            // Comments count
            int commentsCount = i_Post.Comments != null ? i_Post.Comments.Count : 0;
            this.CommentsLabel.Text = $"{commentsCount} Comments";

            // Profile picture
            if (i_Post.From != null && !string.IsNullOrEmpty(i_Post.From.PictureNormalURL))
            {
                try
                {
                    this.ProfilePicPictureBox.LoadAsync(i_Post.From.PictureNormalURL);
                }
                catch
                {
                    // ignore image loading errors, keep default image
                }
            }
        }

        // Optional: click handlers (you can raise events here later if you want)
        private void label1_Click(object sender, EventArgs e)
        {
            // For example: open profile of m_Post.From
        }

        private void CommentsLabel_Click(object sender, EventArgs e)
        {
            // For example: open comments of m_Post
        }
    }
}
