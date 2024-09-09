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
    public partial class Transaction : Form
    {
        public Transaction()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Transaction_Load(object sender, EventArgs e)
        {
            LoadBorrowtable();
            Book.LoadDataIntoDataGridView(availableBookstable);

        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-T44UTOP\\SQLEXPRESS01;Initial Catalog=MyLibrary;Integrated Security=True");
            //con.Open();

            //SqlCommand borrowCmd = new SqlCommand("INSERT INTO Borrowers (BorrowerID, BookID, BorrowDate) VALUES (@BorrowerID, @BookID, @BorrowDate)", con);
            //borrowCmd.Parameters.AddWithValue("@BorrowerID", txtBorrowerID.Text);
            //borrowCmd.Parameters.AddWithValue("@BookID", txtID.Text);
            //borrowCmd.Parameters.AddWithValue("@BorrowDate",DateTime.Parse(borrowDate.Text));
            //borrowCmd.ExecuteNonQuery();

            string borrowerId = txtBorrowerID.Text;
            string bookId = txtID.Text;
            DateTime borrowDate = DateTime.Now;

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-T44UTOP\\SQLEXPRESS01;Initial Catalog=MyLibrary;Integrated Security=True"))
            {
                con.Open();

                // Check if the book is available
                SqlCommand checkAvailabilityCmd = new SqlCommand("SELECT Status FROM Books WHERE ID = @BookID", con);
                checkAvailabilityCmd.Parameters.AddWithValue("@BookID", bookId);
                object statusObj = checkAvailabilityCmd.ExecuteScalar();

                if (statusObj == null || statusObj == DBNull.Value)
                {
                    MessageBox.Show("This book doesn't exist in the library.");
                    return;
                }

                string bookStatus = (string)statusObj;

                if (bookStatus != "Available")
                {
                    MessageBox.Show("This book is not available for borrowing.");
                    return;
                }

                // Insert into Borrowers table
                SqlCommand borrowCmd = new SqlCommand("INSERT INTO Borrowers (BorrowerID, BookID, BorrowDate) VALUES (@BorrowerID, @BookID, @BorrowDate)", con);
                borrowCmd.Parameters.AddWithValue("@BorrowerID", borrowerId);
                borrowCmd.Parameters.AddWithValue("@BookID", bookId);
                borrowCmd.Parameters.AddWithValue("@BorrowDate", borrowDate);
                borrowCmd.ExecuteNonQuery();

                // Update book status to 'Borrowed'
                SqlCommand updateBookCmd = new SqlCommand("UPDATE Books SET Status = 'Borrowed' WHERE ID = @BookID", con);
                updateBookCmd.Parameters.AddWithValue("@BookID", bookId);
                updateBookCmd.ExecuteNonQuery();
                LoadBorrowtable();

                con.Close();

                // Refresh DataGridView or perform any other necessary actions
                Book.LoadDataIntoDataGridView(availableBookstable);

                MessageBox.Show("Book successfully borrowed.");
                ClearBookDetails();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtBorrowerID_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadBorrowtable()
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-T44UTOP\\SQLEXPRESS01;Initial Catalog=MyLibrary;Integrated Security=True"))
            {
                con.Open();

                string query = "SELECT BorrowerID, BookID, BorrowDate FROM Borrowers";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Assuming you have a DataGridView named borrowTable on your form
                borrowTable.DataSource = dataTable;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string bookIdToReturn = ReturnID.Text;


            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-T44UTOP\\SQLEXPRESS01;Initial Catalog=MyLibrary;Integrated Security=True"))
            {
                con.Open();

                // Check if the book is borrowed
                SqlCommand checkBorrowedCmd = new SqlCommand("SELECT BorrowerID FROM Borrowers WHERE BookID = @BookID", con);
                checkBorrowedCmd.Parameters.AddWithValue("@BookID", bookIdToReturn);
                object borrowerIdObj = checkBorrowedCmd.ExecuteScalar();

                if (borrowerIdObj == null || borrowerIdObj == DBNull.Value)
                {
                    MessageBox.Show("This book is not currently borrowed.");
                    return;
                }
                

                // Remove the association with the borrower
                SqlCommand removeBorrowerCmd = new SqlCommand("DELETE FROM Borrowers WHERE BookID = @BookID", con);
                removeBorrowerCmd.Parameters.AddWithValue("@BookID", ReturnID.Text);
                removeBorrowerCmd.ExecuteNonQuery();

                // Update the Books table to mark the book as available
                SqlCommand updateBookCmd = new SqlCommand("UPDATE Books SET Status = 'Available' WHERE ID = @BookID", con);
                updateBookCmd.Parameters.AddWithValue("@BookID", bookIdToReturn);
                updateBookCmd.ExecuteNonQuery();

                con.Close();

                // Refresh DataGridView or perform any other necessary actions
                Book.LoadDataIntoDataGridView(availableBookstable);
                LoadBorrowtable();

                MessageBox.Show("Book successfully returned to the library.");
                ClearBookDetails();

            }
        }

        private void ClearBookDetails()
        {
            txtID.Text = "";
            txtBorrowerID.Text = "";
            ReturnID.Text = "";


        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        

        private void availableBookstable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearBookDetails();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearBookDetails();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
