using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibrarySystemManagement
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAvailable_Click(object sender, EventArgs e)
        {
            AvailableBook availableBook = new AvailableBook();
            availableBook.Show();
            this.Hide();
        }

        private void btnbook_Click(object sender, EventArgs e)
        {
            ManageBook manBook = new ManageBook();
            manBook.Show();
            this.Hide();
        }

        private void btntran_Click(object sender, EventArgs e)
        {
            Transaction trans = new Transaction();
            trans.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
