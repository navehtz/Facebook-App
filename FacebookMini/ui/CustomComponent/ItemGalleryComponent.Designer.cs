namespace FacebookWinFormsApp.CustomComponent
{
    partial class ItemGalleryComponent
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelItems;
        private System.Windows.Forms.Button btnAddToFavorites;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panelItems = new System.Windows.Forms.Panel();
            this.btnAddToFavorites = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelItems
            // 
            this.panelItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelItems.Location = new System.Drawing.Point(0, 0);
            this.panelItems.Name = "panelItems";
            this.panelItems.Size = new System.Drawing.Size(154, 260);
            this.panelItems.TabIndex = 0;
            // 
            // vScrollBar
            //  
            this.btnAddToFavorites.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddToFavorites.Location = new System.Drawing.Point(0, 275);
            this.btnAddToFavorites.Name = "btnAddToFavorites";
            this.btnAddToFavorites.Size = new System.Drawing.Size(183, 25);
            this.btnAddToFavorites.TabIndex = 2;
            this.btnAddToFavorites.Text = "Add to Favorites";
            this.btnAddToFavorites.UseVisualStyleBackColor = true;
            // 
            // ItemGalleryComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelItems);
            this.Name = "ItemGalleryComponent";
            this.Size = new System.Drawing.Size(171, 260);
            this.ResumeLayout(false);
            // 
        }

        #endregion
    }
}
