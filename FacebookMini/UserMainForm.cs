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
        private readonly FavoritesManager r_FavouritesManager;
        
        public UserMainForm(User i_LoggedInUser)
        {
            r_LoggedInUser = i_LoggedInUser;
            r_FavouritesManager = new FavoritesManager();
            
            InitializeComponent();
        }

        

        private void UserMainForm_Load(object sender, EventArgs e)
        {
          
        }

        private void selectionWithImagePreview1_Load(object sender, EventArgs e)
        {
            selectionWithImagePreview1.SetData(r_LoggedInUser.LikedPages, "Pages i liked:", obj => ((Page)obj).Name, obj => ((Page)obj).PictureNormalURL);
        }

        private void selectionWithImagePreview2_Load(object sender, EventArgs e)
        {
            r_FavouritesManager.Add(r_LoggedInUser.LikedPages.First(), eFavoritesCategory.LikedPages);
            selectionWithImagePreview2.SetData(r_FavouritesManager.GetList(eFavoritesCategory.LikedPages), "favorites paSges:", obj => ((Page)obj).Name, obj => ((Page)obj).PictureNormalURL);
        }

    }
}
