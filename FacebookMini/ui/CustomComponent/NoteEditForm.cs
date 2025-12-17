using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMini.ui.CustomComponent
{
    public partial class NoteEditForm : Form
    {
        public string NoteText => textBoxNote.Text;
        
        public NoteEditForm(string i_AllTags)
        {
            InitializeComponent();
            textBoxNote.Text = i_AllTags ?? string.Empty;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}