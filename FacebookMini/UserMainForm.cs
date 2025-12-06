using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FacebookMini.Logic;
using FacebookMini.MyComponents;
using FacebookWinFormsApp.CustomComponent;
using FacebookWrapper.ObjectModel;

namespace FacebookMini
{
    public partial class UserMainForm : Form
    {
        private readonly User r_LoggedInUser;
        private readonly IFacebookAppLogic r_AppLogic;

        private Control m_ProfilePage;
        private Control m_FeedPage;
        private Control m_SettingsPage;
        private Control m_Feature1Page;
        private Control m_Feature2Page;

        public UserMainForm()
        {
            InitializeComponent();
        }

        public UserMainForm(IFacebookAppLogic i_AppLogic)
            : this() // calls the parameterless ctor (InitializeComponent)
        {
            r_AppLogic = i_AppLogic ?? throw new ArgumentNullException(nameof(i_AppLogic));
        }

        private void UserMainForm_Load(object sender, EventArgs e)
        {
            buildPages();

            showPage(m_ProfilePage); // default
        }

        private void buildPages()
        {
            m_ProfilePage = buildProfilePage();
            m_FeedPage = buildSimplePlaceholderPage("Feed");
            m_SettingsPage = buildSimplePlaceholderPage("Settings");
            m_Feature1Page = buildSimplePlaceholderPage("Feature 1");
            m_Feature2Page = buildSimplePlaceholderPage("Feature 2");
        }

        private Control buildSimplePlaceholderPage(string i_Title)
        {
            var label = new Label
            {
                Dock = DockStyle.Fill,
                Text = i_Title,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold)
            };

            var panel = new Panel { Dock = DockStyle.Fill };
            panel.Controls.Add(label);
            return panel;
        }

        /// <summary>
        /// Profile page example: posts component + item gallery (albums / liked pages).
        /// </summary>

        private Control buildProfilePage()
        {
            var profilePanel = new Panel { Dock = DockStyle.Fill };

            User loggedInUser = r_AppLogic.LoggedInUser;

            // ===== top "Profile" title =====
            var labelHeader = new Label
            {
                Text = "Profile",
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Padding = new Padding(10, 5, 0, 0)
            };

            // ===== user info section (full width) =====
            var userInfoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                Padding = new Padding(10, 5, 10, 5)
            };

            var userPictureBox = new PictureBox
            {
                Size = new Size(80, 80),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(10, 10),
                Image = FacebookWinFormsApp.Properties.Resources.Facebook_default_male_avatar1
            };

            if (!string.IsNullOrEmpty(loggedInUser.PictureNormalURL))
            {
                try { userPictureBox.LoadAsync(loggedInUser.PictureNormalURL); }
                catch { }
            }

            var userNameLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(110, 20),
                Text = loggedInUser.Name
            };

            string extraInfo = string.Empty;

            if (!string.IsNullOrEmpty(loggedInUser.Email))
            {
                extraInfo += loggedInUser.Email;
            }

            if (loggedInUser.Birthday != null)
            {
                if (extraInfo.Length > 0) extraInfo += "   |   ";
                extraInfo += $"Birthday: {loggedInUser.Birthday}";
            }

            if (loggedInUser.Location != null &&
                !string.IsNullOrEmpty(loggedInUser.Location.Name))
            {
                if (extraInfo.Length > 0) extraInfo += "   |   ";
                extraInfo += loggedInUser.Location.Name;
            }

            var userExtraLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(110, 55),
                Text = extraInfo
            };

            userInfoPanel.Controls.Add(userPictureBox);
            userInfoPanel.Controls.Add(userNameLabel);
            userInfoPanel.Controls.Add(userExtraLabel);

            // ===== split container: left = posts, right = albums/pages =====
            var splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical
            };

            // ----- LEFT: posts section -----
            var postsSectionPanel = new Panel { Dock = DockStyle.Fill };

            var postsTitleLabel = new Label
            {
                Text = "Posts",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Padding = new Padding(5, 5, 0, 0)
            };

            var postsFlowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10, 5, 10, 10)
            };

            postsSectionPanel.Controls.Add(postsFlowPanel);
            postsSectionPanel.Controls.Add(postsTitleLabel);
            splitContainer.Panel1.Controls.Add(postsSectionPanel);

            // ----- RIGHT: albums (top) + pages (bottom) -----
            var rightPanel = new Panel { Dock = DockStyle.Fill };

            var albumsTitleLabel = new Label
            {
                Text = "Albums",
                Dock = DockStyle.Top,
                Height = 25,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(5, 5, 0, 0)
            };

            var albumsSection = new ItemGalleryComponent
            {
                Dock = DockStyle.Top,
                Height = 250
            };

            var pagesTitleLabel = new Label
            {
                Text = "Pages you like",
                Dock = DockStyle.Top,
                Height = 25,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(5, 10, 0, 0)
            };

            var pagesSection = new ItemGalleryComponent
            {
                Dock = DockStyle.Fill
            };

            rightPanel.Controls.Add(pagesSection);
            rightPanel.Controls.Add(pagesTitleLabel);
            rightPanel.Controls.Add(albumsSection);
            rightPanel.Controls.Add(albumsTitleLabel);

            splitContainer.Panel2.Controls.Add(rightPanel);

            // add to main panel
            profilePanel.SuspendLayout();
            profilePanel.Controls.Add(splitContainer);   // Fill
            profilePanel.Controls.Add(userInfoPanel);    // Top
            profilePanel.Controls.Add(labelHeader);      // Top
            profilePanel.ResumeLayout();

            profilePanel.Resize += (sender, args) =>
            {
                if (profilePanel.Width > 0)
                {
                    splitContainer.SplitterDistance = (int)(profilePanel.Width * 0.6);
                }
            };

            // ===== fill posts (via logic) =====
            var posts = r_AppLogic.GetUserPosts();
            if (posts != null)
            {
                foreach (Post post in posts)
                {
                    var postControl = new PostComponent
                    {
                        Margin = new Padding(5, 5, 5, 15)
                    };

                    // still uses Facebook types – but the data comes from logic
                    postControl.SetPost(post, loggedInUser);
                    postsFlowPanel.Controls.Add(postControl);
                }
            }

            // ===== fill albums (via logic) =====
            var albumsItems = new System.Collections.Generic.List<GalleryItem>();
            var albums = r_AppLogic.GetUserAlbums();

            if (albums != null)
            {
                foreach (Album album in albums)
                {
                    Image albumImage = album.ImageAlbum;

                    albumsItems.Add(new GalleryItem
                    {
                        Title = album.Name,
                        Image = albumImage,
                        Tag = album
                    });
                }
            }

            albumsSection.SetItems(albumsItems);

            if (albumsItems.Count == 0)
            {
                albumsSection.Visible = false;
                albumsTitleLabel.Visible = false;
                albumsSection.Height = 0;
            }

            // ===== fill pages (via logic) =====
            var pagesItems = new System.Collections.Generic.List<GalleryItem>();
            var likedPages = r_AppLogic.GetUserLikedPages();

            if (likedPages != null)
            {
                foreach (Page page in likedPages)
                {
                    Image pageImage = page.ImageNormal;

                    pagesItems.Add(new GalleryItem
                    {
                        Title = page.Name,
                        Image = pageImage,
                        Tag = page
                    });
                }
            }

            pagesSection.SetItems(pagesItems);

            return profilePanel;
        }




        private void showPage(Control i_Page)
        {
            panelContent.Controls.Clear();
            if (i_Page != null)
            {
                panelContent.Controls.Add(i_Page);
            }
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            showPage(m_ProfilePage);
        }

        private void buttonFeed_Click(object sender, EventArgs e)
        {
            showPage(m_FeedPage);
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            showPage(m_SettingsPage);
        }

        private void buttonFeature1_Click(object sender, EventArgs e)
        {
            showPage(m_Feature1Page);
        }

        private void buttonFeature2_Click(object sender, EventArgs e)
        {
            showPage(m_Feature2Page);
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            // TODO: add real logout logic.
            this.Close();
        }
    }
}
