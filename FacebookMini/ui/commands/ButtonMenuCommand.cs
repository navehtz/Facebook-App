using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMini.ui.commands
{
    public class ButtonMenuCommand
    {
        private readonly Button r_Button;
        private Action CommandAction { get; set; }

        public ButtonMenuCommand(Button i_Button, Action i_Action)
        {
            r_Button = i_Button ?? throw new ArgumentNullException(nameof(i_Button));
            CommandAction = i_Action ?? throw new ArgumentNullException(nameof(i_Action));

            r_Button.Click += Button_OnClick;
        }

        private void Button_OnClick(object sender, EventArgs e)
        {
            try
            {
                CommandAction.Invoke();
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while executing the command: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}