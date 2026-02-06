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
        private readonly Color r_NormalBackColor;
        private readonly Color r_PressedBackColor;

        public CommandObserverForButton(Button i_Button)
            : base(i_Button)
        {
            r_NormalBackColor = i_Button.BackColor;
            r_PressedBackColor = Color.LightGray;
        }

        protected override void UpdateSpecific()
        {
            if(m_Command.Checked)
            {
                m_Button.BackColor = r_PressedBackColor;
                m_Button.Font = new Font(m_Button.Font, FontStyle.Bold);
            }
            else
            {
                m_Button.BackColor = r_NormalBackColor;
                m_Button.Font = new Font(m_Button.Font, FontStyle.Regular);
            }
        }
    }
}