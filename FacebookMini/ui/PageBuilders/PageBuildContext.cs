using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;
using FacebookMini.Logic;
using FacebookWrapper.ObjectModel;
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
        public User LoggedInUser { get; }
        public IPostNotesManager NotesManager { get; }
        public IPostTagsManager TagsManager { get; }
        public PictureBox UserPictureBoxTopBar { get; }

        public PageBuildContext(IFacebookAppLogic i_IppLogic, User i_LoggedInUser, IPostNotesManager i_NotesManager, IPostTagsManager i_TagsManager, PictureBox i_UserPictureBoxTopBar)
        {
            AppLogic = i_IppLogic;
            LoggedInUser = i_LoggedInUser;
            NotesManager = i_NotesManager;
            TagsManager = i_TagsManager;
            UserPictureBoxTopBar = i_UserPictureBoxTopBar;
        }
    }

}
