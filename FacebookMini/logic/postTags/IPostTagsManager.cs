using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookWinFormsApp.logic.postTags
{
    internal interface IPostTagsManager
    {
        List<string> GetPostTags(string i_PostId);
        void SetPostTags(string i_PostId, List<string> i_Tags);
        List<string> GetAllTags();
    }
}
