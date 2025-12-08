using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;
using FacebookWinFormsApp.logic.postNotes;
using FacebookWinFormsApp.ui.CustomComponent;
using FacebookWrapper.ObjectModel;

namespace FacebookWinFormsApp.CustomComponent
{
    public partial class PostComponent : UserControl
    {
        private Post m_Post;
        public IPostNotesManager PostNotesManager { get; set; }
        public string PostId { get; private set; }
        private static readonly Random sr_Random = new Random();

        public PostComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Binds a Facebook Post object to this UI component.
        /// </summary>
        public void SetPost(Post i_Post, User i_OwnerUser)
        {
            if (i_Post == null)
            {
                throw new ArgumentNullException(nameof(i_Post));
            }
            if (i_OwnerUser == null)
            {
                throw new ArgumentNullException(nameof(i_OwnerUser));
            }

            m_Post = i_Post;

            User displayUser = i_OwnerUser;

            // Name
            this.NameLabel.Text = displayUser.Name ?? string.Empty;

            // Date / time
            this.DateTimeLabel.Text = i_Post.CreatedTime?.ToString("g") ?? string.Empty;

            // Caption / text
            string captionText = !string.IsNullOrEmpty(i_Post.Message)
                                     ? i_Post.Message
                                     : i_Post.Caption;
            this.CaptionLabel.Text = captionText ?? string.Empty;

            // Likes (real if available, otherwise random)
            int likesCount;
            try
            {
                likesCount = i_Post.LikedBy?.Count ?? sr_Random.Next(5, 150);
            }
            catch
            {
                likesCount = sr_Random.Next(5, 150);
            }
            this.LikesLabel.Text = $"{likesCount} Likes";

            // Comments
            int commentsCount;
            try
            {
                commentsCount = i_Post.Comments?.Count ?? sr_Random.Next(0, 50);
            }
            catch
            {
                commentsCount = sr_Random.Next(0, 50);
            }
            this.CommentsLabel.Text = $"{commentsCount} Comments";

            // Profile picture of the owner user
            if (!string.IsNullOrEmpty(displayUser.PictureNormalURL))
            {
                try
                {
                    this.ProfilePicPictureBox.LoadAsync(displayUser.PictureNormalURL);
                }
                catch
                {
                    // ignore – keep default avatar
                }
            }

            PostId = i_Post.Id;
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

        private void btnNote_Click(object sender, EventArgs e)
        {
            if(PostNotesManager == null || string.IsNullOrEmpty(PostId))
            {
                return;
            }

            string currentNote = PostNotesManager.GetNoteForPost(PostId) ?? string.Empty;

            using(var noteForm = new NoteEditForm(currentNote))
            {
                if(noteForm.ShowDialog() == DialogResult.OK)
                {
                    string newNote = noteForm.NoteText;
                    if(string.IsNullOrEmpty(newNote))
                    {
                        PostNotesManager.RemoveNoteForPost(PostId);
                        btnNote.Text = "Add Note";
                        NoteIcon.Visible = false;
                    }
                    else
                    {
                        PostNotesManager.SetNoteForPost(PostId, newNote);
                        btnNote.Text = "Edit Note";
                        NoteIcon.Visible = !string.IsNullOrWhiteSpace(newNote);
                    }
                }
            }
        }
    }
}
