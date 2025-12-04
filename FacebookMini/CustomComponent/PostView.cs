using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace FacebookMini.MyComponents
{
    public class PostView : UserControl
    {
        private readonly TableLayoutPanel r_Layout;
        private readonly Label r_TitleLabel;
        private readonly Label r_MessageLabel;
        private readonly PictureBox r_ImageBox;
        private readonly Panel r_FooterPanel;
        private readonly Label r_LikesLabel;
        private readonly Label r_DateLabel;

        private Post m_Post;

        public PostView()
        {
            // General styling
            BackColor = Color.White;
            Padding = new Padding(12);
            DoubleBuffered = true;

            // Main vertical layout
            r_Layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            r_Layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            // Optional title (shown only if provided)
            r_TitleLabel = new Label
            {
                AutoSize = true,
                Font = new Font(Font, FontStyle.Bold),
                Text = "",
                Visible = false,
                Margin = new Padding(0, 0, 0, 6)
            };

            // Post message text
            r_MessageLabel = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(600, 0), // Wrap text
                Text = "",
                Margin = new Padding(0, 0, 0, 8)
            };

            // Post image (hidden if no image exists)
            r_ImageBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Height = 280,
                Visible = false,
                Margin = new Padding(0, 0, 0, 8),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Footer panel (likes + date)
            r_FooterPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 22
            };

            r_LikesLabel = new Label
            {
                AutoSize = true,
                Text = "Likes: 0",
                ForeColor = Color.DimGray
            };

            r_DateLabel = new Label
            {
                AutoSize = true,
                Text = "",
                ForeColor = Color.DimGray,
                Anchor = AnchorStyles.Right
            };

            // Footer layout
            r_FooterPanel.Controls.Add(r_LikesLabel);
            r_FooterPanel.Controls.Add(r_DateLabel);

            r_LikesLabel.Location = new Point(0, 2);

            // Auto-position date on the right side
            r_FooterPanel.Resize += (s, e) =>
            {
                r_DateLabel.Location = new Point(r_FooterPanel.Width - r_DateLabel.Width, 2);
            };

            // Add all components to layout
            r_Layout.Controls.Add(r_TitleLabel, 0, 0);
            r_Layout.Controls.Add(r_MessageLabel, 0, 1);
            r_Layout.Controls.Add(r_ImageBox, 0, 2);
            r_Layout.Controls.Add(r_FooterPanel, 0, 3);

            Controls.Add(r_Layout);

            // Draw border around the control
            Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.Gainsboro))
                {
                    var rect = ClientRectangle;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };
        }

        /// <summary>
        /// Sets a post to display. Title is optional.
        /// If title is null or empty, the title will not be shown.
        /// </summary>
        public void SetPost(Post i_Post, string i_Title = null)
        {
            m_Post = i_Post ?? throw new ArgumentNullException(nameof(i_Post));

            // Title handling
            if (!string.IsNullOrWhiteSpace(i_Title))
            {
                r_TitleLabel.Text = i_Title;
                r_TitleLabel.Visible = true;
            }
            else
            {
                r_TitleLabel.Text = "";
                r_TitleLabel.Visible = false;
            }

            // Message text (fallback to Caption/Description if needed)
            string message = m_Post.Message;
            if (string.IsNullOrWhiteSpace(message))
            {
                message = m_Post.Caption;
                if (string.IsNullOrWhiteSpace(message))
                {
                    message = m_Post.Description;
                }
            }
            r_MessageLabel.Text = message ?? "(No text)";

            // Load image (if available)
            SetImageFromPost(m_Post);

            // Likes
            int likes = m_Post.LikedBy?.Count ?? 0;
            r_LikesLabel.Text = $"Likes: {likes}";

            // Created date
            var created = m_Post.CreatedTime;
            r_DateLabel.Text = created.HasValue
                ? created.Value.ToString("dd/MM/yyyy HH:mm")
                : "";

            r_FooterPanel.PerformLayout();
            r_FooterPanel.Invalidate();
        }

        /// <summary>
        /// Clears the current displayed post.
        /// </summary>
        public void Clear()
        {
            m_Post = null;

            r_TitleLabel.Visible = false;
            r_TitleLabel.Text = "";

            r_MessageLabel.Text = "";

            SetPicture(null);
            r_ImageBox.Visible = false;

            r_LikesLabel.Text = "Likes: 0";
            r_DateLabel.Text = "";
        }

        /// <summary>
        /// Detects which image URL exists in the post and loads it.
        /// Attempts PictureURL first and falls back to Source.
        //</summary>
        private void SetImageFromPost(Post post)
        {
            string url = null;

            // Try primary field
            try { url = post.PictureURL; } catch { }

            // Fallback
            if (string.IsNullOrWhiteSpace(url))
            {
                try { url = post.Source; } catch { }
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                try
                {
                    SetPicture(null); // Clear old image
                    r_ImageBox.Visible = true;
                    r_ImageBox.LoadAsync(url);
                }
                catch
                {
                    r_ImageBox.Visible = false;
                    SetPicture(null);
                }
            }
            else
            {
                r_ImageBox.Visible = false;
                SetPicture(null);
            }
        }

        private void SetPicture(Image img)
        {
            // Prevent memory leaks by disposing previous images
            if (r_ImageBox.Image != null)
            {
                var old = r_ImageBox.Image;
                r_ImageBox.Image = null;
                old.Dispose();
            }

            if (img != null)
            {
                r_ImageBox.Image = img;
                r_ImageBox.Visible = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SetPicture(null);
                r_TitleLabel?.Dispose();
                r_MessageLabel?.Dispose();
                r_ImageBox?.Dispose();
                r_LikesLabel?.Dispose();
                r_DateLabel?.Dispose();
                r_FooterPanel?.Dispose();
                r_Layout?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
