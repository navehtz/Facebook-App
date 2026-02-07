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

        public CommandObserverForButton(Button i_Button)
            : base(i_Button)
        { }

        protected override void UpdateSpecific()
        {
            m_Button.UseVisualStyleBackColor = false;

            if(m_Command.Checked)
            {
                m_Button.BackColor = sr_SelectedBack;
                m_Button.Font = new Font(m_Button.Font, FontStyle.Bold);
            }
            else
            {
                m_Button.BackColor = sr_NormalBack;
                m_Button.Font = new Font(m_Button.Font, FontStyle.Regular);
            }
        }
    }
}