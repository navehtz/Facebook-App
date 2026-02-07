using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMini.ui.commands
{
    public class CommandObserverForButton : CommandObserver
    {
        private static readonly Color sr_NormalBack = Color.FromArgb(36, 37, 38);
        private static readonly Color sr_SelectedBack = Color.FromArgb(60, 60, 60);

        private Button Button
        {
            get { return (Button)m_Control; }
        }

        public CommandObserverForButton(Button i_Button)
            : base(i_Button)
        { }

        protected override void UpdateSpecific()
        {
            Button.UseVisualStyleBackColor = false;

            if(m_Command.Checked)
            {
                Button.BackColor = sr_SelectedBack;
                Button.Font = new Font(Button.Font, FontStyle.Bold);
            }
            else
            {
                Button.BackColor = sr_NormalBack;
                Button.Font = new Font(Button.Font, FontStyle.Regular);
            }
        }
    }
}