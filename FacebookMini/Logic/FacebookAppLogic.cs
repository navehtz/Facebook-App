using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using FacebookMini.shared.adapters;
using FacebookMini.shared.galleryItem;
using FacebookMini.shared.profileData;


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

        //public IEnumerable<Post> GetUserPosts()
        //{
        //    return LoggedInUser.Posts;
        //}

        //public IEnumerable<Album> GetUserAlbums()
        //{
        //    return LoggedInUser.Albums;
        //}

        //public IEnumerable<Page> GetUserLikedPages()
        //{
        //    return LoggedInUser.LikedPages;
        //}

        public IEnumerable<IPostData> GetFriendsFeedPostsData()
        {

            List<IPostData> feedPosts = new List<IPostData>();

            if(LoggedInUser?.Friends != null)
            {
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
            }

            return feedPosts;
        }

        public IEnumerable<IPostData> GetMyPostsData()
        {
            List<IPostData> postsData = new List<IPostData>();

            if(LoggedInUser?.Posts != null)
            {
                foreach(Post post in LoggedInUser.Posts)
                {
                    if(post != null)
                    {
                        IPostData postData = new FacebookPostAdapter(post, LoggedInUser);

                        postsData.Add(postData);
                    }
                }
            }

            return postsData;
        }

        public List<GalleryItem> GetAlbumsGalleryItems()
        {
            List<GalleryItem> albumsAsGalleryItems = new List<GalleryItem>();

            if(LoggedInUser?.Albums != null)
            {
                foreach(Album album in LoggedInUser.Albums)
                {
                    if(album == null)
                    {
                        continue;
                    }

                    GalleryItem item = new GalleryItem
                    {
                       Title = album.Name ?? string.Empty,
                       Image = album.ImageAlbum,
                       ItemType = eGalleryItemType.Album,
                       Id = album.Id ?? string.Empty
                    };

                    albumsAsGalleryItems.Add(item);
                }
            }

            return albumsAsGalleryItems;
        }

        public List<GalleryItem> GetLikedPagesGalleryItems()
        {
            List<GalleryItem> pagesAsGalleryItems = new List<GalleryItem>();

            if(LoggedInUser?.LikedPages != null)
            {
                foreach(Page page in LoggedInUser.LikedPages)
                {
                    if(page == null)
                    {
                        continue;
                    }

                    GalleryItem item = new GalleryItem
                    {
                       Title = page.Name ?? string.Empty,
                       Image = page.ImageNormal,
                       ItemType = eGalleryItemType.Page,
                       Id = page.Id ?? string.Empty
                    };

                    pagesAsGalleryItems.Add(item);
                }
            }

            return pagesAsGalleryItems;
        }

        public UserProfileData GetLoggedInUserProfileData()
        {
            UserProfileData userProfileData = new UserProfileData();

            if(LoggedInUser != null)
            {
                userProfileData.Name = LoggedInUser.Name ?? string.Empty;
                userProfileData.ProfilePictureUrl = LoggedInUser.PictureNormalURL ?? string.Empty;
                userProfileData.Email = LoggedInUser.Email ?? string.Empty;
                userProfileData.Birthday = LoggedInUser.Birthday;

                if(LoggedInUser.Location != null)
                {
                    userProfileData.LocationName = LoggedInUser.Location.Name ?? string.Empty;
                }
                else
                {
                    userProfileData.LocationName = string.Empty;
                }
            }

            return userProfileData;
        }

        public string GetNoteForPost(string i_PostId)
        {
            string resultNote = string.Empty;

            if (!string.IsNullOrEmpty(i_PostId))
            {
                string noteFromManager = PostNotesManager.GetNoteForPost(i_PostId);
                if (noteFromManager != null)
                {
                    resultNote = noteFromManager;
                }
            }

            return resultNote;
        }

        public void SetNoteForPost(string i_PostId, string i_NoteText)
        {
            if (string.IsNullOrEmpty(i_PostId))
            {
                return;
            }

            if (string.IsNullOrEmpty(i_NoteText))
            {
                PostNotesManager.RemoveNoteForPost(i_PostId);
            }
            else
            {
                PostNotesManager.SetNoteForPost(i_PostId, i_NoteText);
            }
        }

        public void RemoveNoteForPost(string i_PostId)
        {
            if (!string.IsNullOrEmpty(i_PostId))
            {
                PostNotesManager.RemoveNoteForPost(i_PostId);
            }
        }

        public ICollection<string> GetTagsForPost(string i_PostId)
        {
            ICollection<string> resultTags = new List<string>();

            if (!string.IsNullOrEmpty(i_PostId))
            {
                ICollection<string> tagsFromManager = PostTagsManager.GetPostTags(i_PostId);

                if (tagsFromManager != null)
                {
                    resultTags = tagsFromManager;
                }
            }

            return resultTags;
        }

        public void SetTagsForPost(string i_PostId, ICollection<string> i_Tags)
        {
            if (string.IsNullOrEmpty(i_PostId))
            {
                return;
            }

            if (i_Tags == null)
            {
                i_Tags = new List<string>();
            }

            PostTagsManager.SetPostTags(i_PostId, i_Tags);
        }
    }
}