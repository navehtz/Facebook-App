using FacebookWinFormsApp.Logic;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using FacebookWinFormsApp.logic.dummyData;

namespace FacebookMini.Logic
{
    public class FacebookAppLogic : IFacebookAppLogic
    {
        private readonly User r_LoggedInUser;
        private readonly FavoritesManager r_FavoritesManager = new FavoritesManager();
        private readonly AnalyticsManager r_analyticsManager;

        public FacebookAppLogic(User i_LoggedInUser)
        {
            r_LoggedInUser = i_LoggedInUser;
            r_analyticsManager = new AnalyticsManager(r_LoggedInUser.Posts.ToList(), r_LoggedInUser.PhotosTaggedIn.ToList());

            //generate fake data about posts and photos.
            //List<Photo> albumPhotos = new List<Photo>();

            //foreach (Album album in r_LoggedInUser.Albums)
            //{
            //    foreach (Photo photo in album.Photos)
            //    {
            //        albumPhotos.Add(photo);
            //    }
            //}
            //r_analyticsManager = new AnalyticsManager(r_LoggedInUser.Posts.ToList(), albumPhotos.ToList());
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
            //return r_LoggedInUser.Friends != null ? r_LoggedInUser.Friends : DummyDataLoader.LoadDummyFriends();
        }
    }
}