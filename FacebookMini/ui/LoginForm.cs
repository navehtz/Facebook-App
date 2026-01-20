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

namespace FacebookMini.ui
{
    public partial class LoginForm : Form
    {
        private FacebookWrapper.LoginResult m_LoginResult;

        private readonly string[] r_RequestedPermissions =
            {   
                "public_profile",
                "email",
                "user_friends",
                "user_posts",
                "user_photos",
                "user_birthday",
                "user_hometown",
                "user_likes"
            };

        public LoginForm()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;
        }

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
                showLoadingUi("Logging in to Facebook...");

                m_LoginResult = FacebookService.Login(textBoxAppID.Text, r_RequestedPermissions);

                if (string.IsNullOrEmpty(m_LoginResult.AccessToken) == false)
                {
                    showLoadingUi("Opening main window...");
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
            finally
            {
                hideLoadingUi();
            }
        }

        private void buttonConnectAsDesig_Click(object sender, EventArgs e)
        {
            try
            {
                showLoadingUi("Logging in to Facebook...");
                m_LoginResult = FacebookService.Connect("EAAUm6cZC4eUEBQTAa3rRgO39UZCIJLeD9OpF5SYAevqSaFI16sfjT6JznpAUbyX5Soyj4Uv2ZBRkesoHO9omNcJ3KSYPZCExgaKrIprACUMIVnhiHzT5a46zbdC2VkvZC04n1ZARj8WmvOCYyuIdmRZBNjtWZCFJrbjFoms5t3sU8G9dO1xDCYH7kkfU67heIUZCFDIuTtL0CzF2JUHBpRpwPdXYilOJW811z3C5fY9TOyBiUwZAqx4ZAV6YS5ZBBtYKdsb7");
                showLoadingUi("Opening main window...");
                openMainForm(m_LoginResult.LoggedInUser);
            }
            catch 
            {
                MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed");
                executeLogout();
                this.Enabled = true;
            }
            finally
            {
                hideLoadingUi();
            }
        }

        private void openMainForm(User i_LoggedInUser)
        {
            IFacebookAppLogic appLogic = new FacebookAppLogic(i_LoggedInUser);

            using (UserMainForm userMainForm = new UserMainForm(appLogic))
            {
                userMainForm.Shown += (i_Sender, i_Args) => this.Hide();
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

        private void showLoadingUi(string i_Text)
        {
            labelLoading.Text = i_Text;
            labelLoading.Visible = true;
            progressBarLoading.Visible = true;

            this.UseWaitCursor = true;

            // Force paint NOW before the blocking work starts
            labelLoading.Refresh();
            progressBarLoading.Refresh();
            this.Refresh();
            Application.DoEvents();
        }

        private void hideLoadingUi()
        {
            this.UseWaitCursor = false;
            progressBarLoading.Visible = false;
            labelLoading.Visible = false;
        }
    }
}
