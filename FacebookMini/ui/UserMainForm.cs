using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Facebook;
using FacebookMini.Logic;
using FacebookMini.CustomComponent;
using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;
using FacebookWrapper.ObjectModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace FacebookMini
{
    public partial class UserMainForm : Form
    {
        private readonly User r_LoggedInUser;
        private readonly IFacebookAppLogic r_AppLogic;
        private readonly IPostNotesManager r_PostNotesManager = new InMemoryPostNotesManager();
        private readonly IPostTagsManager r_PostTagsManager = new InMemoryPostTagsManager();

        private Control m_ProfilePage;
        private Control m_FeedPage;
        
        private Control m_TagsAnalyticsPage;
        private Chart m_TagsChart;
        private Label m_TagsInfoLabel;

        public UserMainForm()
        {
            InitializeComponent();
            this.MinimumSize = new Size(1000, 700);
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
            m_FeedPage = buildFriendsFeedPage();
            m_TagsAnalyticsPage = buildTagsAnalyticsPage();

            updateAnalyticsPage();
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
                Padding = new Padding(10, 5, 0, 5)
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
                Image = r_LoggedInUser.ImageNormal ?? FacebookMini.Properties.Resources.Facebook_default_male_avatar1
            };

            if (!string.IsNullOrEmpty(r_LoggedInUser.PictureNormalURL))
            {
                try 
                { 
                    userPictureBox.LoadAsync(r_LoggedInUser.PictureNormalURL); 
                    userPictureBoxTopBar.Image = userPictureBox.Image;
                }
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

            // ----- RIGHT: albums (top) + pages (bottom) 50/50 -----
            var tlpRight = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));

            // --- ALBUMS ---
            var albumsTitleLabel = new Label
            {
                Text = "Albums",
                Dock = DockStyle.Top,
                Height = 25,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(5, 2, 0, 0)
            };
            var albumsSection = new ItemGalleryComponent { Dock = DockStyle.Fill };

            var albumsContainer = new Panel { Dock = DockStyle.Fill };
            albumsContainer.Controls.Add(albumsSection);      // Fill
            albumsContainer.Controls.Add(albumsTitleLabel);   // Top

            // --- PAGES ---
            var pagesTitleLabel = new Label
            {
                Text = "Pages you like",
                Dock = DockStyle.Top,
                Height = 25,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(5, 2, 0, 0)
            };
            var pagesSection = new ItemGalleryComponent { Dock = DockStyle.Fill };

            var pagesContainer = new Panel { Dock = DockStyle.Fill };
            pagesContainer.Controls.Add(pagesSection);        // Fill
            pagesContainer.Controls.Add(pagesTitleLabel);     // Top

            tlpRight.Controls.Add(albumsContainer, 0, 0);
            tlpRight.Controls.Add(pagesContainer, 0, 1);

            splitContainer.Panel2.Controls.Add(tlpRight);

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
                foreach (Post post in posts)
                {
                    PostComponent postControl = new PostComponent
                    {
                        Margin = new Padding(5, 5, 5, 15),
                        PostNotesManager = r_PostNotesManager,
                        PostTagsManager = r_PostTagsManager
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

            var headerLabel = new Label
            {
                Text = "Feed",
                Dock = DockStyle.Top,
                Height = 45,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Padding = new Padding(10, 5, 0, 5)
            };

            var postsFlowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10, 5, 10, 10) 
            };

            feedPanel.SuspendLayout();
            feedPanel.Controls.Add(postsFlowPanel); // Fill
            feedPanel.Controls.Add(headerLabel);    // Top
            feedPanel.ResumeLayout();

            if (r_LoggedInUser.Friends != null && r_LoggedInUser.Friends.Count > 0)
            {
                foreach (User friend in r_LoggedInUser.Friends)
                {
                    if (friend?.Posts == null) continue;

                    foreach (Post post in friend.Posts)
                    {
                        if (post == null) continue;

                        var postControl = new PostComponent
                        {
                            Margin = new Padding(5, 5, 5, 15),
                            PostNotesManager = r_PostNotesManager,
                            PostTagsManager = r_PostTagsManager
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

                showPage(m_FeedPage);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    @"An error occurred while loading the feed.{Environment.NewLine} {ex.Message}",
                    "Feed error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        
        private void buttonTagsAnalytics_Click(object sender, EventArgs e)
        {
            updateAnalyticsPage();
            showPage(m_TagsAnalyticsPage);
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Control buildTagsAnalyticsPage()
        {
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;

            // Header
            Label headerLabel = new Label();
            headerLabel.Text = "Tags Analytics";
            headerLabel.Dock = DockStyle.Top;
            headerLabel.Height = 40;
            headerLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            headerLabel.Padding = new Padding(10, 5, 0, 0);
            mainPanel.Controls.Add(headerLabel);

            // Info label
            Label infoLabel = new Label();
            infoLabel.Dock = DockStyle.Top;
            infoLabel.Height = 30;
            infoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            infoLabel.Padding = new Padding(10, 5, 0, 0);
            mainPanel.Controls.Add(infoLabel);
            mainPanel.Controls.SetChildIndex(infoLabel, 1);   // under header

            // Chart
            Chart tagsChart = new Chart();
            tagsChart.Width = 500;
            tagsChart.Height = 350;
            tagsChart.Anchor = AnchorStyles.Top;
            tagsChart.Top = 80;

            tagsChart.Left = (mainPanel.Width - tagsChart.Width) / 2;
            mainPanel.Resize += delegate
                {
                    tagsChart.Left = (mainPanel.Width - tagsChart.Width) / 2;
                };

            ChartArea chartArea = new ChartArea("TagsArea");
            tagsChart.ChartAreas.Add(chartArea);

            Series series = new Series("Tags");
            series.ChartType = SeriesChartType.Pie;
            series.YValueType = ChartValueType.Int32;

            series.IsValueShownAsLabel = true;
            series.Label = "#AXISLABEL (#PERCENT{P0})";
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";

            tagsChart.Series.Add(series);

            mainPanel.Controls.Add(tagsChart);
            mainPanel.Controls.SetChildIndex(tagsChart, 2);

            // keep references for later updates
            m_TagsChart = tagsChart;
            m_TagsInfoLabel = infoLabel;

            return mainPanel;
        }

        private void updateAnalyticsPage() 
        {
            bool chartReady = m_TagsChart != null && m_TagsInfoLabel != null;

            if (chartReady)
            {
                Series series = m_TagsChart.Series[0];
                series.Points.Clear();

                ICollection<string> allTags = r_PostTagsManager.GetAllTags();
                Dictionary<string, int> tagsCountDictionary =
                    new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                int total = 0;

                if (allTags != null)
                {
                    foreach (string tag in allTags)
                    {
                        if (!string.IsNullOrEmpty(tag))
                        {
                            int currentTagCount;

                            if (tagsCountDictionary.TryGetValue(tag, out currentTagCount))
                            {
                                tagsCountDictionary[tag] = currentTagCount + 1;
                            }
                            else
                            {
                                tagsCountDictionary[tag] = 1;
                            }

                            total++;
                        }
                    }
                }

                if (total == 0)
                {
                    m_TagsInfoLabel.Text =
                        "No tags to display yet. Add tags to your posts first.";
                }
                else
                {
                    m_TagsInfoLabel.Text =
                        "Showing distribution of all tags by percentage.";

                    foreach (KeyValuePair<string, int> pair in tagsCountDictionary)
                    {
                        string tagName = pair.Key;
                        int count = pair.Value;

                        DataPoint point = new DataPoint();
                        point.YValues = new double[] { count };
                        point.AxisLabel = tagName;
                        series.Points.Add(point);
                    }
                }
            }
        }
    }
}
