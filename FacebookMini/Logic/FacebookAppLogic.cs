using FacebookMini.Adapters;
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

        public IEnumerable<IPostData> GetFriendsFeedPostsData()
        {

            List<IPostData> feedPosts = new List<IPostData>();

            if(LoggedInUser?.Friends == null)
            {
                return feedPosts;
            }

            HashSet<string> addedPostIds = new HashSet<string>();

            foreach(User friend in LoggedInUser.Friends)
            {
                if(friend?.Posts == null)
                {
                    continue;
                }

                foreach(Post post in friend.Posts)
                {
                    if(post == null)
                    {
                        continue;
                    }

                    string postId = post.Id;

                    if(!string.IsNullOrEmpty(postId) && addedPostIds.Contains(postId))
                    {
                        continue;
                    }

                    IPostData postData = new FacebookPostAdapter(post, friend);
                    feedPosts.Add(postData);

                    if(!string.IsNullOrEmpty(postId))
                    {
                        addedPostIds.Add(postId);
                    }
                }
            }

            return feedPosts;
        }
    }
}