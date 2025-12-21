using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.logic.features.postNotes
{
    public interface IPostNotesManager
    {
        string GetNoteForPost(string i_PostId);
        void SetNoteForPost(string i_PostId, string i_NoteText);
        void RemoveNoteForPost(string i_PostId);
    }
}
