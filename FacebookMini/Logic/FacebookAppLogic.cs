using FacebookMini.logic;
using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace FacebookMini.Logic
{
    public class FacebookAppLogic : IFacebookAppLogic
    {
        public FacebookAppLogic(User i_LoggedInUser)
        {
            LoggedInUser = i_LoggedInUser;
            PostNotesManager = new InMemoryPostNotesManager();
            PostTagsManager = new InMemoryPostTagsManager();
        }

        public User LoggedInUser { get; }
        public IPostNotesManager PostNotesManager { get; }
        public IPostTagsManager PostTagsManager { get; }
        
        public IEnumerable<Post> GetUserPosts()
        {
            return LoggedInUser.Posts;
        }

        public IEnumerable<Album> GetUserAlbums()
        {
            return LoggedInUser.Albums;
        }

        public IEnumerable<Page> GetUserLikedPages()
        {
            return LoggedInUser.LikedPages;
        }
    }
}