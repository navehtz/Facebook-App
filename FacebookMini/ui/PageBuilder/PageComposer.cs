using System.Windows.Forms;

namespace FacebookMini.ui.PageBuilder
{
    public class PageComposer
    {
        private readonly PageBuildContext r_Context;
        public IPageBuilder Builder { get; set; }


        public PageComposer(PageBuildContext i_Context, IPageBuilder i_Builder)
        {
            r_Context = i_Context;
            Builder = i_Builder;
        }

        public Control Compose()
        {
            Builder.Reset();
            Builder.DeliverContext(r_Context);
            Builder.BuildHeader();
            Builder.BuildBody();

            return Builder.GetResult();
        }
    }
}
