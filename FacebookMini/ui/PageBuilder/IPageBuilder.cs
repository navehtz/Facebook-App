using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMini.ui.PageBuilder
{
    public interface IPageBuilder
    {
        void Reset();
        void DeliverContext(PageBuildContext i_Context);
        void BuildHeader();
        void BuildBody();
        Control GetResult();
    }
}
