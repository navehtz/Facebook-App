using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FacebookMini.ui.CustomComponent;

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
