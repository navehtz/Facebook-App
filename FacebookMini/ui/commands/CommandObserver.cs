using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMini.ui.commands
{
    public abstract class CommandObserver
    {
        protected ICommand m_Command;
        protected Button m_Button;

        public ICommand Command
        {
            get { return m_Command; }
            set
            {
                if (m_Command != value)
                {
                    unHookFromCommand();
                    m_Command = value;
                    updateAccordingToCommandState();
                    hookToCommand();
                }
            }
        }

        public CommandObserver(Button i_Button)
        {
            m_Button = i_Button;
        }

        private void hookToCommand()
        {
            m_Command.PropertyChanged += new PropertyChangedEventHandler(m_Command_PropertyChanged);
        }

        private void unHookFromCommand()
        {
            if (m_Command != null)
            {
                m_Command.PropertyChanged -= new PropertyChangedEventHandler(m_Command_PropertyChanged);
            }
        }

        private void m_Command_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            updateAccordingToCommandState();
        }

        private void updateAccordingToCommandState()
        {
            m_Button.Enabled = m_Command.Enabled;
            m_Button.Visible = m_Command.Available;

            if (!string.IsNullOrEmpty(m_Command.Title))
            {
                m_Button.Text = m_Command.Title;
            }

            UpdateSpecific();
        }

        protected abstract void UpdateSpecific();

        public static CommandObserver  CreateCommandHolder(Button i_Button)
        {
            return new CommandObserverForButton(i_Button);
        }
    }
}