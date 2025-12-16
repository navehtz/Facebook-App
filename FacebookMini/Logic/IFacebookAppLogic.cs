using FacebookMini.logic;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace FacebookMini.Logic
{
    public interface IFacebookAppLogic
    {
        User LoggedInUser { get; }
        IEnumerable<Post> GetUserPosts();
        IEnumerable<Album> GetUserAlbums();
        IEnumerable<Page> GetUserLikedPages();
    }
}