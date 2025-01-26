using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace asdf
{
    public partial class Form1 : Form
    {
        private string dbPath;
        private SqlConnection Con;

        public static string user;

        public Form1()
        {
            InitializeComponent();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            dbPath = System.IO.Path.Combine(basePath, "Cafedb.mdf");
            Con = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True;");
        }

        private void label7_Click(object sender, EventArgs e) // Wyjście z programu
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)  // Weryfikacja loginu i hasła użytkownika z bazą danych
        {
            if (UnameTb.Text == "" || UnameTb.Text == "")
            {
                MessageBox.Show("Fill all fields");
            }
            else
            {
                user = UnameTb.Text;
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UsersTbl where Uname='"+UnameTb.Text+"' and Upassword='"+ PasswordTb.Text+"'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    UserOrder uorder = new UserOrder();
                    uorder.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong username or password");
                }
                Con.Close();
            }

        }

        private void PasswordTb_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UnameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
