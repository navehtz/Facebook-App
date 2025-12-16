using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.logic.features.postNotes
{
    public class InMemoryPostNotesManager : IPostNotesManager
    {
        private readonly Dictionary<string, string> r_PostIdToNote = new Dictionary<string, string>();

        public string GetNoteForPost(string i_PostId)
        {
            return i_PostId != null && r_PostIdToNote.TryGetValue(i_PostId, out string o_Note)
                   ? o_Note
                   : null;
        }

        public void SetNoteForPost(string i_PostId, string i_NoteText)
        {
            if(!string.IsNullOrEmpty(i_PostId))
            {
                r_PostIdToNote[i_PostId] = i_NoteText;
            }
        }

        public void RemoveNoteForPost(string i_PostId)
        {
            if (!string.IsNullOrEmpty(i_PostId))
            {
                r_PostIdToNote.Remove(i_PostId);
            }
        }
    }
}
