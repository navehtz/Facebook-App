using System.Collections.Generic;

namespace FacebookMini.logic.features.postTags
{
    public interface IPostTagsManager
    {
        ICollection<string> GetPostTags(string i_PostId);
        void SetPostTags(string i_PostId, ICollection<string> i_Tags);
        ICollection<string> GetAllTags();
    }
}