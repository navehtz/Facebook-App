using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.ui.commands
{
    internal class CommandWithDelegate : CommandBase
    {
        public Action Action { get; set; }

        public CommandWithDelegate(Action i_Action)
        {
            Action = i_Action ?? throw new ArgumentNullException(nameof(i_Action));
        }

        public CommandWithDelegate(ICommand i_ChainedCommand)
            : base(i_ChainedCommand)
        { }

        protected override void InternalExecute(object param)
        {
            Action.Invoke();
        }
    }
}