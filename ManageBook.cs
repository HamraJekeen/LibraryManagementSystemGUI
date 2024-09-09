using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibrarySystemManagement
{
    public partial class ManageBook : Form
    {
       
        
       
        public ManageBook()
        {

            InitializeComponent();
        }
        public DataGridView GetAvailableBooksDataGridView()
        {
            return availableBookstable;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-T44UTOP\\SQLEXPRESS01;Initial Catalog=MyLibrary;Integrated Security=True"))
                {
                    con.Open();

                    // Check if the ID already exists in the database
                    using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Books WHERE ID = @ID", con))
                    {
                        checkCmd.Parameters.AddWithValue("@ID", txtID.Text);
                        int existingCount = (int)checkCmd.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            MessageBox.Show("Book with the same ID already exists. Please use a different ID.");
                            return; // Exit the method to avoid adding the duplicate entry
                        }
                    }

                    // If ID is unique, proceed with adding the book
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Books VALUES(@ID, @Title, @Author, @ISBN, @Status)", con))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtID.Text);
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);

                        int isbn;
                        if (int.TryParse(txtISBN.Text, out isbn))
                        {
                            cmd.Parameters.AddWithValue("@ISBN", isbn);
                            cmd.Parameters.AddWithValue("@Status", "Available");

                            cmd.ExecuteNonQuery();
                            Book.LoadDataIntoDataGridView(availableBookstable);
                            MessageBox.Show("Successfully added");
                            ClearBookDetails();
                        }
                        else
                        {
                            MessageBox.Show("Invalid ISBN. Please enter a valid integer.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding book: " + ex.Message);
            }
        }

        private void btnRemoveBook_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-T44UTOP\\SQLEXPRESS01;Initial Catalog=MyLibrary;Integrated Security=True"))
                {
                    con.Open();

                    // Check if the ID exists in the database before attempting to delete
                    using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Books WHERE ID = @ID", con))
                    {
                        checkCmd.Parameters.AddWithValue("@ID", txtRemoveID.Text);
                        int existingCount = (int)checkCmd.ExecuteScalar();

                        if (existingCount == 0)
                        {
                            MessageBox.Show("Book with the specified ID does not exist. Please enter a valid ID.");
                            return; // Exit the method to avoid attempting to remove a non-existent entry
                        }
                    }

                    // If ID exists, proceed with removing the book
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE ID = @ID", con))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtRemoveID.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Successfully removed");
                Book.LoadDataIntoDataGridView(availableBookstable);
                ClearBookDetails();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing book: " + ex.Message);
            }
        }


        private void availableBookstable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ManageBook_Load(object sender, EventArgs e)
        {
            Book.LoadDataIntoDataGridView(availableBookstable);

        }
        private void ClearBookDetails()
        {
            txtID.Text = "";
            txtTitle.Text = "";
            txtAuthor.Text = "";
            txtISBN.Text = "";
            txtRemoveID.Text = "";
        }

        private void btnAddClear_Click(object sender, EventArgs e)
        {
            ClearBookDetails();
        }

        private void btnRemoveClear_Click(object sender, EventArgs e)
        {
            ClearBookDetails();
        }
    }
    }

