using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMini.ui
{
    public interface IPageBuilder
    {
        void Reset();
        void BuildHeader();
        void BuildBody();
        Control GetResult();
    }
}
