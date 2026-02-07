using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.ui.commands
{
    public abstract class CommandBase : ICommand
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private ICommand m_ChainedCommand;

        protected CommandBase(ICommand i_ChainedCommand)
        {
            m_ChainedCommand = i_ChainedCommand;
        }

        protected CommandBase()
        { }

        protected void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, e);
            }
        }

        public string Name { get; set; }

        private bool m_Enabled = true;
        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                if (m_Enabled != value)
                {
                    m_Enabled = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("Enabled"));
                }
            }
        }

        private bool m_Available = true;
        public bool Available
        {
            get { return m_Available; }
            set
            {
                if (m_Available != value)
                {
                    m_Available = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("Available"));
                }
            }
        }

        private bool m_Checked;
        public bool Checked
        {
            get { return m_Checked; }
            set
            {
                if (m_Checked != value)
                {
                    m_Checked = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("Checked"));
                }
            }
        }

        private string m_Title;
        public string Title
        {
            get { return m_Title; }
            set
            {
                if (m_Title != value)
                {
                    m_Title = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("Title"));
                }
            }
        }

        private string m_ToolTip;
        public string Description
        {
            get { return m_ToolTip; }
            set
            {
                if (m_ToolTip != value)
                {
                    m_ToolTip = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("ToolTip"));
                }
            }
        }

        public void Execute(object param)
        {
            InternalExecute(param);

            if (m_ChainedCommand != null)
            {
                m_ChainedCommand.Execute(param);
            }
        }

        protected abstract void InternalExecute(object param);
    }
}