using System.Windows.Forms;

namespace FacebookWinFormsApp.CustomComponent
{
    partial class PostComponent
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        System.Windows.Forms.Label NameLabel = new Label();
        System.Windows.Forms.Label CaptionLabel = new Label();
        System.Windows.Forms.Label LikesLabel = new Label();
        System.Windows.Forms.Label CommentsLabel = new Label();

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.btnNote = new System.Windows.Forms.Button();
            this.NoteIcon = new System.Windows.Forms.PictureBox();
            this.CommentsPictureBox = new System.Windows.Forms.PictureBox();
            this.LikesPictureBox = new System.Windows.Forms.PictureBox();
            this.ProfilePicPictureBox = new System.Windows.Forms.PictureBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.CaptionLabel = new System.Windows.Forms.Label();
            this.LikesLabel = new System.Windows.Forms.Label();
            this.CommentsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NoteIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentsPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LikesPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.AutoSize = true;
            this.DateTimeLabel.Location = new System.Drawing.Point(91, 48);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(56, 13);
            this.DateTimeLabel.TabIndex = 2;
            this.DateTimeLabel.Text = "Date Time";
            // 
            // btnNote
            // 
            this.btnNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNote.Location = new System.Drawing.Point(300, 200);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(70, 26);
            this.btnNote.TabIndex = 8;
            this.btnNote.Text = "Add Note";
            this.btnNote.UseVisualStyleBackColor = true;
            this.btnNote.Click += new System.EventHandler(this.btnNote_Click);
            // 
            // NoteIcon
            // 
            this.NoteIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NoteIcon.Image = global::FacebookWinFormsApp.Properties.Resources.note;
            this.NoteIcon.Location = new System.Drawing.Point(272, 201);
            this.NoteIcon.Name = "NoteIcon";
            this.NoteIcon.Size = new System.Drawing.Size(22, 22);
            this.NoteIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.NoteIcon.TabIndex = 9;
            this.NoteIcon.TabStop = false;
            this.NoteIcon.Visible = false;
            // 
            // CommentsPictureBox
            // 
            this.CommentsPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CommentsPictureBox.Image = global::FacebookWinFormsApp.Properties.Resources.Comments;
            this.CommentsPictureBox.Location = new System.Drawing.Point(110, 199);
            this.CommentsPictureBox.Name = "CommentsPictureBox";
            this.CommentsPictureBox.Size = new System.Drawing.Size(33, 33);
            this.CommentsPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CommentsPictureBox.TabIndex = 7;
            this.CommentsPictureBox.TabStop = false;
            // 
            // LikesPictureBox
            // 
            this.LikesPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LikesPictureBox.Image = global::FacebookWinFormsApp.Properties.Resources.Like;
            this.LikesPictureBox.InitialImage = global::FacebookWinFormsApp.Properties.Resources.Like;
            this.LikesPictureBox.Location = new System.Drawing.Point(11, 198);
            this.LikesPictureBox.Name = "LikesPictureBox";
            this.LikesPictureBox.Size = new System.Drawing.Size(38, 35);
            this.LikesPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LikesPictureBox.TabIndex = 6;
            this.LikesPictureBox.TabStop = false;
            // 
            // ProfilePicPictureBox
            // 
            this.ProfilePicPictureBox.ErrorImage = global::FacebookWinFormsApp.Properties.Resources.no_image;
            this.ProfilePicPictureBox.Image = global::FacebookWinFormsApp.Properties.Resources.Facebook_default_male_avatar1;
            this.ProfilePicPictureBox.InitialImage = null;
            this.ProfilePicPictureBox.Location = new System.Drawing.Point(15, 15);
            this.ProfilePicPictureBox.Name = "ProfilePicPictureBox";
            this.ProfilePicPictureBox.Size = new System.Drawing.Size(60, 57);
            this.ProfilePicPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ProfilePicPictureBox.TabIndex = 0;
            this.ProfilePicPictureBox.TabStop = false;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new System.Drawing.Point(91, 24);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new System.Drawing.Size(35, 13);
            NameLabel.TabIndex = 1;
            NameLabel.Text = "Name";
            NameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // CaptionLabel
            // 
            CaptionLabel.AutoSize = true;
            CaptionLabel.Location = new System.Drawing.Point(22, 91);
            CaptionLabel.Name = "CaptionLabel";
            CaptionLabel.Size = new System.Drawing.Size(52, 13);
            CaptionLabel.TabIndex = 3;
            CaptionLabel.Text = "Caption...";
            // 
            // LikesLabel
            // 
            LikesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            LikesLabel.AutoSize = true;
            LikesLabel.Location = new System.Drawing.Point(50, 207);
            LikesLabel.Name = "LikesLabel";
            LikesLabel.Size = new System.Drawing.Size(41, 13);
            LikesLabel.TabIndex = 4;
            LikesLabel.Text = "0 Likes";
            // 
            // CommentsLabel
            // 
            CommentsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            CommentsLabel.AutoSize = true;
            CommentsLabel.Location = new System.Drawing.Point(150, 207);
            CommentsLabel.Name = "CommentsLabel";
            CommentsLabel.Size = new System.Drawing.Size(65, 13);
            CommentsLabel.TabIndex = 5;
            CommentsLabel.Text = "0 Comments";
            CommentsLabel.Click += new System.EventHandler(this.CommentsLabel_Click);
            // 
            // PostComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.NoteIcon);
            this.Controls.Add(this.btnNote);
            this.Controls.Add(this.CommentsPictureBox);
            this.Controls.Add(this.LikesPictureBox);
            this.Controls.Add(CommentsLabel);
            this.Controls.Add(LikesLabel);
            this.Controls.Add(CaptionLabel);
            this.Controls.Add(this.DateTimeLabel);
            this.Controls.Add(NameLabel);
            this.Controls.Add(this.ProfilePicPictureBox);
            this.Name = "PostComponent";
            this.Size = new System.Drawing.Size(387, 236);
            ((System.ComponentModel.ISupportInitialize)(this.NoteIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentsPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LikesPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox ProfilePicPictureBox;
        private Label DateTimeLabel;
        private PictureBox LikesPictureBox;
        private PictureBox CommentsPictureBox;
        private Button btnNote;
        private PictureBox NoteIcon;
    }
}
