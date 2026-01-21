using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookMini.Logic;
using FacebookMini.shared.adapters;

namespace FacebookMini.ui.CustomComponent
{
    public partial class PostComponent : UserControl
    {
        public string PostId { get; private set; }

        private  IPostData m_PostData;
        private bool m_IsLikedByUser = false;
        //private static readonly Random sr_Random = new Random();

        public IFacebookAppLogic AppLogic { get; set; }

        private Button m_TagsButton;
        private Label m_TagsLabel;

        public PostComponent()
        { 
            InitializeComponent();
            initializeTagsUi();
            MessagePanel.RightToLeft = RightToLeft.No;
        }
        /// <summary>
        /// Binds a Facebook Post object to this UI component.
        /// </summary>
        public void SetPost(IPostData i_PostData)
        {
            m_PostData = i_PostData ?? throw new ArgumentNullException(nameof(i_PostData));

            iPostDataBindingSource.DataSource = m_PostData;


            PostId = i_PostData.Id;

            updateTagsLabel();
        }

        //Optional - Maybe add later
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
            if (AppLogic == null || string.IsNullOrEmpty(PostId))
            {
                return;
            }

            string currentNote = AppLogic.GetNoteForPost(PostId) ?? string.Empty;

            using (NoteEditForm noteForm = new NoteEditForm(currentNote))
            {
                if (noteForm.ShowDialog() == DialogResult.OK)
                {
                    string newNote = noteForm.NoteText;

                    if (string.IsNullOrEmpty(newNote))
                    {
                        AppLogic.RemoveNoteForPost(PostId);
                        btnNote.Text = @"Add Note";
                        NoteIcon.Visible = false;
                    }
                    else
                    {
                        AppLogic.SetNoteForPost(PostId, newNote);
                        btnNote.Text = @"Edit Note";
                        NoteIcon.Visible = !string.IsNullOrWhiteSpace(newNote);
                    }
                }
            }
        }

        private void initializeTagsUi()
        {
            // "Tags" button – next to the Note button
            m_TagsButton = new Button();
            m_TagsButton.Text = @"Tags";
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
            m_TagsLabel.Top = btnNote.Top - 24;
            m_TagsLabel.Visible = false;

            Controls.Add(m_TagsLabel);
        }

        private void tagsButton_Click(object sender, EventArgs e)
        {
            if(AppLogic == null || string.IsNullOrEmpty(PostId))
            {
                return;
            }

            string existingTagsText = AppLogic.GetTagsCommaSeparated(PostId);

            using (NoteEditForm dialog = new NoteEditForm(existingTagsText))
            {
                dialog.Text = @"Edit tags (comma separated)";

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    AppLogic.SetTagsFromCommaSeparated(PostId, dialog.NoteText);
                    updateTagsLabel();
                }
            }
        }

        private void updateTagsLabel()
        {
            string tagsText = string.Empty;

            if(AppLogic != null && !string.IsNullOrEmpty(PostId))
            {
                tagsText = AppLogic.GetTagsCommaSeparated(PostId);
            }

            if (m_TagsLabel != null)
            {
                if (string.IsNullOrEmpty(tagsText))
                {
                    m_TagsLabel.Visible = false;
                }
                else
                {
                    m_TagsLabel.Text = $@"Tags: {tagsText}";
                    m_TagsLabel.Visible = true;
                }
            }
        }

        private void LikesPictureBox_Click(object sender, EventArgs e)
        {
            if (m_PostData == null)
            {
                return;
            }

            if (m_IsLikedByUser)
            {
                m_IsLikedByUser = false;
                LikesPictureBox.BackColor = Color.Transparent;
                m_PostData.LikesCount -= 1;
            }
            else
            {
                m_IsLikedByUser = true;
                LikesPictureBox.BackColor = Color.LightGray;
                m_PostData.LikesCount += 1;
            }

            LikesLabel.Text = $@"{m_PostData.LikesText}";
        }

        private void LikesPictureBox_MouseEnter(object sender, EventArgs e)
        {
            LikesPictureBox.BackColor = Color.LightGray;
        }

        private void LikesPictureBox_MouseLeave(object sender, EventArgs e)
        {
            if(!m_IsLikedByUser)
            {
                LikesPictureBox.BackColor = Color.Transparent;
            }
        }
    }
}