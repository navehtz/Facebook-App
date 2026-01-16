using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;
using FacebookMini.Logic;
//using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
