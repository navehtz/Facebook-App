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
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
            m_Composer = new PageComposer(m_Context);

            buildPages();
            showPage(m_ProfilePage);// defult
        }

        private void buildPages()
        {
            m_ProfilePage = m_Composer.Compose(new ProfilePageBuilder(m_Context));
            m_FeedPage = m_Composer.Compose(new FeedPageBuilder(m_Context));
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