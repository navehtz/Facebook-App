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
