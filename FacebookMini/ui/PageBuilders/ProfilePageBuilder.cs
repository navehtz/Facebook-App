using FacebookMini.CustomComponent;
using FacebookMini.ui.CustomComponent;
using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FacebookMini.ui.Adapters;


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
            m_UserInfoPanel = new Panel
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
                Image = r_Context.LoggedInUser.ImageNormal
                        ?? FacebookMini.Properties.Resources.Facebook_default_male_avatar
            };

            if (!string.IsNullOrEmpty(r_Context.LoggedInUser.PictureNormalURL))
            {
                try
                {
                    userPictureBox.LoadAsync(r_Context.LoggedInUser.PictureNormalURL);
                    r_Context.UserPictureBoxTopBar.Image = userPictureBox.Image;
                }
                catch { }
            }

            var userNameLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(110, 20),
                Text = r_Context.LoggedInUser.Name
            };

            string extraInfo = string.Empty;

            if (!string.IsNullOrEmpty(r_Context.LoggedInUser.Email))
            {
                extraInfo += r_Context.LoggedInUser.Email;
            }

            if (r_Context.LoggedInUser.Birthday != null)
            {
                if (extraInfo.Length > 0) extraInfo += "   |   ";
                extraInfo += $"Birthday: {r_Context.LoggedInUser.Birthday}";
            }

            if (r_Context.LoggedInUser.Location != null &&
                !string.IsNullOrEmpty(r_Context.LoggedInUser.Location.Name))
            {
                if (extraInfo.Length > 0) extraInfo += "   |   ";
                extraInfo += r_Context.LoggedInUser.Location.Name;
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
            m_ProfilePanel.Controls.SetChildIndex(m_UserInfoPanel, 0); // מתחת ל-header
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

            // Same resize logic you had
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

        //public void BindData()
        //{
        //    bindPosts();
        //    bindAlbums();
        //    bindPages();
        //}

        private void bindPosts()
        {
            var posts = r_Context.AppLogic.GetUserPosts();
            if (posts == null) return;

            foreach (Post post in posts)
            {
                var postControl = new PostComponent
                {
                    Margin = new Padding(5, 5, 5, 15),
                    PostNotesManager = r_Context.NotesManager,
                    PostTagsManager = r_Context.TagsManager
                };
              
                IPostData postData = new FacebookPostAdapter(post, r_Context.LoggedInUser);
                postControl.SetPost(postData);

                m_PostsFlowPanel.Controls.Add(postControl);
            }
        }
        
        private void bindAlbums()
        {
            var albumsItems = new List<GalleryItem>();
            var albums = r_Context.AppLogic.GetUserAlbums();

            if (albums != null)
            {
                foreach (Album album in albums)
                {
                    albumsItems.Add(new GalleryItem
                    {
                        Title = album.Name,
                        Image = album.ImageAlbum,
                        Tag = album,
                        ItemType = eGalleryItemType.Album
                    });
                }
            }

            m_AlbumsSection.SetItems(albumsItems);

            if (albumsItems.Count == 0)
            {
                m_AlbumsSection.Visible = false;
                m_AlbumsTitleLabel.Visible = false;
                m_AlbumsSection.Height = 0;
            }
        }

        private void bindPages()
        {
            var pagesItems = new List<GalleryItem>();
            var likedPages = r_Context.AppLogic.GetUserLikedPages();

            if (likedPages != null)
            {
                foreach (Page page in likedPages)
                {
                    pagesItems.Add(new GalleryItem
                    {
                        Title = page.Name,
                        Image = page.ImageNormal,
                        Tag = page,
                        ItemType = eGalleryItemType.Page
                    });
                }
            }

            m_PagesSection.SetItems(pagesItems);
        }

        public Control GetResult()
        {
            return m_ProfilePanel;
        }
    }
}
