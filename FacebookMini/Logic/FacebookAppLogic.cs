using FacebookMini.logic;
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
        }

        public User LoggedInUser { get; }

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