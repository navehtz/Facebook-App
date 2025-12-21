using FacebookMini.logic;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;

namespace FacebookMini.Logic
{
    public interface IFacebookAppLogic
    {
        User LoggedInUser { get; }
        IPostNotesManager PostNotesManager {get; }
        IPostTagsManager PostTagsManager { get; }
        IEnumerable<Post> GetUserPosts();
        IEnumerable<Album> GetUserAlbums();
        IEnumerable<Page> GetUserLikedPages();
    }
}