using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookWinFormsApp.logic.postTags
{
    internal class PostTagsManager : IPostTagsManager
    {
        private readonly Dictionary<string, List<string>>r_PostIdToTags =
            new Dictionary<string, List<string>>();

        public IList<string> GetPostTags(string i_PostId)
        {
            List<string> outTagsList = new List<string>();

            if(!string.IsNullOrEmpty(i_PostId))
            {
                List<string> savedTags;
                
                if(r_PostIdToTags.TryGetValue(i_PostId, out savedTags))
                {
                    outTagsList = new List<string>(savedTags);
                }
            }

            return outTagsList;
        }

        public void SetPostTags(string i_PostId, IList<string> i_Tags)
        {
            if (!string.IsNullOrEmpty(i_PostId) && i_Tags != null)
            {
                List<string> tagsList = new List<string>(i_Tags);

                r_PostIdToTags[i_PostId] = tagsList;
            }
        }

        public IList<string> GetAllTags()
        {
            List<string> allTagList = new List<string>();

            foreach (List<string> tagsList in r_PostIdToTags.Values)
            {
                foreach (string tag in tagsList)
                {
                    allTagList.Add(tag);
                }
            }

            return allTagList;
        }
    }
}
