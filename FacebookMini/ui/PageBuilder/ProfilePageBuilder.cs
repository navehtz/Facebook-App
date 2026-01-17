using FacebookMini.ui.CustomComponent;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FacebookMini.shared.adapters;
using FacebookMini.shared.galleryItem;
using FacebookMini.shared.profileData;


namespace FacebookMini.ui.PageBuilder
{
    public class ProfilePageBuilder : IPageBuilder
    {
        private readonly PageBuildContext r_Context;

        private Panel m_ProfilePanel;
        private Label m_LabelHeader;
        private Panel m_UserInfoPanel;

        private SplitContainer m_SplitContainer;
        private FlowLayoutPanel m_PostsFlowPanel;

        private ItemGalleryComponent m_AlbumsSection;
        private ItemGalleryComponent m_PagesSection;
        private Label m_AlbumsTitleLabel;
        private Label m_PagesTitleLabel;

        public ProfilePageBuilder(PageBuildContext i_Context)
        {
            r_Context = i_Context;
        }

        public void Reset()
        {
            m_ProfilePanel = new Panel { Dock = DockStyle.Fill };
        }

        public void BuildHeader()
        {
            m_LabelHeader = new Label
            {
                Text = "Profile",
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Padding = new Padding(10, 5, 0, 5)
            };

            m_ProfilePanel.Controls.Add(m_LabelHeader);
        }

        public void BuildBody()
        {
            buildUserInfoPanel();
            buildSplitContent();
            namingSectionsAndPanels();
        }

        private void buildUserInfoPanel()
        {
            UserProfileData userData = r_Context.AppLogic.GetLoggedInUserProfileData();

            m_UserInfoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                Padding = new Padding(10, 5, 10, 5)
            };

            PictureBox userPictureBox = new PictureBox
            {
                Size = new Size(80, 80),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(10, 10),
                Image = Properties.Resources.Facebook_default_male_avatar
            };

            if (!string.IsNullOrEmpty(userData.ProfilePictureUrl))
            {
                try
                {
                    userPictureBox.LoadCompleted += (s, e) =>
                        {
                            if(userPictureBox.Image != null)
                            {
                                r_Context.UserPictureBoxTopBar.Image = userPictureBox.Image;
                            }
                        };

                    userPictureBox.LoadAsync(userData.ProfilePictureUrl);
                }
                catch { }
            }

            var userNameLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(110, 20),
                Text = userData.Name
            };

            string extraInfo = string.Empty;

            if (!string.IsNullOrEmpty(userData.Email))
            {
                extraInfo += userData.Email;
            }

            if (!string.IsNullOrEmpty(userData.Birthday))
            {
                if (extraInfo.Length > 0) extraInfo += "   |   ";
                extraInfo += $"Birthday: {userData.Birthday}";
            }

            if (!string.IsNullOrEmpty(userData.LocationName))
            {
                if (extraInfo.Length > 0) extraInfo += "   |   ";
                extraInfo += userData.LocationName;
            }

            var userExtraLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(110, 55),
                Text = extraInfo
            };

            m_UserInfoPanel.Controls.Add(userPictureBox);
            m_UserInfoPanel.Controls.Add(userNameLabel);
            m_UserInfoPanel.Controls.Add(userExtraLabel);

            m_ProfilePanel.Controls.Add(m_UserInfoPanel);
            m_ProfilePanel.Controls.SetChildIndex(m_UserInfoPanel, 0);
        }

        private void buildSplitContent()
        {
            m_SplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical
            };

            // LEFT: posts
            var postsSectionPanel = new Panel { Dock = DockStyle.Fill };

            var postsTitleLabel = new Label
            {
                Text = "Posts",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Padding = new Padding(5, 5, 0, 0)
            };

            m_PostsFlowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10, 5, 10, 10)
            };

            postsSectionPanel.Controls.Add(m_PostsFlowPanel);
            postsSectionPanel.Controls.Add(postsTitleLabel);
            m_SplitContainer.Panel1.Controls.Add(postsSectionPanel);

            // RIGHT: albums + pages
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

            // Albums
            m_AlbumsTitleLabel = new Label
            {
                Text = "Albums",
                Dock = DockStyle.Top,
                Height = 25,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(5, 2, 0, 0)
            };
            m_AlbumsSection = new ItemGalleryComponent { Dock = DockStyle.Fill };

            var albumsContainer = new Panel { Dock = DockStyle.Fill };
            albumsContainer.Controls.Add(m_AlbumsSection);
            albumsContainer.Controls.Add(m_AlbumsTitleLabel);

            // Pages
            m_PagesTitleLabel = new Label
            {
                Text = "Pages you like",
                Dock = DockStyle.Top,
                Height = 25,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(5, 2, 0, 0)
            };
            m_PagesSection = new ItemGalleryComponent { Dock = DockStyle.Fill };

            var pagesContainer = new Panel { Dock = DockStyle.Fill };
            pagesContainer.Controls.Add(m_PagesSection);
            pagesContainer.Controls.Add(m_PagesTitleLabel);

            tlpRight.Controls.Add(albumsContainer, 0, 0);
            tlpRight.Controls.Add(pagesContainer, 0, 1);
            m_SplitContainer.Panel2.Controls.Add(tlpRight);

            m_ProfilePanel.Controls.Add(m_SplitContainer);
            m_ProfilePanel.Controls.SetChildIndex(m_SplitContainer, 0); // Fill

            m_ProfilePanel.Resize += (sender, args) =>
            {
                if (m_ProfilePanel.Width > 0)
                {
                    try { m_SplitContainer.SplitterDistance = (int)(m_ProfilePanel.Width * 0.6); }
                    catch { }
                }
            };
        }

        private void namingSectionsAndPanels() 
        {
            m_PostsFlowPanel.Name = "ProfilePostsFlow";
            m_AlbumsSection.Name = "ProfileAlbumsGallery";
            m_PagesSection.Name = "ProfilePagesGallery";
        }

        public Control GetResult()
        {
            return m_ProfilePanel;
        }
    }
}
