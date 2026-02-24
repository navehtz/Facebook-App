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
        protected Control m_Control;

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

        public CommandObserver(Control i_Control)
        {
            m_Control = i_Control;
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
            m_Control.Enabled = m_Command.Enabled;
            m_Control.Visible = m_Command.Available;

            if (!string.IsNullOrEmpty(m_Command.Title))
            {
                m_Control.Text = m_Command.Title;
            }

            UpdateSpecific();
        }

        protected abstract void UpdateSpecific();

        /// Static Factory Method:
        public static CommandObserver  CreateCommandHolder(Control i_Control)
        {
            if (i_Control == null)
            {
                throw new ArgumentNullException(nameof(i_Control));
            }

            CommandObserver retVal = null;

            if (i_Control is Button button)
            {
                retVal = new CommandObserverForButton(button);
            }

            return retVal;
        }
    }
}