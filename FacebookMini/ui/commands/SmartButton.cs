using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMini.ui.commands
{
    public class SmartButton : Button
    {
        private CommandObserver m_CommandHolder;
        private Button m_BasicButton;

        public ICommand Command
        {
            set { m_CommandHolder.Command = value; }
        }

        public SmartButton(Button i_Button, ICommand i_Command)
        {
            m_BasicButton = i_Button;
            i_Command.Title = i_Button.Text;
            m_CommandHolder = CommandObserver.CreateCommandHolder(this);
            m_CommandHolder.Command = i_Command;
            replaceBasicItem();
        }

        private void replaceBasicItem()
        {
            this.Cursor = m_BasicButton.Cursor;
            this.Dock = m_BasicButton.Dock;
            this.FlatStyle = m_BasicButton.FlatStyle;
            this.Font = m_BasicButton.Font;
            this.ForeColor = m_BasicButton.ForeColor;
            this.Location = m_BasicButton.Location;
            this.Margin = m_BasicButton.Margin;
            this.Name = m_BasicButton.Name;
            this.Padding = m_BasicButton.Padding;
            this.Size = m_BasicButton.Size;
            this.TabIndex = m_BasicButton.TabIndex;
            this.Text = m_BasicButton.Text;
            this.TextAlign = m_BasicButton.TextAlign;
            this.UseVisualStyleBackColor = false;
            this.BackColor = m_BasicButton.BackColor;
            this.FlatAppearance.BorderSize = m_BasicButton.FlatAppearance.BorderSize;
            this.FlatAppearance.MouseOverBackColor = m_BasicButton.FlatAppearance.MouseOverBackColor;
            this.FlatAppearance.MouseDownBackColor = m_BasicButton.FlatAppearance.MouseDownBackColor;

            m_BasicButton.Visible = false;
            m_BasicButton.Enabled = false;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            m_CommandHolder.Command.Execute(null);
        }
    }
}





//this.buttonLogout.Cursor = System.Windows.Forms.Cursors.Hand;
//this.buttonLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
//this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//this.buttonLogout.Font = new System.Drawing.Font("Segoe UI", 10F);
//this.buttonLogout.ForeColor = System.Drawing.Color.White;
//this.buttonLogout.Location = new System.Drawing.Point(0, 988);
//this.buttonLogout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//this.buttonLogout.Name = "buttonLogout";
//this.buttonLogout.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
//this.buttonLogout.Size = new System.Drawing.Size(270, 69);
//this.buttonLogout.TabIndex = 5;
//this.buttonLogout.Text = "Logout";
//this.buttonLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//this.buttonLogout.UseVisualStyleBackColor = true;