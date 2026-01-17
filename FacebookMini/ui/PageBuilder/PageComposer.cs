using FacebookMini.ui.PageBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMini.ui.PageBuilder
{
    public class PageComposer
    {
        private readonly PageBuildContext r_Context;

        public PageComposer(PageBuildContext i_Context)
        {
            r_Context = i_Context;
        }

        public Control Compose(IPageBuilder i_Builder)
        {
            i_Builder.Reset();
            i_Builder.BuildHeader();
            i_Builder.BuildBody();
            return i_Builder.GetResult();
        }
    }
}
