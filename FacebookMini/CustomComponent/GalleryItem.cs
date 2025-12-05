using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FacebookWinFormsApp.CustomComponent
{
    public enum eGalleryItemType
    {
        Album,
        Page,
        Photo,
        Other
    }

    internal class GalleryItem
    {
        public Image Image { get; set; }
        public string Title { get; set; }
        public eGalleryItemType ItemType { get; set; }
        public object Tag { get; set; }
    }
}
