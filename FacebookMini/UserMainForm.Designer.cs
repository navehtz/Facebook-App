namespace FacebookMini
{
    partial class UserMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectionWithImagePreview1 = new FacebookMini.MyComponents.SelectionWithImagePreview();
            this.selectionWithImagePreview2 = new FacebookMini.MyComponents.SelectionWithImagePreview();
            this.selectionWithImagePreview3 = new FacebookMini.MyComponents.SelectionWithImagePreview();
            this.SuspendLayout();
            // 
            // selectionWithImagePreview1
            // 
            this.selectionWithImagePreview1.DataSource = null;
            this.selectionWithImagePreview1.DisplaySelector = null;
            this.selectionWithImagePreview1.ImageUrlSelector = null;
            this.selectionWithImagePreview1.Location = new System.Drawing.Point(1014, 157);
            this.selectionWithImagePreview1.Name = "selectionWithImagePreview1";
            this.selectionWithImagePreview1.Size = new System.Drawing.Size(151, 231);
            this.selectionWithImagePreview1.SplitterDistance = 164;
            this.selectionWithImagePreview1.TabIndex = 1;
            this.selectionWithImagePreview1.TitleText = "Items";
            this.selectionWithImagePreview1.Load += new System.EventHandler(this.selectionWithImagePreview1_Load);
            // 
            // selectionWithImagePreview2
            // 
            this.selectionWithImagePreview2.DataSource = null;
            this.selectionWithImagePreview2.DisplaySelector = null;
            this.selectionWithImagePreview2.ImageUrlSelector = null;
            this.selectionWithImagePreview2.Location = new System.Drawing.Point(817, 157);
            this.selectionWithImagePreview2.Name = "selectionWithImagePreview2";
            this.selectionWithImagePreview2.Size = new System.Drawing.Size(151, 231);
            this.selectionWithImagePreview2.SplitterDistance = 164;
            this.selectionWithImagePreview2.TabIndex = 2;
            this.selectionWithImagePreview2.TitleText = "Items";
            this.selectionWithImagePreview2.Load += new System.EventHandler(this.selectionWithImagePreview2_Load);
            // 
            // selectionWithImagePreview3
            // 
            this.selectionWithImagePreview3.DataSource = null;
            this.selectionWithImagePreview3.DisplaySelector = null;
            this.selectionWithImagePreview3.ImageUrlSelector = null;
            this.selectionWithImagePreview3.Location = new System.Drawing.Point(610, 157);
            this.selectionWithImagePreview3.Name = "selectionWithImagePreview3";
            this.selectionWithImagePreview3.Size = new System.Drawing.Size(151, 231);
            this.selectionWithImagePreview3.SplitterDistance = 164;
            this.selectionWithImagePreview3.TabIndex = 3;
            this.selectionWithImagePreview3.TitleText = "Items";
            this.selectionWithImagePreview3.Load += new System.EventHandler(this.selectionWithImagePreview3_Load);
            // 
            // UserMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 773);
            this.Controls.Add(this.selectionWithImagePreview3);
            this.Controls.Add(this.selectionWithImagePreview2);
            this.Controls.Add(this.selectionWithImagePreview1);
            this.Name = "UserMainForm";
            this.Text = "UserMainForm";
            this.Load += new System.EventHandler(this.UserMainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private MyComponents.SelectionWithImagePreview selectionWithImagePreview1;
        private MyComponents.SelectionWithImagePreview selectionWithImagePreview2;
        private MyComponents.SelectionWithImagePreview selectionWithImagePreview3;
    }
}