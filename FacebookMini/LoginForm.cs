using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookMini.Logic;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookMini
{
    public partial class LoginForm : Form
    {
        private const string k_AppId = "806995989056767";
        private readonly string[] r_RequestedPermissions =
            {   
                "public_profile",
                "email",
                "user_friends",
                "user_posts",
                "user_photos",
                "user_events",
                "email",
                "user_age_range",
                "user_birthday",
                "user_gender",
                "user_hometown",
                "user_likes",
                "user_link",
                "user_location",
                "user_videos"

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
                m_LoginResult = FacebookService.Login(k_AppId, r_RequestedPermissions);

                //m_LoginResult = FacebookService.Connect()

                if (string.IsNullOrEmpty(m_LoginResult.ErrorMessage))
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
                this.Enabled = true;
                //Try again or exit
            }


        }

        private void buttonConnectAsDesig_Click(object sender, EventArgs e)
        {
            try
            {
                m_LoginResult = FacebookService.Connect("EAAUm6cZC4eUEBPZCFs9rJRpwlUmdHcPvU1tUNkIyP37zRZCjSvfdHaW5t3xsOnUL0bEKHL8Snjk6AZC3O32KWEbaItglEnXWQ2zEMXHqsdfdv0ecXNs3hO69juHrZCfRN9FGvfuJZAXhP4Pm57DRRoDeB8De6ZABnfrRflh6zgPwnavpyHS3ZCYX1E6K1QLTHff5sAZDZD");
                openMainForm(m_LoginResult.LoggedInUser);
            }
            catch (Exception ex)
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

            Close();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.LogoutWithUI();
            buttonLogin.Text = "Login";
            buttonLogin.BackColor = buttonLogout.BackColor;
            m_LoginResult = null;
            buttonLogin.Enabled = true;
            buttonLogout.Enabled = false;
        }
    }
}
