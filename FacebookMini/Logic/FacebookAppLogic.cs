using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using FacebookMini.shared.adapters;
using FacebookMini.shared.galleryItem;


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

        public IEnumerable<IPostData> GetMyPostsData()
        {
            List<IPostData> postsData = new List<IPostData>();

            if (LoggedInUser?.Posts == null)
            {
                return postsData;
            }

            foreach (Post post in LoggedInUser.Posts)
            {
                if (post != null)
                {
                    IPostData postData = new FacebookPostAdapter(post, LoggedInUser);
                    postsData.Add(postData);
                }
            }

            return postsData;
        }

        public List<GalleryItem> GetAlbumsGalleryItems()
        {
            List<GalleryItem> albumsAsGalleryItems = new List<GalleryItem>();

            if (LoggedInUser?.Albums == null)
            {
                return albumsAsGalleryItems;
            }

            foreach (Album album in LoggedInUser.Albums)
            {
                if (album == null)
                {
                    continue;
                }

                GalleryItem item = new GalleryItem
                {
                   Title = album.Name,
                   Image = album.ImageAlbum,
                   ItemType = eGalleryItemType.Album,
                   Id = album.Id
                };

                albumsAsGalleryItems.Add(item);
            }

            return albumsAsGalleryItems;
        }

        public List<GalleryItem> GetLikedPagesGalleryItems()
        {
            List<GalleryItem> pagesAsGalleryItems = new List<GalleryItem>();

            if (LoggedInUser?.LikedPages == null)
            {
                return pagesAsGalleryItems;
            }

            foreach (Page page in LoggedInUser.LikedPages)
            {
                if (page == null)
                {
                    continue;
                }

                GalleryItem item = new GalleryItem
                {
                    Title = page.Name,
                    Image = page.ImageNormal,
                    ItemType = eGalleryItemType.Page,
                    Id = page.Id
                };

                pagesAsGalleryItems.Add(item);
            }

            return pagesAsGalleryItems;
        }
    }
}