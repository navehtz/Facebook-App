using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.logic.features.postTags
{
    internal class InMemoryPostTagsManager : IPostTagsManager
    {
        private readonly Dictionary<string, ICollection<string>>r_PostIdToTags =
            new Dictionary<string, ICollection<string>>();

        public ICollection<string> GetPostTags(string i_PostId)
        {
            ICollection<string> outTagsList = new HashSet<string>();

            if(!string.IsNullOrEmpty(i_PostId))
            {
                ICollection<string> savedTags;
                
                if(r_PostIdToTags.TryGetValue(i_PostId, out savedTags))
                {
                    outTagsList = new HashSet<string>(savedTags);
                }
            }

            return outTagsList;
        }

        public void SetPostTags(string i_PostId, ICollection<string> i_Tags)
        {
            if (!string.IsNullOrEmpty(i_PostId) && i_Tags != null)
            {
                HashSet<string> tagsSet = new HashSet<string>();

                foreach(string tagName in i_Tags) 
                {
                    if (!string.IsNullOrWhiteSpace(tagName))
                    {
                        tagsSet.Add(char.ToUpper(tagName[0]) + tagName.Substring(1).ToLower());
                    }
                }

                r_PostIdToTags[i_PostId] = tagsSet;
            }
        }

        public ICollection<string> GetAllTags()
        {
            ICollection<string> allTagsList = new List<string>();

            foreach (ICollection<string> tagSet in r_PostIdToTags.Values)
            {
                foreach (string tagName in tagSet)
                {
                    if(!string.IsNullOrEmpty(tagName))
                    {
                        allTagsList.Add(char.ToUpper(tagName[0]) + tagName.Substring(1).ToLower());
                    }
                }
            }

            return allTagsList;
        }
    }
}
