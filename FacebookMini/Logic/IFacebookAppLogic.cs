using FacebookMini.logic;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using FacebookMini.shared.adapters;
using FacebookMini.logic.features.postNotes;
using FacebookMini.logic.features.postTags;
using FacebookMini.shared.galleryItem;
using FacebookMini.ui.CustomComponent;

namespace FacebookMini.Logic
{
    public interface IFacebookAppLogic
    {
        User LoggedInUser { get; }
        IPostNotesManager PostNotesManager {get; }
        IPostTagsManager PostTagsManager { get; }
        IEnumerable<Post> GetUserPosts();
        IEnumerable<Album> GetUserAlbums();
        IEnumerable<Page> GetUserLikedPages();
        IEnumerable<IPostData> GetFriendsFeedPostsData();
        IEnumerable<IPostData> GetMyPostsData();
        List<GalleryItem> GetAlbumsGalleryItems();
        List<GalleryItem> GetLikedPagesGalleryItems();
    }
}