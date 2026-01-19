using System.Windows.Forms;

namespace FacebookMini.ui.CustomComponent
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
            System.Windows.Forms.Label NameLabel;
            System.Windows.Forms.Label LikesLabel;
            System.Windows.Forms.Label CommentsLabel;
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.btnNote = new System.Windows.Forms.Button();
            this.NoteIcon = new System.Windows.Forms.PictureBox();
            this.CommentsPictureBox = new System.Windows.Forms.PictureBox();
            this.LikesPictureBox = new System.Windows.Forms.PictureBox();
            this.ProfilePicPictureBox = new System.Windows.Forms.PictureBox();
            this.MessagePanel = new System.Windows.Forms.Panel();
            this.CaptionBox = new System.Windows.Forms.RichTextBox();
            NameLabel = new System.Windows.Forms.Label();
            LikesLabel = new System.Windows.Forms.Label();
            CommentsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NoteIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentsPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LikesPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicPictureBox)).BeginInit();
            this.MessagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new System.Drawing.Point(136, 37);
            NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new System.Drawing.Size(51, 20);
            NameLabel.TabIndex = 1;
            NameLabel.Text = "Name";
            NameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // LikesLabel
            // 
            LikesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            LikesLabel.AutoSize = true;
            LikesLabel.Location = new System.Drawing.Point(75, 380);
            LikesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LikesLabel.Name = "LikesLabel";
            LikesLabel.Size = new System.Drawing.Size(59, 20);
            LikesLabel.TabIndex = 4;
            LikesLabel.Text = "0 Likes";
            // 
            // CommentsLabel
            // 
            CommentsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            CommentsLabel.AutoSize = true;
            CommentsLabel.Location = new System.Drawing.Point(225, 380);
            CommentsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            CommentsLabel.Name = "CommentsLabel";
            CommentsLabel.Size = new System.Drawing.Size(99, 20);
            CommentsLabel.TabIndex = 5;
            CommentsLabel.Text = "0 Comments";
            CommentsLabel.Click += new System.EventHandler(this.CommentsLabel_Click);
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.AutoSize = true;
            this.DateTimeLabel.Location = new System.Drawing.Point(136, 74);
            this.DateTimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(82, 20);
            this.DateTimeLabel.TabIndex = 2;
            this.DateTimeLabel.Text = "Date Time";
            // 
            // btnNote
            // 
            this.btnNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNote.Location = new System.Drawing.Point(498, 375);
            this.btnNote.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(105, 40);
            this.btnNote.TabIndex = 8;
            this.btnNote.Text = "Add Note";
            this.btnNote.UseVisualStyleBackColor = true;
            this.btnNote.Click += new System.EventHandler(this.btnNote_Click);
            // 
            // NoteIcon
            // 
            this.NoteIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NoteIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NoteIcon.Image = global::FacebookMini.Properties.Resources.note;
            this.NoteIcon.Location = new System.Drawing.Point(578, 23);
            this.NoteIcon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NoteIcon.Name = "NoteIcon";
            this.NoteIcon.Size = new System.Drawing.Size(33, 34);
            this.NoteIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.NoteIcon.TabIndex = 9;
            this.NoteIcon.TabStop = false;
            this.NoteIcon.Visible = false;
            this.NoteIcon.Click += new System.EventHandler(this.btnNote_Click);
            // 
            // CommentsPictureBox
            // 
            this.CommentsPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CommentsPictureBox.Image = global::FacebookMini.Properties.Resources.Comments;
            this.CommentsPictureBox.InitialImage = global::FacebookMini.Properties.Resources.Comments;
            this.CommentsPictureBox.Location = new System.Drawing.Point(163, 363);
            this.CommentsPictureBox.Name = "CommentsPictureBox";
            this.CommentsPictureBox.Size = new System.Drawing.Size(55, 55);
            this.CommentsPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CommentsPictureBox.TabIndex = 7;
            this.CommentsPictureBox.TabStop = false;
            // 
            // LikesPictureBox
            // 
            this.LikesPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LikesPictureBox.Image = global::FacebookMini.Properties.Resources.Old_Facebook_Like__Cornered___1106x1008;
            this.LikesPictureBox.InitialImage = global::FacebookMini.Properties.Resources.Old_Facebook_Like__Cornered___1106x1008;
            this.LikesPictureBox.Location = new System.Drawing.Point(20, 366);
            this.LikesPictureBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LikesPictureBox.Name = "LikesPictureBox";
            this.LikesPictureBox.Size = new System.Drawing.Size(47,47);
            this.LikesPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LikesPictureBox.TabIndex = 6;
            this.LikesPictureBox.TabStop = false;
            // 
            // ProfilePicPictureBox
            // 
            this.ProfilePicPictureBox.ErrorImage = global::FacebookMini.Properties.Resources.no_image;
            this.ProfilePicPictureBox.Image = global::FacebookMini.Properties.Resources.Facebook_default_male_avatar;
            this.ProfilePicPictureBox.InitialImage = null;
            this.ProfilePicPictureBox.Location = new System.Drawing.Point(22, 23);
            this.ProfilePicPictureBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ProfilePicPictureBox.Name = "ProfilePicPictureBox";
            this.ProfilePicPictureBox.Size = new System.Drawing.Size(90, 88);
            this.ProfilePicPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ProfilePicPictureBox.TabIndex = 0;
            this.ProfilePicPictureBox.TabStop = false;
            // 
            // MessagePanel
            // 
            this.MessagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessagePanel.AutoScroll = true;
            this.MessagePanel.Controls.Add(this.CaptionBox);
            this.MessagePanel.Location = new System.Drawing.Point(22, 123);
            this.MessagePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MessagePanel.Name = "MessagePanel";
            this.MessagePanel.Size = new System.Drawing.Size(580, 215);
            this.MessagePanel.TabIndex = 10;
            // 
            // CaptionBox
            // 
            this.CaptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CaptionBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CaptionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CaptionBox.DetectUrls = false;
            this.CaptionBox.Location = new System.Drawing.Point(10, 11);
            this.CaptionBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CaptionBox.Name = "CaptionBox";
            this.CaptionBox.ReadOnly = true;
            this.CaptionBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CaptionBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.CaptionBox.Size = new System.Drawing.Size(66, 148);
            this.CaptionBox.TabIndex = 0;
            this.CaptionBox.TabStop = false;
            this.CaptionBox.Text = "";
            // 
            // PostComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.MessagePanel);
            this.Controls.Add(this.NoteIcon);
            this.Controls.Add(this.btnNote);
            this.Controls.Add(this.CommentsPictureBox);
            this.Controls.Add(this.LikesPictureBox);
            this.Controls.Add(CommentsLabel);
            this.Controls.Add(LikesLabel);
            this.Controls.Add(this.DateTimeLabel);
            this.Controls.Add(NameLabel);
            this.Controls.Add(this.ProfilePicPictureBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PostComponent";
            this.Size = new System.Drawing.Size(630, 446);
            ((System.ComponentModel.ISupportInitialize)(this.NoteIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentsPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LikesPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicPictureBox)).EndInit();
            this.MessagePanel.ResumeLayout(false);
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

        // NEW
        private System.Windows.Forms.Panel MessagePanel;
        private System.Windows.Forms.RichTextBox CaptionBox;
    }
}
