using System.Windows.Forms;

namespace FacebookWinFormsApp.CustomComponent
{
    partial class PostComponent
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label CaptionLabel;
        private System.Windows.Forms.Label LikesLabel;
        private System.Windows.Forms.Label CommentsLabel;

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProfilePicPictureBox = new System.Windows.Forms.PictureBox();
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.CaptionLabel = new System.Windows.Forms.Label();
            this.LikesLabel = new System.Windows.Forms.Label();
            this.CommentsLabel = new System.Windows.Forms.Label();
            this.LikesPictureBox = new System.Windows.Forms.PictureBox();
            this.CommentsPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LikesPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentsPictureBox)).BeginInit();
            this.SuspendLayout();
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
            // DateTimeLabel
            // 
            this.DateTimeLabel.AutoSize = true;
            this.DateTimeLabel.Location = new System.Drawing.Point(91, 48);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(64, 15);
            this.DateTimeLabel.TabIndex = 2;
            this.DateTimeLabel.Text = "Date Time";
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new System.Drawing.Point(91, 24);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new System.Drawing.Size(41, 15);
            NameLabel.TabIndex = 1;
            NameLabel.Text = "Name";
            NameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // CaptionLabel
            // 
            CaptionLabel.AutoSize = true;
            CaptionLabel.Location = new System.Drawing.Point(22, 91);
            CaptionLabel.Name = "CaptionLabel";
            CaptionLabel.Size = new System.Drawing.Size(58, 15);
            CaptionLabel.TabIndex = 3;
            CaptionLabel.Text = "Caption...";
            // 
            // LikesLabel
            // 
            LikesLabel.AutoSize = true;
            LikesLabel.Location = new System.Drawing.Point(50, 207);
            LikesLabel.Name = "LikesLabel";
            LikesLabel.Size = new System.Drawing.Size(46, 15);
            LikesLabel.TabIndex = 4;
            LikesLabel.Text = "0 Likes";
            // 
            // CommentsLabel
            // 
            CommentsLabel.AutoSize = true;
            CommentsLabel.Location = new System.Drawing.Point(150, 207);
            CommentsLabel.Name = "CommentsLabel";
            CommentsLabel.Size = new System.Drawing.Size(77, 15);
            CommentsLabel.TabIndex = 5;
            CommentsLabel.Text = "0 Comments";
            CommentsLabel.Click += new System.EventHandler(this.CommentsLabel_Click);
            // 
            // LikesPictureBox
            // 
            this.LikesPictureBox.Image = global::FacebookWinFormsApp.Properties.Resources.Like;
            this.LikesPictureBox.InitialImage = global::FacebookWinFormsApp.Properties.Resources.Like;
            this.LikesPictureBox.Location = new System.Drawing.Point(11, 198);
            this.LikesPictureBox.Name = "LikesPictureBox";
            this.LikesPictureBox.Size = new System.Drawing.Size(38, 35);
            this.LikesPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LikesPictureBox.TabIndex = 6;
            this.LikesPictureBox.TabStop = false;
            // 
            // CommentsPictureBox
            // 
            this.CommentsPictureBox.Image = global::FacebookWinFormsApp.Properties.Resources.Comments;
            this.CommentsPictureBox.Location = new System.Drawing.Point(110, 199);
            this.CommentsPictureBox.Name = "CommentsPictureBox";
            this.CommentsPictureBox.Size = new System.Drawing.Size(33, 33);
            this.CommentsPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CommentsPictureBox.TabIndex = 7;
            this.CommentsPictureBox.TabStop = false;
            // 
            // PostComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LikesPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentsPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox ProfilePicPictureBox;
        private Label DateTimeLabel;
        private PictureBox LikesPictureBox;
        private PictureBox CommentsPictureBox;


    }
}
