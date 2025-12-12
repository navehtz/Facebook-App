using FacebookWinFormsApp.logic;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace FacebookMini.Logic
{
    public class FacebookAppLogic : IFacebookAppLogic
    {
        private readonly User r_LoggedInUser;
       

        public FacebookAppLogic(User i_LoggedInUser)
        {
            r_LoggedInUser = i_LoggedInUser;
        }

        public User LoggedInUser => r_LoggedInUser;

        public IEnumerable<Post> GetUserPosts()
        {
            return r_LoggedInUser.Posts;
        }

        public IEnumerable<Album> GetUserAlbums()
        {
            return r_LoggedInUser.Albums;
        }

        public IEnumerable<Page> GetUserLikedPages()
        {
            return r_LoggedInUser.LikedPages;
        }

        public IEnumerable<User> GetUserFriendsOrDummy()
        {
            return r_LoggedInUser.Friends;
        }
    }
}