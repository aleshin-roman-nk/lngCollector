using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lngCollector.WinForm
{
    public partial class EnterWordForm : Form
    {
        public EnterWordForm(EWord w)
        {
            InitializeComponent();

            Word = w;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Word.description = rtxtDescription.Text;
            Word.text = txtText.Text;
            DialogResult = DialogResult.OK;
        }

        public EWord Word { get; }
    }
}
