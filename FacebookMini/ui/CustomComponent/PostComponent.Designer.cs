using System.Windows.Forms;

namespace FacebookMini.CustomComponent
{
    partial class PostComponent
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.btnNote = new System.Windows.Forms.Button();
            this.NoteIcon = new System.Windows.Forms.PictureBox();
            this.CommentsPictureBox = new System.Windows.Forms.PictureBox();
            this.LikesPictureBox = new System.Windows.Forms.PictureBox();
            this.ProfilePicPictureBox = new System.Windows.Forms.PictureBox();
            this.NameLabel = new System.Windows.Forms.Label();

            // Scrollable panel for the post text
            this.MessagePanel = new System.Windows.Forms.Panel();
            this.CaptionBox = new RichTextBox();

            this.LikesLabel = new System.Windows.Forms.Label();
            this.CommentsLabel = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.NoteIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentsPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LikesPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicPictureBox)).BeginInit();

            this.SuspendLayout();

            // DateTimeLabel
            this.DateTimeLabel.AutoSize = true;
            this.DateTimeLabel.Location = new System.Drawing.Point(91, 48);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(56, 13);
            this.DateTimeLabel.TabIndex = 2;
            this.DateTimeLabel.Text = "Date Time";

            // btnNote
            this.btnNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNote.Location = new System.Drawing.Point(332, 244);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(70, 26);
            this.btnNote.TabIndex = 8;
            this.btnNote.Text = "Add Note";
            this.btnNote.UseVisualStyleBackColor = true;
            this.btnNote.Click += new System.EventHandler(this.btnNote_Click);

            // NoteIcon
            this.NoteIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NoteIcon.Image = global::FacebookMini.Properties.Resources.note;
            this.NoteIcon.Location = new System.Drawing.Point(385, 15);
            this.NoteIcon.Name = "NoteIcon";
            this.NoteIcon.Size = new System.Drawing.Size(22, 22);
            this.NoteIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.NoteIcon.TabIndex = 9;
            this.NoteIcon.Click += new System.EventHandler(this.btnNote_Click);
            this.NoteIcon.TabStop = false;
            this.NoteIcon.Visible = false;
            this.NoteIcon.Cursor = Cursors.Hand;

            // CommentsPictureBox
            this.CommentsPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CommentsPictureBox.Image = global::FacebookMini.Properties.Resources.Comments;
            this.CommentsPictureBox.Location = new System.Drawing.Point(110, 238);
            this.CommentsPictureBox.Name = "CommentsPictureBox";
            this.CommentsPictureBox.Size = new System.Drawing.Size(33, 33);
            this.CommentsPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CommentsPictureBox.TabIndex = 7;
            this.CommentsPictureBox.TabStop = false;

            // LikesPictureBox
            this.LikesPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LikesPictureBox.Image = global::FacebookMini.Properties.Resources.Like;
            this.LikesPictureBox.InitialImage = global::FacebookMini.Properties.Resources.Like;
            this.LikesPictureBox.Location = new System.Drawing.Point(11, 238);
            this.LikesPictureBox.Name = "LikesPictureBox";
            this.LikesPictureBox.Size = new System.Drawing.Size(38, 35);
            this.LikesPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LikesPictureBox.TabIndex = 6;
            this.LikesPictureBox.TabStop = false;

            // ProfilePicPictureBox
            this.ProfilePicPictureBox.ErrorImage = global::FacebookMini.Properties.Resources.no_image;
            this.ProfilePicPictureBox.Image = global::FacebookMini.Properties.Resources.Facebook_default_male_avatar;
            this.ProfilePicPictureBox.InitialImage = null;
            this.ProfilePicPictureBox.Location = new System.Drawing.Point(15, 15);
            this.ProfilePicPictureBox.Name = "ProfilePicPictureBox";
            this.ProfilePicPictureBox.Size = new System.Drawing.Size(60, 57);
            this.ProfilePicPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ProfilePicPictureBox.TabIndex = 0;
            this.ProfilePicPictureBox.TabStop = false;

            // NameLabel
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(91, 24);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(35, 13);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "Name";
            this.NameLabel.Click += new System.EventHandler(this.label1_Click);

            // MessagePanel (scroll container for the text)
            this.MessagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                        | System.Windows.Forms.AnchorStyles.Right)
                                        | System.Windows.Forms.AnchorStyles.Bottom)));
            this.MessagePanel.AutoScroll = true;
            this.MessagePanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MessagePanel.Location = new System.Drawing.Point(15, 80);
            this.MessagePanel.Name = "MessagePanel";
            this.MessagePanel.Size = new System.Drawing.Size(387, 140);
            this.MessagePanel.TabIndex = 10;


            // CaptionBox (RichTextBox)
            this.CaptionBox = new System.Windows.Forms.RichTextBox();
            this.CaptionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CaptionBox.ReadOnly = true;
            this.CaptionBox.Multiline = true;
            this.CaptionBox.WordWrap = true;
            this.CaptionBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.CaptionBox.DetectUrls = false;
            this.CaptionBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CaptionBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CaptionBox.TabStop = false;

            this.CaptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top
              | System.Windows.Forms.AnchorStyles.Left
              | System.Windows.Forms.AnchorStyles.Right
              | System.Windows.Forms.AnchorStyles.Bottom));

            this.CaptionBox.Location = new System.Drawing.Point(7, 7);
            this.CaptionBox.Size = new System.Drawing.Size(this.MessagePanel.Width - 14, this.MessagePanel.Height - 14);

            this.MessagePanel.Controls.Add(this.CaptionBox);

            // width is constrained at runtime to enable wrapping + scrolling

            // add caption to panel
            this.MessagePanel.Controls.Add(this.CaptionBox);

            // LikesLabel
            this.LikesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LikesLabel.AutoSize = true;
            this.LikesLabel.Location = new System.Drawing.Point(50, 247);
            this.LikesLabel.Name = "LikesLabel";
            this.LikesLabel.Size = new System.Drawing.Size(41, 13);
            this.LikesLabel.TabIndex = 4;
            this.LikesLabel.Text = "0 Likes";

            // CommentsLabel
            this.CommentsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CommentsLabel.AutoSize = true;
            this.CommentsLabel.Location = new System.Drawing.Point(150, 247);
            this.CommentsLabel.Name = "CommentsLabel";
            this.CommentsLabel.Size = new System.Drawing.Size(65, 13);
            this.CommentsLabel.TabIndex = 5;
            this.CommentsLabel.Text = "0 Comments";
            this.CommentsLabel.Click += new System.EventHandler(this.CommentsLabel_Click);

            // PostComponent (root)
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;

            this.Controls.Add(this.MessagePanel);
            this.Controls.Add(this.NoteIcon);
            this.Controls.Add(this.btnNote);
            this.Controls.Add(this.CommentsPictureBox);
            this.Controls.Add(this.LikesPictureBox);
            this.Controls.Add(this.CommentsLabel);
            this.Controls.Add(this.LikesLabel);
            this.Controls.Add(this.DateTimeLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.ProfilePicPictureBox);

            this.Name = "PostComponent";
            this.Size = new System.Drawing.Size(420, 280); // a bit bigger by default


            ((System.ComponentModel.ISupportInitialize)(this.NoteIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentsPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LikesPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicPictureBox)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox ProfilePicPictureBox;
        private System.Windows.Forms.Label DateTimeLabel;
        private System.Windows.Forms.PictureBox LikesPictureBox;
        private System.Windows.Forms.PictureBox CommentsPictureBox;
        private System.Windows.Forms.Button btnNote;
        private System.Windows.Forms.PictureBox NoteIcon;

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label LikesLabel;
        private System.Windows.Forms.Label CommentsLabel;

        // NEW
        private System.Windows.Forms.Panel MessagePanel;
        private System.Windows.Forms.RichTextBox CaptionBox;
    }
}
