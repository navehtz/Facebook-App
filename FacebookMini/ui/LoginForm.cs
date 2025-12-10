using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookMini.Logic;
using FacebookWinFormsApp.utils;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookMini
{
    public partial class LoginForm : Form
    {
        private readonly string[] r_RequestedPermissions =
            {   
                "public_profile",
                "email",
                "user_friends",
                "user_posts",
                "user_photos",
                "email",
                "user_age_range",
                "user_birthday",
                "user_gender",
                "user_hometown",
                "user_likes",
                "user_link",
            };
        public LoginForm()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;
        }

        FacebookWrapper.LoginResult m_LoginResult;

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("design.patterns");

            if (m_LoginResult == null)
            {
                login();
            }
        }

        private void login()
        {
            this.Enabled = false;

            try
            {
                m_LoginResult = FacebookService.Login(Constants.k_AppId, r_RequestedPermissions);

                if (string.IsNullOrEmpty(m_LoginResult.AccessToken) == false)
                {
                    openMainForm(m_LoginResult.LoggedInUser);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed");
                executeLogout();
                this.Enabled = true;
            }


        }

        private void buttonConnectAsDesig_Click(object sender, EventArgs e)
        {
            try
            {
                m_LoginResult = FacebookService.Connect("EAAUm6cZC4eUEBPZCFs9rJRpwlUmdHcPvU1tUNkIyP37zRZCjSvfdHaW5t3xsOnUL0bEKHL8Snjk6AZC3O32KWEbaItglEnXWQ2zEMXHqsdfdv0ecXNs3hO69juHrZCfRN9FGvfuJZAXhP4Pm57DRRoDeB8De6ZABnfrRflh6zgPwnavpyHS3ZCYX1E6K1QLTHff5sAZDZD");
                openMainForm(m_LoginResult.LoggedInUser);
            }
            catch 
            {
                MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed");
            }
        }

        private void openMainForm(User i_LoggedInUser)
        {
            IFacebookAppLogic appLogic = new FacebookAppLogic(i_LoggedInUser);

            using (UserMainForm userMainForm = new UserMainForm(appLogic))
            {
                Hide();
                userMainForm.ShowDialog();
            }

            executeLogout();
            Show();
            this.Enabled = true;
        }
        private void executeLogout()
        {
            try
            {
                FacebookService.Logout();
                m_LoginResult = null;
            }
            catch 
            {
                throw new Exception();
            }
        }
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
