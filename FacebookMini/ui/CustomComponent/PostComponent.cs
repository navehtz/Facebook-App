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
using FacebookWinFormsApp.logic.postTags;
using FacebookWinFormsApp.ui.CustomComponent;
using FacebookWrapper.ObjectModel;

namespace FacebookWinFormsApp.CustomComponent
{
    public partial class PostComponent : UserControl
    {
        private Post m_Post;
        public string PostId { get; private set; }
        public IPostNotesManager PostNotesManager { get; set; }
        private IPostTagsManager m_PostTagsManager;
        private static readonly Random sr_Random = new Random();

        private Button m_TagsButton;
        private Label m_TagsLabel;

        public IPostTagsManager PostTagsManager
        {
            get { return m_PostTagsManager; }
            set
            {
                m_PostTagsManager = value;
                updateTagsLabel();
               
            }
        }

        public PostComponent()
        { 
            InitializeComponent();
            initializeTagsUi();
            MessagePanel.RightToLeft = RightToLeft.No;
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
            this.CaptionBox.Text = captionText ?? string.Empty;

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
            updateTagsLabel();
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
            if (PostNotesManager == null || string.IsNullOrEmpty(PostId))
            {
                return;
            }

            string currentNote = PostNotesManager.GetNoteForPost(PostId) ?? string.Empty;

            using (var noteForm = new NoteEditForm(currentNote))
            {
                if (noteForm.ShowDialog() == DialogResult.OK)
                {
                    string newNote = noteForm.NoteText;
                    if (string.IsNullOrEmpty(newNote))
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

        private void initializeTagsUi()
        {
            // "Tags" button – next to the Note button
            m_TagsButton = new Button();
            m_TagsButton.Text = "Tags";
            m_TagsButton.Width = 60;
            m_TagsButton.Height = 26;
            m_TagsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            // place it to the left of the Note button
            m_TagsButton.Left = btnNote.Left - m_TagsButton.Width - 5;
            m_TagsButton.Top = btnNote.Top;

            m_TagsButton.Click += tagsButton_Click;
            Controls.Add(m_TagsButton);

            // Label that shows the tags, above the buttons on the left
            m_TagsLabel = new Label();
            m_TagsLabel.AutoSize = true;
            m_TagsLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            m_TagsLabel.Left = 11;
            m_TagsLabel.Top = btnNote.Top - 18;
            m_TagsLabel.Visible = false;

            Controls.Add(m_TagsLabel);
        }

        private void tagsButton_Click(object sender, EventArgs e)
        {
            if (m_PostTagsManager != null && !string.IsNullOrEmpty(PostId))
            {
                ICollection<string> existingTags = m_PostTagsManager.GetPostTags(PostId);
                StringBuilder tagsStringBuilder = new StringBuilder();
                bool isFirstTag = true;

                foreach (string tagName in existingTags)
                {
                    if (!isFirstTag)
                    {
                        tagsStringBuilder.Append(", ");
                    }
                    else
                    {
                        isFirstTag = false;
                    }

                    tagsStringBuilder.Append(tagName);
                }

                using (NoteEditForm dialog = new NoteEditForm(tagsStringBuilder.ToString()))
                {
                    dialog.Text = "Edit tags (comma separated)";

                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        string raw = dialog.NoteText;
                        List<string> tagsList = new List<string>();

                        if (!string.IsNullOrEmpty(raw))
                        {
                            string[] parts = raw.Split(',');

                            foreach (string part in parts)
                            {
                                if (part != null)
                                {
                                    string tag = part.Trim();
                                    if (tag.Length > 0)
                                    {
                                        tagsList.Add(tag);
                                    }
                                }
                            }
                        }

                        m_PostTagsManager.SetPostTags(PostId, tagsList);
                        updateTagsLabel();
                    }
                }
            }
        }

        private void updateTagsLabel()
        {
            if (m_TagsLabel == null)
            {
                return;
            }

            if (m_PostTagsManager == null || string.IsNullOrEmpty(PostId))
            {
                m_TagsLabel.Visible = false;
            }
            else
            {
                ICollection<string> existingTags = m_PostTagsManager.GetPostTags(PostId);

                if (existingTags == null || existingTags.Count == 0)
                {
                    m_TagsLabel.Visible = false;
                }
                else
                {
                    StringBuilder tagsStringBuilder = new StringBuilder("Tags: ");
                    bool isFirstTag = true;

                    foreach (string tagName in existingTags)
                    {
                        if (!isFirstTag)
                        {
                            tagsStringBuilder.Append(", ");
                        }
                        else
                        {
                            isFirstTag = false;
                        }

                        tagsStringBuilder.Append(tagName);
                    }

                    m_TagsLabel.Text = tagsStringBuilder.ToString();
                    m_TagsLabel.Visible = true;
                }
            }
        }
    }
}
