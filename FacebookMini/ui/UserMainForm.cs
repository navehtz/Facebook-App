using Facebook;
using FacebookMini.CustomComponent;
using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;
using FacebookMini.Logic;
using FacebookMini.ui.Adapters;
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

namespace FacebookMini.ui
{
    public partial class UserMainForm : Form
    {
        private readonly User r_LoggedInUser;
        private readonly IFacebookAppLogic r_AppLogic;
        private readonly IPostNotesManager r_PostNotesManager;
        private readonly IPostTagsManager r_PostTagsManager;

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
            r_PostNotesManager = r_AppLogic.PostNotesManager;
            r_PostTagsManager = r_AppLogic.PostTagsManager;
        }

        private void UserMainForm_Load(object sender, EventArgs e)
        {
            m_Context = new PageBuildContext(r_AppLogic, r_LoggedInUser, r_PostNotesManager, r_PostTagsManager, userPictureBoxTopBar);
            m_Composer = new PageComposer(m_Context);

            buildPages();
            showPage(m_ProfilePage);// defult

            startProfileAsyncLoaders();
        }

        private void buildPages()
        {
            m_ProfilePage = m_Composer.Compose(new ProfilePageBuilder(m_Context));
            m_FeedPage = m_Composer.Compose(new FeedPageBuilder(m_Context));
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

            var profilePosts = r_AppLogic.GetUserPosts();
            foreach (Post post in profilePosts)
            {
                if (post == null)
                {
                    continue;
                }
                profilePagePostsFlow.Invoke(new Action(() =>
                {
                    PostComponent postControl = new PostComponent
                    {
                        Margin = new Padding(5, 5, 5, 15),
                        PostNotesManager = r_PostNotesManager,
                        PostTagsManager = r_PostTagsManager
                    };

                    IPostData postData = new FacebookPostAdapter(post, r_LoggedInUser);
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

            HashSet<KeyValuePair<Post, User>> friendsPosts = new HashSet<KeyValuePair<Post, User>>();

            foreach (User friend in r_LoggedInUser.Friends)
            {
                if (friend?.Posts == null) continue;

                foreach (Post post in friend.Posts)
                {
                    if (post == null) 
                    {
                        continue;
                    }

                    friendsPosts.Add(new KeyValuePair<Post, User>(post, friend));
                }
            }

            foreach (KeyValuePair<Post, User> postOfFriend in friendsPosts)
            {
                feedPagePostsFlow.Invoke(new Action(() =>
                {
                    var postControl = new PostComponent
                    {
                        Margin = new Padding(5, 5, 5, 15),
                        PostNotesManager = r_PostNotesManager,
                        PostTagsManager = r_PostTagsManager
                    };

                    IPostData postData = new FacebookPostAdapter(postOfFriend.Key, postOfFriend.Value);
                    postControl.SetPost(postData);

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

            var albums = r_AppLogic.GetUserAlbums();

            if (albums != null)
            {
                foreach (Album album in albums)
                {
                    albumsGallery.Invoke(new Action(() =>
                    {

                        albumsGallery.SetItem(new GalleryItem
                        {
                            Title = album.Name,
                            Image = album.ImageAlbum,
                            Tag = album,
                            ItemType = eGalleryItemType.Album
                        });
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

            var pages = r_AppLogic.GetUserLikedPages();

            if (pages != null)
            {
                foreach (Page page in pages)
                {
                    likedPagesGallery.Invoke(new Action(() =>
                    {

                        likedPagesGallery.SetItem(new GalleryItem
                        {
                            Title = page.Name,
                            Image = page.ImageNormal,
                            Tag = page,
                            ItemType = eGalleryItemType.Page
                        });
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
            m_TagsAnalyticsPage = m_Composer.Compose(new TagsAnalyticsPageBuilder(m_Context));
            showPage(m_TagsAnalyticsPage);
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // TODO: Separate ui from count total tags logic.
    }
}