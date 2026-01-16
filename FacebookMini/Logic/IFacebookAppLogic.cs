using FacebookMini.logic;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using FacebookMini.shared.adapters;
using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;
using FacebookMini.shared.galleryItem;
using FacebookMini.shared.profileData;
using FacebookMini.ui.CustomComponent;

namespace FacebookMini.Logic
{
    public interface IFacebookAppLogic
    {
        User LoggedInUser { get; }
        IPostNotesManager PostNotesManager { get; }
        IPostTagsManager PostTagsManager { get; }
        //IEnumerable<Post> GetUserPosts();
        //IEnumerable<Album> GetUserAlbums();
        //IEnumerable<Page> GetUserLikedPages();
        IEnumerable<IPostData> GetFriendsFeedPostsData();
        IEnumerable<IPostData> GetMyPostsData();
        UserProfileData GetLoggedInUserProfileData();
        List<GalleryItem> GetAlbumsGalleryItems();
        List<GalleryItem> GetLikedPagesGalleryItems();

        string GetNoteForPost(string i_PostId);
        void SetNoteForPost(string i_PostId, string i_NoteText);
        void RemoveNoteForPost(string i_PostId);

        ICollection<string> GetTagsForPost(string i_PostId);
        void SetTagsForPost(string i_PostId, ICollection<string> i_Tags);
        ICollection<string> GetAllTags();
        //TODO: update
    }
}