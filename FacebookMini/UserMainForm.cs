using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookMini.MyComponents;
using FacebookWinFormsApp.CustomComponent;
using FacebookWrapper.ObjectModel;

namespace FacebookMini
{
    public partial class UserMainForm : Form
    {
        private readonly User r_LoggedInUser;

        private Control m_ProfilePage;
        private Control m_FeedPage;
        private Control m_SettingsPage;
        private Control m_Feature1Page;
        private Control m_Feature2Page;

        public UserMainForm()
        {
            InitializeComponent();
        }

        public UserMainForm(User i_LoggedInUser)
            : this() // calls the parameterless ctor (InitializeComponent)
        {
            r_LoggedInUser = i_LoggedInUser ?? throw new ArgumentNullException(nameof(i_LoggedInUser));
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

            // ===== top "Profile" title =====
            var labelHeader = new Label
            {
                Text = "Profile",
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Padding = new Padding(10, 5, 0, 0)
            };
            profilePanel.Controls.Add(labelHeader);

            // ===== user info section =====
            var userInfoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                Padding = new Padding(10, 5, 10, 5)
            };

            // profile picture
            var userPictureBox = new PictureBox
            {
                Size = new Size(80, 80),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(10, 10),
                Image = FacebookWinFormsApp.Properties.
                            Resources.Facebook_default_male_avatar1 // fallback
            };

            if (!string.IsNullOrEmpty(r_LoggedInUser.PictureNormalURL))
            {
                try
                {
                    userPictureBox.LoadAsync(r_LoggedInUser.PictureNormalURL);
                }
                catch
                {
                    // ignore, keep default avatar
                }
            }

            // user name
            var userNameLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(110, 20),
                Text = r_LoggedInUser.Name
            };

            // user extra info (email / birthday / location if available)
            string extraInfo = string.Empty;

            if (!string.IsNullOrEmpty(r_LoggedInUser.Email))
            {
                extraInfo += r_LoggedInUser.Email;
            }

            if (r_LoggedInUser.Birthday != null)
            {
                if (extraInfo.Length > 0) extraInfo += "   |   ";
                extraInfo += $"Birthday: {r_LoggedInUser.Birthday}";
            }

            if (r_LoggedInUser.Location != null &&
                !string.IsNullOrEmpty(r_LoggedInUser.Location.Name))
            {
                if (extraInfo.Length > 0) extraInfo += "   |   ";
                extraInfo += r_LoggedInUser.Location.Name;
            }

            var userExtraLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Location = new Point(110, 55),
                Text = extraInfo
            };

            userInfoPanel.Controls.Add(userPictureBox);
            userInfoPanel.Controls.Add(userNameLabel);
            userInfoPanel.Controls.Add(userExtraLabel);

            profilePanel.Controls.Add(userInfoPanel);
            profilePanel.Controls.SetChildIndex(userInfoPanel, 1);   // under "Profile"
            profilePanel.Controls.SetChildIndex(labelHeader, 0);     // title remains top

            // ===== layout for posts + gallery =====
            var splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical
            };

            // ===== LEFT: posts section =====
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
                WrapContents = false
            };

            postsSectionPanel.Controls.Add(postsFlowPanel);
            postsSectionPanel.Controls.Add(postsTitleLabel);

            splitContainer.Panel1.Controls.Add(postsSectionPanel);

            // ===== RIGHT: albums + pages stacked vertically =====
            var rightPanel = new Panel { Dock = DockStyle.Fill };

            // Albums section
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

            // Pages section
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

            // add in reverse order for Dock = Top stacking
            rightPanel.Controls.Add(pagesSection);
            rightPanel.Controls.Add(pagesTitleLabel);
            rightPanel.Controls.Add(albumsSection);
            rightPanel.Controls.Add(albumsTitleLabel);

            splitContainer.Panel2.Controls.Add(rightPanel);

            // add split container under header + user info
            profilePanel.Controls.Add(splitContainer);
            profilePanel.Controls.SetChildIndex(splitContainer, 2);

            // set splitter distance after panel has a valid size
            profilePanel.Resize += (sender, args) =>
            {
                if (profilePanel.Width > 0)
                {
                    splitContainer.SplitterDistance = (int)(profilePanel.Width * 0.6);
                }
            };

            // ===== fill posts =====
            if (r_LoggedInUser?.Posts != null)
            {
                foreach (Post post in r_LoggedInUser.Posts)
                {
                    var postControl = new PostComponent
                    {
                        Margin = new Padding(5)
                    };

                    postControl.SetPost(post, r_LoggedInUser);
                    postsFlowPanel.Controls.Add(postControl);
                }
            }

            // ===== fill albums =====
            var albumsItems = new System.Collections.Generic.List<GalleryItem>();
            if (r_LoggedInUser.Albums != null)
            {
                foreach (Album album in r_LoggedInUser.Albums)
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

            // ===== fill pages =====
            var pagesItems = new System.Collections.Generic.List<GalleryItem>();
            if (r_LoggedInUser.LikedPages != null)
            {
                foreach (Page page in r_LoggedInUser.LikedPages)
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
