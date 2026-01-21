using System.Drawing;

namespace FacebookMini.shared.galleryItem
{
    public class GalleryItem
    {
        public Image Image { get; set; }
        public string Title { get; set; }
        public eGalleryItemType ItemType { get; set; }
        public string Id { get; set; }
    }
}
