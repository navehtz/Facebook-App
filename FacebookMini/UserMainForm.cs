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
using FacebookMini.Logic;
using FacebookMini.MyComponents;

namespace FacebookMini
{
    public partial class UserMainForm : Form
    {
        private readonly User r_LoggedInUser;
        //private readonly AnalyticsManager r_AnalyticsManager;
        //private readonly ThemeManager r_ThemeManager = new ThemeManager();
        
        public UserMainForm(User i_LoggedInUser)
        {
            r_LoggedInUser = i_LoggedInUser;
            //r_AnalyticsManager = new AnalyticsManager(r_LoggedInUser);
            
            InitializeComponent();
        }

        

        private void UserMainForm_Load(object sender, EventArgs e)
        {
          
            
            //postView1.SetPost(r_AnalyticsManager.MostLikedPost, "most liked post");
            //postView2.SetPost(r_AnalyticsManager.LatestPostLikedByUser, "latest post liked by user");
            //postView3.SetPost(r_AnalyticsManager.MostLikedPhoto, "most liked photo");
            //postView4.SetPost(r_AnalyticsManager.LatestPhotoLikedByUser, "latest photo liked by user");
        }

        private void selectionWithImagePreview1_Load(object sender, EventArgs e)
        {
            selectionWithImagePreview1.SetData(r_LoggedInUser.LikedPages, "Pages i liked:", obj => ((Page)obj).Name, obj => ((Page)obj).PictureNormalURL);
        }

        private void selectionWithImagePreview2_Load(object sender, EventArgs e)
        {
            //selectionWithImagePreview2.SetData(r_LoggedInUser.Albums.SelectMany(a => a.Photos) , "My photos:", obj => ((Photo)obj).Name, obj => ((Photo)obj).PictureNormalURL);
        }

        
        private void selectionWithImagePreview3_Load(object sender, EventArgs e)
        {

        }

    }
}
