using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMini.ui.CustomComponent
{
    static class GalleryTileFactory 
    {
        public static Control CreateTile(GalleryItem i_Item)
        {
            if (i_Item == null)
            {
                return new Panel();
            }

            switch (i_Item.ItemType)
            {
                case eGalleryItemType.Album:
                    return createDefaultTile(i_Item);

                case eGalleryItemType.Page:
                    return createDefaultTile(i_Item);

                case eGalleryItemType.Photo:
                    return createDefaultTile(i_Item);

                default:
                    return createDefaultTile(i_Item);
            }
        }
       
        private static Control createDefaultTile(GalleryItem i_Item)
        {
            var panel = new Panel
            {
                Width = 160,
                Height = 120,
                Margin = new Padding(3),
                Tag = i_Item.Tag
            };

            var pic = new PictureBox
            {
                Image = i_Item.Image,
                Dock = DockStyle.Top,
                Height = 80,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            var label = new Label
            {
                Text = i_Item.Title ?? string.Empty,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.TopCenter
            };

            panel.Controls.Add(label);
            panel.Controls.Add(pic);

            return panel;
        }
    }
}
