using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace LibrarySystemManagement
{
    public class Book
    {
        public static void LoadDataIntoDataGridView(DataGridView dataGridView)
    {
        using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-T44UTOP\\SQLEXPRESS01;Initial Catalog=MyLibrary;Integrated Security=True"))
        {
            con.Open();

            string query = "SELECT ID, Title, Author, ISBN,Status FROM Books";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView.DataSource = dataTable;
        }
    }
    }
}
