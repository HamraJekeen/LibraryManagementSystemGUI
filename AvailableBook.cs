using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LibrarySystemManagement
{
    public partial class AvailableBook : Form
    {
        public AvailableBook()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void AvailableBook_Load(object sender, EventArgs e)
        {
            Book.LoadDataIntoDataGridView(availableBookstable);
        }

        private void availableBookstable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
