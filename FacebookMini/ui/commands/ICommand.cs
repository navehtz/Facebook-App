using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.ui.commands
{
    public interface ICommand : INotifyPropertyChanged
    {
        void Execute(object i_Param);

        string Name { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        bool Enabled { get; set; }
        bool Available { get; set; }
        bool Checked { get; set; }
    }
}