using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace FacebookMini
{
    public partial class UserMainForm : Form
    {
        private readonly User r_LoggedInUser;
        private readonly AnalyticsManager r_AnalyticsManager;
        //private readonly ThemeManager r_ThemeManager = new ThemeManager();

        public UserMainForm(User i_LoggedInUser)
        {
            r_LoggedInUser = i_LoggedInUser;
            r_AnalyticsManager = new AnalyticsManager(r_LoggedInUser);

            InitializeComponent();

            initializeProfileTab();
            initializeAnalyticsTab();
            //initializeThemeControls();
            //applyCurrentTheme();
        }

        private void initializeProfileTab()
        {
            labelUserName.Text =
                string.Format("{0} {1}", r_LoggedInUser.FirstName, r_LoggedInUser.LastName);

            pictureBoxProfile.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxProfile.LoadAsync(r_LoggedInUser.PictureNormalURL);

            listBoxFriends.DisplayMember = "Name";
            listBoxFriends.DataSource = r_LoggedInUser.Friends;

            listBoxPosts.DisplayMember = "Message";
            listBoxPosts.DataSource = r_LoggedInUser.Posts;
        }

        private void initializeAnalyticsTab()
        {
            buttonRefreshAnalytics.Text = "Refresh Analytics";
            labelMostLikedPost.Text = "Most liked post: -";
            labelMostLikedPhoto.Text = "Most liked photo: -";
        }

        //private void initializeThemeControls()
        //{
        //    //comboBoxThemes.DataSource = r_ThemeManager.GetAvailableThemes();
        //    comboBoxThemes.DisplayMember = "Name";

        //    buttonApplyTheme.Text = "Apply Theme";
        //}

        private void buttonRefreshAnalytics_Click(object i_Sender, EventArgs i_E)
        {
            refreshAnalyticsSummary();
        }

        private void refreshAnalyticsSummary()
        {
            Post mostLikedPost = r_AnalyticsManager.GetMostLikedPost();
            Photo mostLikedPhoto = r_AnalyticsManager.GetMostLikedPhoto();

            labelMostLikedPost.Text = mostLikedPost != null
                ? string.Format("Most liked post: {0} (likes: {1})",
                    trimForDisplay(mostLikedPost.Message),
                    mostLikedPost.LikedBy?.Count ?? 0)
                : "Most liked post: -";

            labelMostLikedPhoto.Text = mostLikedPhoto != null
                ? string.Format("Most liked photo: {0} (likes: {1})",
                    trimForDisplay(mostLikedPhoto.Name),
                    mostLikedPhoto.LikedBy?.Count ?? 0)
                : "Most liked photo: -";

            listBoxTopPosts.DisplayMember = "Message";
            listBoxTopPosts.DataSource = r_AnalyticsManager.GetTopPostsByLikes(5).ToList();
        }

        private string trimForDisplay(string i_Text)
        {
            if (string.IsNullOrEmpty(i_Text))
            {
                return "[no text]";
            }

            const int k_MaxLength = 40;

            return i_Text.Length <= k_MaxLength
                ? i_Text
                : i_Text.Substring(0, k_MaxLength) + "...";
        }

        private void buttonApplyTheme_Click(object i_Sender, EventArgs i_E)
        {
            Theme selectedTheme = comboBoxThemes.SelectedItem as Theme;

            if (selectedTheme != null)
            {
                r_ThemeManager.SetTheme(selectedTheme);
                applyCurrentTheme();
            }
        }

        private void applyCurrentTheme()
        {
            r_ThemeManager.ApplyTheme(this);
        }

        private void UserMainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
