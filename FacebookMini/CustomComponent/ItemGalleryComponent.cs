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
    internal partial class ItemGalleryComponent : UserControl
    {
        private const int k_TileWidth = 110;
        private const int k_TileHeight = 130;
        private const int k_TileMargin = 8;

        private int m_TotalContentHeight;
        private bool m_IsFavoriteSelectionMode = false;

        public event EventHandler<GalleryItem> ItemClicked;
        public event EventHandler<GalleryItem> AddToFavoritesRequested;

        public ItemGalleryComponent()
        {
            InitializeComponent();

            vScrollBar.Scroll += vScrollBar_Scroll;
            this.Resize += ItemGalleryComponent_Resize;
        }

        private void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            m_IsFavoriteSelectionMode = true;
            btnAddToFavorites.Text = "Select item…";
            Cursor = Cursors.Hand;
        }

        public void SetItems(IEnumerable<GalleryItem> items)
        {
            panelItems.SuspendLayout();
            panelItems.Controls.Clear();

            int index = 0;
            int columns = Math.Max(1, panelItems.Width / (k_TileWidth + k_TileMargin));

            foreach (GalleryItem item in items)
            {
                Panel tilePanel = createTilePanel(item);

                int col = index % columns;
                int row = index / columns;

                tilePanel.Location = new Point(
                    col * (k_TileWidth + k_TileMargin),
                    row * (k_TileHeight + k_TileMargin));

                panelItems.Controls.Add(tilePanel);
                index++;
            }

            int rows = (int)Math.Ceiling(index / (double)columns);
            m_TotalContentHeight = rows * (k_TileHeight + k_TileMargin);

            updateScrollBar();
            panelItems.ResumeLayout();
        }

        private Panel createTilePanel(GalleryItem item)
        {
            Panel panel = new Panel();
            panel.Size = new Size(k_TileWidth, k_TileHeight);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Top;
            pictureBox.Height = 90;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = item.Image;

            Label label = new Label();
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = item.Title;

            // make whole tile clickable
            panel.Tag = item;
            pictureBox.Tag = item;
            label.Tag = item;

            panel.Click += onTileClick;
            pictureBox.Click += onTileClick;
            label.Click += onTileClick;

            panel.Controls.Add(label);
            panel.Controls.Add(pictureBox);

            return panel;
        }

        private void onTileClick(object sender, EventArgs e)
        {
            if (sender is Control control && control.Tag is GalleryItem item)
            {
                if (m_IsFavoriteSelectionMode)
                {
                    // leave selection mode
                    m_IsFavoriteSelectionMode = false;
                    btnAddToFavorites.Text = "Add to Favorites";
                    Cursor = Cursors.Default;

                    // tell outside world “this item should be added to favorites”
                    AddToFavoritesRequested?.Invoke(this, item);
                }
                else
                {
                    // normal click behavior
                    ItemClicked?.Invoke(this, item);
                }
            }
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            panelItems.Top = -vScrollBar.Value;
        }

        private void ItemGalleryComponent_Resize(object sender, EventArgs e)
        {
            // when the control is resized we may have more/less columns
            // so in a real project you’d want to re-layout items here
            updateScrollBar();
        }

        private void updateScrollBar()
        {
            int visibleHeight = panelItems.Height;

            if (m_TotalContentHeight <= visibleHeight)
            {
                vScrollBar.Enabled = false;
                vScrollBar.Value = 0;
                panelItems.Top = 0;
            }
            else
            {
                vScrollBar.Enabled = true;
                vScrollBar.Minimum = 0;
                vScrollBar.Maximum = m_TotalContentHeight - visibleHeight;
                //vScrollBar.LargeChange = visibleHeight;
                if (vScrollBar.Value > vScrollBar.Maximum)
                {
                    vScrollBar.Value = vScrollBar.Maximum;
                }
                panelItems.Top = -vScrollBar.Value;
            }
        }
    }
}
