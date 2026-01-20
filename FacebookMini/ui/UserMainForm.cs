using Facebook;
using FacebookMini.ui.CustomComponent;
using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;
using FacebookMini.Logic;
using FacebookMini.ui.CustomComponent;
using FacebookMini.ui.PageBuilder;
using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CefSharp;
using FacebookMini.shared.adapters;
using FacebookMini.shared.galleryItem;
using IPostData = FacebookMini.shared.adapters.IPostData;

namespace FacebookMini.ui
{
    public partial class UserMainForm : Form
    {
        private readonly User r_LoggedInUser;
        private readonly IFacebookAppLogic r_AppLogic;

        private Control m_ProfilePage;
        private Control m_FeedPage;
        
        private Control m_TagsAnalyticsPage;
        private Chart m_TagsChart;
        private Label m_TagsInfoLabel;


        private PageComposer m_Composer;
        private PageBuildContext m_Context;

        private bool m_ProfileLoaded = false;
        private bool m_FeedLoaded = false;

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
            m_Context = new PageBuildContext(r_AppLogic, userPictureBoxTopBar);
            m_Composer = new PageComposer(m_Context, new ProfilePageBuilder());

            buildPages();
            showPage(m_ProfilePage);// defult

            startProfileAsyncLoaders();
        }

        private void buildPages()
        {
            m_ProfilePage = m_Composer.Compose();

            m_Composer.Builder = new FeedPageBuilder();
            m_FeedPage = m_Composer.Compose();
        }


        private void startProfileAsyncLoaders()
        {
            if (m_ProfileLoaded)
            {
                return;
            }

            m_ProfileLoaded = true;

            new Thread(fetchProfilePostsAsync).Start();
            new Thread(fetchAlbumsAsync).Start();
            new Thread(fetchPagesAsync).Start();            
        }

        private void startFeedAsynceLoaders() 
        {
            if (m_FeedLoaded)
            {
                return;
            }

            m_FeedLoaded = true;

            new Thread(fetchFeedPostsAsync).Start();
        }

        private void fetchProfilePostsAsync()
        {
            FlowLayoutPanel profilePagePostsFlow = m_ProfilePage.Controls.Find("ProfilePostsFlow", true).FirstOrDefault() as FlowLayoutPanel;
           
            if (profilePagePostsFlow == null)
            {
                return;
            }

            IEnumerable<IPostData> profilePosts = r_AppLogic.GetMyPostsData();

            foreach (IPostData postData in profilePosts)
            {
                if (postData == null)
                {
                    continue;
                }

                profilePagePostsFlow.BeginInvoke(new Action(() =>
                {
                    PostComponent postControl = new PostComponent
                    {
                        Margin = new Padding(5, 5, 5, 15),
                        AppLogic = r_AppLogic
                    };

                    postControl.SetPost(postData);
                    profilePagePostsFlow.Controls.Add(postControl);
                }));
            }
        }

        private void fetchFeedPostsAsync() 
        {
            FlowLayoutPanel feedPagePostsFlow = m_FeedPage.Controls.Find("feedPostsFlow", true).FirstOrDefault() as FlowLayoutPanel;

            if (feedPagePostsFlow == null)
            {
                return;
            }

            if (r_LoggedInUser.Friends == null || r_LoggedInUser.Friends.Count == 0)
            {
                return;
            }

            IEnumerable<IPostData> friendsPosts = r_AppLogic.GetFriendsFeedPostsData();
            
            foreach (IPostData postOfFriend in friendsPosts)
            {
                feedPagePostsFlow.BeginInvoke(
                    new Action(() =>
                    {
                        var postControl = new PostComponent
                              {
                                  Margin = new Padding(5, 5, 5, 15), AppLogic = r_AppLogic
                              };

                        postControl.SetPost(postOfFriend);
                        feedPagePostsFlow.Controls.Add(postControl);
                    }));
            }
        }

        private void fetchAlbumsAsync()
        {
            ItemGalleryComponent albumsGallery = m_ProfilePage.Controls.Find("ProfileAlbumsGallery", true).FirstOrDefault() as ItemGalleryComponent;

            if (albumsGallery == null)
            {
                return;
            }

            List<GalleryItem> albumsItems = r_AppLogic.GetAlbumsGalleryItems();

            if (albumsItems != null)
            {
                foreach (GalleryItem album in albumsItems)
                {
                    albumsGallery.BeginInvoke(new Action(() =>
                        {
                            albumsGallery.SetItem(album);
                        }));
                }
            }
        }

        private void fetchPagesAsync()
        {
            ItemGalleryComponent likedPagesGallery = m_ProfilePage.Controls.Find("ProfilePagesGallery", true).FirstOrDefault() as ItemGalleryComponent;

            if (likedPagesGallery == null)
            {
                return;
            }

            List<GalleryItem> pagesItems = r_AppLogic.GetLikedPagesGalleryItems();

            if (pagesItems != null)
            {
                foreach (GalleryItem page in pagesItems)
                {
                    likedPagesGallery.BeginInvoke(new Action(() =>
                        {
                            likedPagesGallery.SetItem(page);
                        }));
                }
            }
        }

        /// <summary>
        /// Profile page example: posts component + item gallery (albums / liked pages).
        /// </summary>

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
                startFeedAsynceLoaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while loading the feed.{Environment.NewLine} {ex.Message}",
                    "Feed error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        
        private void buttonTagsAnalytics_Click(object sender, EventArgs e)
        {
            m_Composer.Builder = new TagsAnalyticsPageBuilder();
            m_TagsAnalyticsPage = m_Composer.Compose();
            showPage(m_TagsAnalyticsPage);
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}