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

namespace asdf
{
    public partial class UsersForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        public UsersForm()
        {
            InitializeComponent();
        }

        public void populate() // Metoda wypełniania pola danymi z bazy danych
        {
            Con.Open();
            string query = "select * from UsersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button3_Click(object sender, EventArgs e) // Kliknięcie przycisku otwiera okno UserOrder
        {
            UserOrder uorder = new UserOrder();
            uorder.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e) // Kliknięcie przycisku otwiera okno ItemsForm
        {
            ItemsForm item = new ItemsForm();
            item.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e) // Wyłączanie programu
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e) // Powrót do ekranu startowego
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e) // Dodawanie użytkownika
        {
            Con.Open();
            string query = "insert into UsersTbl values('" + uNameTb.Text + "','" + uPhoneTb.Text + "','" + uPassTb.Text + "')";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("User created");
            Con.Close();
            populate();
        }


        private void UsersForm_Load_1(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click_1(object sender, EventArgs e) // Usuwanie użytkownika
        {
            if (uPhoneTb.Text == "")
            {
                MessageBox.Show("Select a user to delete");
            }
            else
            {
                Con.Open();
                string query = "delete from UsersTbl where Uphone = '" + uPhoneTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populate();
            }
        }

        private void button2_Click_1(object sender, EventArgs e) // Zmiana danych użytkownika
        {
            if (uNameTb.Text == "" || uPhoneTb.Text == "" || uPassTb.Text == "")
            {
                MessageBox.Show("Fill all fields");
            }
            else
            {
                Con.Open();
                string query = "update UsersTbl set Uname='" + uNameTb.Text + "', Uphone='" + uPhoneTb.Text + "' where Upassword='" + uPassTb.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populate();
            }
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e) // Wypełnienie wymaganych pól danymi z bazy danych poprzez kliknięcie na tabelę.
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = UsersGV.Rows[e.RowIndex];
                uNameTb.Text = row.Cells[0].Value?.ToString();
                uPhoneTb.Text = row.Cells[1].Value?.ToString();
                uPassTb.Text = row.Cells[2].Value?.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void uPassTb_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
