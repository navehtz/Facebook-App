using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FacebookWinFormsApp.CustomComponent
{
    public partial class ItemGalleryComponent : UserControl
    {
        private readonly FlowLayoutPanel m_Flow;
        public event EventHandler AddToFavoritesClicked;

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

        private Control createTile(GalleryItem i_Item)
        {
            var panel = new Panel
                            {
                                Width = 160,
                                Height = 120,
                                Margin = new Padding(3)
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
                                Text = i_Item.Title,
                                Dock = DockStyle.Fill,
                                TextAlign = ContentAlignment.TopCenter
                            };

            panel.Controls.Add(label);
            panel.Controls.Add(pic);
            panel.Tag = i_Item.Tag;

            return panel;
        }
    }
}
