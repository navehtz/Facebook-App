using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMini.MyComponents
{
    public partial class SelectionWithImagePreview : UserControl
    {
        // --- readonly UI fields (לפי התקן: r_) ---
        private readonly SplitContainer r_SplitContainer;   // Panel1: Title+List, Panel2: Image
        private readonly TableLayoutPanel r_TopLayout;      // Row 0: Title, Row 1: List
        private readonly Label r_TitleLabel;                // Title above the list
        private readonly ListBox r_ListBox;                 // Scrollable list
        private readonly PictureBox r_PictureBox;           // Square image below (Panel2)

        // --- state fields (לפי התקן: m_) ---
        private object m_DataSource;                        // IEnumerable
        private string m_TitleText = "Items";
        private Func<object, string> m_DisplaySelector;     // Selects display text for each item
        private Func<object, string> m_ImageUrlSelector;    // Selects image URL for each item
        private Label TitleLabel;
        private VScrollBar vScrollBar1;
        private Image m_CurrentImage;

        // חובה ל-Designer
        public SelectionWithImagePreview()
        {
            // Split: Top (title+list) / Bottom (image)
            r_SplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal, // תמונה מתחת לרשימה
                SplitterDistance = 250                 // גובה האזור העליון
            };

            // Top layout: 2 rows (Title, List)
            r_TopLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            r_TopLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // Title
            r_TopLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // List

            r_TitleLabel = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 6),
                Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold),
                Text = m_TitleText
            };

            // במקום ההגדרה הנוכחית של r_ListBox:
            r_ListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                IntegralHeight = false,
                FormattingEnabled = true   // ← חובה כדי ש-Format יעבוד
            };

            // תמונה ריבועית: לא Dock — נרכז ונתאים לגודל בפאנל התחתון
            r_PictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = SystemColors.Window,
                Anchor = AnchorStyles.None           // נשלוט במיקום ובגודל ידנית
            };

            // לבנות היררכיית UI
            r_TopLayout.Controls.Add(r_TitleLabel, 0, 0);
            r_TopLayout.Controls.Add(r_ListBox, 0, 1);
            r_SplitContainer.Panel1.Controls.Add(r_TopLayout);
            r_SplitContainer.Panel2.Controls.Add(r_PictureBox);
            Controls.Add(r_SplitContainer);

            // אירועים
            r_ListBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
            r_ListBox.Format += listBox_Format;                    // כדי להשתמש ב-DisplaySelector
            r_SplitContainer.Panel2.Resize += panel2_Resize;       // לשמור על תמונה ריבועית ממורכזת
            Disposed += selectionWithImagePreview_Disposed;
        }

        // קונסטרקטור “גנרי” נוח לשימוש ישיר (לא חובה ל-Designer)
        public SelectionWithImagePreview(
            IEnumerable i_Items,
            string i_Title,
            Func<object, string> i_DisplaySelector,
            Func<object, string> i_ImageUrlSelector) : this()
        {
            SetData(i_Items, i_Title, i_DisplaySelector, i_ImageUrlSelector);
        }

        // ---------- Properties (Design-friendly) ----------

        [Browsable(false)]
        public object DataSource
        {
            get { return m_DataSource; }
            set
            {
                m_DataSource = value;
                r_ListBox.DataSource = (IEnumerable)value;
            }
        }

        [Category("Data")]
        [Description("Delegate that selects the display text from an item.")]
        public Func<object, string> DisplaySelector
        {
            get { return m_DisplaySelector; }
            set { m_DisplaySelector = value; }
        }

        [Category("Data")]
        [Description("Delegate that selects the image URL from an item.")]
        public Func<object, string> ImageUrlSelector
        {
            get { return m_ImageUrlSelector; }
            set { m_ImageUrlSelector = value; }
        }

        [Category("Appearance")]
        [Description("Title displayed above the list (e.g., \"רשימת חברים\").")]
        public string TitleText
        {
            get { return m_TitleText; }
            set
            {
                m_TitleText = value ?? "Items";
                r_TitleLabel.Text = m_TitleText;
            }
        }

        [Category("Appearance")]
        [Description("The height (in pixels) of the upper area (title+list).")]
        public int SplitterDistance
        {
            get { return r_SplitContainer.SplitterDistance; }
            set { r_SplitContainer.SplitterDistance = Math.Max(100, value); }
        }

        [Browsable(false)]
        public object SelectedItem
        {
            get { return r_ListBox.SelectedItem; }
        }

        // הגדרה מרוכזת
        public void SetData(
            IEnumerable i_Items,
            string i_Title,
            Func<object, string> i_DisplaySelector,
            Func<object, string> i_ImageUrlSelector)
        {
            TitleText = i_Title;
            DisplaySelector = i_DisplaySelector;
            ImageUrlSelector = i_ImageUrlSelector;
            DataSource = i_Items;
        }

        // ---------- Event Handlers ----------

        private void listBox_Format(object sender, ListControlConvertEventArgs e)
        {
            // מציגים רק את הטקסט שהמשתמש מחזיר — בלי "שם:" וכד'
            if (m_DisplaySelector != null && e.ListItem != null)
            {
                e.Value = m_DisplaySelector(e.ListItem) ?? string.Empty;
            }
        }

        private async void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            await loadSelectedItemImageAsync();
        }

        private void panel2_Resize(object sender, EventArgs e)
        {
            // שומר על PictureBox כריבוע ממורכז בתוך Panel2
            int panelW = r_SplitContainer.Panel2.ClientSize.Width;
            int panelH = r_SplitContainer.Panel2.ClientSize.Height;
            int size = Math.Min(panelW, panelH);

            r_PictureBox.Size = new Size(size, size);
            r_PictureBox.Left = (panelW - size) / 2;
            r_PictureBox.Top = (panelH - size) / 2;
        }

        private void selectionWithImagePreview_Disposed(object sender, EventArgs e)
        {
            disposeImage();
        }

        // ---------- Logic ----------

        private async Task loadSelectedItemImageAsync()
        {
            disposeImage();

            object item = r_ListBox.SelectedItem;
            if (item == null || m_ImageUrlSelector == null)
            {
                return;
            }

            string url = m_ImageUrlSelector(item);
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            Image downloadedImage = await downloadImageAsync(url);
            if (downloadedImage != null)
            {
                m_CurrentImage = downloadedImage;
                r_PictureBox.Image = downloadedImage;

                // לוודא שהמסגרת מעודכנת לגודל הנוכחי
                panel2_Resize(this, EventArgs.Empty);
            }
        }

        private void disposeImage()
        {
            if (m_CurrentImage != null)
            {
                r_PictureBox.Image = null;
                m_CurrentImage.Dispose();
                m_CurrentImage = null;
            }
        }

        private async Task<Image> downloadImageAsync(string i_Url)
        {
            Image retImage;

            try
            {
                using (HttpClient client = new HttpClient())
                using (var stream = await client.GetStreamAsync(i_Url))
                {
                    retImage = new Bitmap(Image.FromStream(stream));
                }
            }
            catch
            {
                retImage = null;
            }

            return retImage;
        }

        private void InitializeComponent()
        {
            this.TitleLabel = new System.Windows.Forms.Label();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(20, 15);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(30, 15);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Title";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(23, 42);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(346, 261);
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // SelectionWithImagePreview
            // 
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.TitleLabel);
            this.Name = "SelectionWithImagePreview";
            this.Size = new System.Drawing.Size(400, 332);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
