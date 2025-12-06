using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace FacebookMini.Logic
{
    public class FacebookAppLogic : IFacebookAppLogic
    {
        private readonly User r_LoggedInUser;
        private readonly FavoritesManager favoritesManager = new FavoritesManager();

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
    }
}