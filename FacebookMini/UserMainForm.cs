using System;
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
            var panel = new Panel { Dock = DockStyle.Fill };

            // top title
            var labelHeader = new Label
            {
                Text = "Profile",
                Dock = DockStyle.Top,
                Height = 40,
                Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(10, 5, 0, 0)
            };
            panel.Controls.Add(labelHeader);

            // layout for posts + gallery
            var splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                Panel1MinSize = 200,
                Panel2MinSize = 200
            };

            // LEFT: posts component – using your SelectionWithImagePreview
            var postsComponent = new PostComponent()
            {
                Dock = DockStyle.Fill,
                TitleText = "Posts"
            };

            // RIGHT: item gallery (albums + liked pages)
            var itemGallery = new ItemGalleryComponent
            {
                Dock = DockStyle.Fill
            };

            splitContainer.Panel1.Controls.Add(postsComponent);
            splitContainer.Panel2.Controls.Add(itemGallery);

            panel.Controls.Add(splitContainer);
            panel.Controls.SetChildIndex(labelHeader, 1); // keep header on top

            // later in your logic you can assign:
            // postsComponent.DataSource = userPosts;
            // itemGallery.SetItems(userAlbumsAndLikedPages);

            return panel;
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
