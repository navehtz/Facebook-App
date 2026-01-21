using FacebookMini.Logic;
using System.Windows.Forms;

namespace FacebookMini.ui.PageBuilder
{
    public class PageBuildContext
    {
        public IFacebookAppLogic AppLogic { get; }
        public PictureBox UserPictureBoxTopBar { get; }

        public PageBuildContext(
            IFacebookAppLogic i_IAppLogic,
            PictureBox i_UserPictureBoxTopBar)
        {
            AppLogic = i_IAppLogic;
            UserPictureBoxTopBar = i_UserPictureBoxTopBar;
        }
    }
}
