namespace FacebookMini
{
    partial class UserMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelTitle;

        private System.Windows.Forms.Panel panelSideMenu;
        private System.Windows.Forms.Button buttonProfile;
        private System.Windows.Forms.Button buttonFeed;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonFeature1;
        private System.Windows.Forms.Button buttonFeature2;
        private System.Windows.Forms.Button buttonLogout;

        private System.Windows.Forms.Panel panelContent;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.userPictureBoxTopBar = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonFeature2 = new System.Windows.Forms.Button();
            this.buttonFeature1 = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonFeed = new System.Windows.Forms.Button();
            this.buttonProfile = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBoxTopBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.panelSideMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(119)))), ((int)(((byte)(242)))));
            this.panelHeader.Controls.Add(this.userPictureBoxTopBar);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Controls.Add(this.pictureBoxLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1195, 86);
            this.panelHeader.TabIndex = 0;
            // 
            // userPictureBoxTopBar
            // 
            this.userPictureBoxTopBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.userPictureBoxTopBar.Image = global::FacebookWinFormsApp.Properties.Resources.Facebook_default_male_avatar1;
            this.userPictureBoxTopBar.Location = new System.Drawing.Point(1115, 13);
            this.userPictureBoxTopBar.Name = "userPictureBoxTopBar";
            this.userPictureBoxTopBar.Size = new System.Drawing.Size(64, 56);
            this.userPictureBoxTopBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPictureBoxTopBar.TabIndex = 0;
            this.userPictureBoxTopBar.TabStop = false;
            this.userPictureBoxTopBar.Click += new System.EventHandler(this.userPictureBoxTopBar_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(75, 23);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(119, 32);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "facebook";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::FacebookWinFormsApp.Properties.Resources.Facebook_logo__square_;
            this.pictureBoxLogo.Location = new System.Drawing.Point(25, 17);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(44, 44);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panelSideMenu.Controls.Add(this.buttonLogout);
            this.panelSideMenu.Controls.Add(this.buttonFeature2);
            this.panelSideMenu.Controls.Add(this.buttonFeature1);
            this.panelSideMenu.Controls.Add(this.buttonSettings);
            this.panelSideMenu.Controls.Add(this.buttonFeed);
            this.panelSideMenu.Controls.Add(this.buttonProfile);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 86);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(180, 687);
            this.panelSideMenu.TabIndex = 1;
            // 
            // buttonLogout
            // 
            this.buttonLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonLogout.ForeColor = System.Drawing.Color.White;
            this.buttonLogout.Location = new System.Drawing.Point(0, 642);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonLogout.Size = new System.Drawing.Size(180, 45);
            this.buttonLogout.TabIndex = 5;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonFeature2
            // 
            this.buttonFeature2.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonFeature2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFeature2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonFeature2.ForeColor = System.Drawing.Color.White;
            this.buttonFeature2.Location = new System.Drawing.Point(0, 180);
            this.buttonFeature2.Name = "buttonFeature2";
            this.buttonFeature2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonFeature2.Size = new System.Drawing.Size(180, 45);
            this.buttonFeature2.TabIndex = 4;
            this.buttonFeature2.Text = "Feature 2";
            this.buttonFeature2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFeature2.UseVisualStyleBackColor = true;
            // 
            // buttonFeature1
            // 
            this.buttonFeature1.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonFeature1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFeature1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonFeature1.ForeColor = System.Drawing.Color.White;
            this.buttonFeature1.Location = new System.Drawing.Point(0, 135);
            this.buttonFeature1.Name = "buttonFeature1";
            this.buttonFeature1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonFeature1.Size = new System.Drawing.Size(180, 45);
            this.buttonFeature1.TabIndex = 3;
            this.buttonFeature1.Text = "Feature 1";
            this.buttonFeature1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFeature1.UseVisualStyleBackColor = true;
            this.buttonFeature1.Click += new System.EventHandler(this.buttonFeature1_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonSettings.ForeColor = System.Drawing.Color.White;
            this.buttonSettings.Location = new System.Drawing.Point(0, 90);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonSettings.Size = new System.Drawing.Size(180, 45);
            this.buttonSettings.TabIndex = 2;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonFeed
            // 
            this.buttonFeed.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonFeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFeed.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonFeed.ForeColor = System.Drawing.Color.White;
            this.buttonFeed.Location = new System.Drawing.Point(0, 45);
            this.buttonFeed.Name = "buttonFeed";
            this.buttonFeed.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonFeed.Size = new System.Drawing.Size(180, 45);
            this.buttonFeed.TabIndex = 1;
            this.buttonFeed.Text = "Feed";
            this.buttonFeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFeed.UseVisualStyleBackColor = true;
            this.buttonFeed.Click += new System.EventHandler(this.buttonFeed_Click);
            // 
            // buttonProfile
            // 
            this.buttonProfile.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProfile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonProfile.ForeColor = System.Drawing.Color.White;
            this.buttonProfile.Location = new System.Drawing.Point(0, 0);
            this.buttonProfile.Name = "buttonProfile";
            this.buttonProfile.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonProfile.Size = new System.Drawing.Size(180, 45);
            this.buttonProfile.TabIndex = 0;
            this.buttonProfile.Text = "Profile";
            this.buttonProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonProfile.UseVisualStyleBackColor = true;
            this.buttonProfile.Click += new System.EventHandler(this.buttonProfile_Click);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(180, 86);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1015, 687);
            this.panelContent.TabIndex = 2;
            // 
            // UserMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 773);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSideMenu);
            this.Controls.Add(this.panelHeader);
            this.Name = "UserMainForm";
            this.Text = "FacebookMini";
            this.Load += new System.EventHandler(this.UserMainForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBoxTopBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.panelSideMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox userPictureBoxTopBar;
    }
}
