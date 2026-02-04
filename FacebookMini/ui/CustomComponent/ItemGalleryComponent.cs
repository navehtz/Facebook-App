using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FacebookMini.shared.galleryItem;

namespace FacebookMini.ui.CustomComponent
{
    public partial class ItemGalleryComponent : UserControl
    {
        private readonly FlowLayoutPanel m_Flow;

        public ItemGalleryComponent()
        {
            InitializeComponent();
            m_Flow = new FlowLayoutPanel
                         {
                             Dock = DockStyle.Fill,
                             AutoScroll = true,
                             FlowDirection = FlowDirection.TopDown,   // one column
                             WrapContents = false,
                             Padding = new Padding(5)
                         };

            this.panelItems.Controls.Add(m_Flow);
        }

        internal void SetItems(IEnumerable<GalleryItem> i_Items)
        {
            m_Flow.Controls.Clear();

            if (i_Items == null)
            {
                return;
            }

            foreach (GalleryItem item in i_Items)
            {
                m_Flow.Controls.Add(createTile(item));
            }
        }

        public void SetItem(GalleryItem i_Item) 
        {
            if (i_Item == null)
            {
                return;
            }

            m_Flow.Controls.Add(createTile(i_Item));
        }

        private Control createTile(GalleryItem i_Item)
        {
            Panel panel = new Panel
                              {
                                  Width = 160,
                                  Height = 120,
                                  Margin = new Padding(3)
                              };

            PictureBox pic = new PictureBox
                                 {
                                     Image = i_Item.Image,
                                     Dock = DockStyle.Top,
                                     Height = 80,
                                     SizeMode = PictureBoxSizeMode.Zoom
                                 };

            Label label = new Label
                              {
                                  Text = i_Item.Title,
                                  Dock = DockStyle.Fill,
                                  TextAlign = ContentAlignment.TopCenter
                              };

            panel.Controls.Add(label);
            panel.Controls.Add(pic);
            panel.Tag = i_Item.Id;

            return panel;
        }
    }
}