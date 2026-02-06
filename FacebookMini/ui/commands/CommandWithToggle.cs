using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.ui.commands
{
    internal class CommandWithToggle : CommandBase
    {
        public CommandWithToggle(ICommand i_ChainedCommand)
            : base(i_ChainedCommand)
        { }

        protected override void InternalExecute(object param)
        {
            this.Checked = !this.Checked;
        }
    }
}