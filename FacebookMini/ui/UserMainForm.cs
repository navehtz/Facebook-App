using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Facebook;
using FacebookMini.Logic;
using FacebookMini.MyComponents;
using FacebookWinFormsApp.CustomComponent;
using FacebookWinFormsApp.logic.postNotes;
using FacebookWrapper.ObjectModel;

namespace FacebookMini
{
    public partial class UserMainForm : Form
    {
        //TODO: Define min size
        private readonly IFacebookAppLogic r_AppLogic;
        private readonly User r_LoggedInUser;
      
        private Control m_ProfilePage;
        private Control m_FeedPage;
        private Control m_SettingsPage;
        private Control m_Feature1Page;

        public UserMainForm()
        {
            InitializeComponent();
        }

        public UserMainForm(IFacebookAppLogic i_AppLogic)
            : this() // calls the parameterless ctor (InitializeComponent)
        {
            r_AppLogic = i_AppLogic ?? throw new ArgumentNullException(nameof(i_AppLogic));
            r_LoggedInUser = r_AppLogic.LoggedInUser;
        }

        private void UserMainForm_Load(object sender, EventArgs e)
        {
            buildPages();
            showPage(m_ProfilePage); // default
        }

        private void buildPages()
        {
            m_ProfilePage = buildProfilePage();
            m_FeedPage = null;
            m_SettingsPage = buildSimplePlaceholderPage("Settings");
            m_Feature1Page = buildSimplePlaceholderPage("Feature 1");
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
                Image = r_LoggedInUser.ImageNormal ?? FacebookWinFormsApp.Properties.Resources.Facebook_default_male_avatar1
            };

            if (!string.IsNullOrEmpty(r_LoggedInUser.PictureNormalURL))
            {
                try { userPictureBox.LoadAsync(r_LoggedInUser.PictureNormalURL); }
                catch { }
            }

            var userNameLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(110, 20),
                Text = r_LoggedInUser.Name
            };

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
                    try
                    {
                        splitContainer.SplitterDistance = (int)(profilePanel.Width * 0.6);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            };

            // ===== fill posts (via logic) =====
            var posts = r_AppLogic.GetUserPosts();

            if (posts != null)
            {
                IPostNotesManager postNotesManager = new InMemoryPostNotesManager();

                foreach (Post post in posts)
                {
                    var postControl = new PostComponent
                    {
                        Margin = new Padding(5, 5, 5, 15),
                        PostNotesManager = postNotesManager
                    };

                    // still uses Facebook types – but the data comes from logic
                    postControl.SetPost(post, r_LoggedInUser);
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

        private Control buildFriendsFeedPage()
        {
            var feedPanel = new Panel { Dock = DockStyle.Fill };

            // Header "Feed"
            var headerLabel = new Label
                                  {
                                      Text = "Feed",
                                      Dock = DockStyle.Top,
                                      Height = 40,
                                      Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                                      Padding = new Padding(10, 5, 0, 0)
                                  };
            feedPanel.Controls.Add(headerLabel);

            // Scrollable list of posts
            var postsFlowPanel = new FlowLayoutPanel
                                     {
                                         Dock = DockStyle.Fill,
                                         AutoScroll = true,
                                         FlowDirection = FlowDirection.TopDown,
                                         WrapContents = false,
                                         Padding = new Padding(10)
                                     };
            feedPanel.Controls.Add(postsFlowPanel);
            feedPanel.Controls.SetChildIndex(postsFlowPanel, 1); // under header

            // --- core: load friends & their posts ---
            if (r_LoggedInUser.Friends != null && r_LoggedInUser.Friends.Count > 0)
            {
                foreach (User friend in r_LoggedInUser.Friends)
                {
                    if (friend == null)
                    {
                        continue;
                    }

                    if (friend.Posts == null)
                    {
                        continue;
                    }

                    foreach (Post post in friend.Posts)
                    {
                        if (post == null)
                        {
                            continue;
                        }

                        var postControl = new PostComponent
                                              {
                                                  Margin = new Padding(5, 5, 5, 15)
                                              };

                        postControl.SetPost(post, friend);

                        postsFlowPanel.Controls.Add(postControl);
                    }
                }
            }

            return feedPanel;
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
            try
            {
                if (r_LoggedInUser.Friends == null || r_LoggedInUser.Friends.Count == 0)
                {
                    MessageBox.Show(
@"No friends are available to display in the feed.

This can happen if:
• The user has no friends
• Or Facebook did not grant access to friends data",
                        "Feed is empty",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                if (m_FeedPage == null)
                {
                    m_FeedPage = buildFriendsFeedPage();
                }

                showPage(m_FeedPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    @"An error occurred while loading the feed.{Environment.NewLine} {ex.Message}",
                    "Feed error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void buttonSettings_Click(object sender, EventArgs e)
        {
            showPage(m_SettingsPage);
        }

        private void buttonFeature1_Click(object sender, EventArgs e)
        {
            showPage(m_Feature1Page);
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            // TODO: add real logout logic.
            this.Close();
        }

        private void userPictureBoxTopBar_Click(object sender, EventArgs e)
        {

        }
    }
}
