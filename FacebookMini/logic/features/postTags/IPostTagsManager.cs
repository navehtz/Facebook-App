using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.logic.features.postTags
{
    public interface IPostTagsManager
    {
        ICollection<string> GetPostTags(string i_PostId);
        void SetPostTags(string i_PostId, ICollection<string> i_Tags);
        ICollection<string> GetAllTags();
    }
}