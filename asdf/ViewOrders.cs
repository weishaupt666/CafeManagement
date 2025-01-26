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
    public partial class ViewOrders : Form
    {
        private string dbPath;
        private SqlConnection Con;
        public ViewOrders()
        {
            InitializeComponent();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            dbPath = System.IO.Path.Combine(basePath, "Cafedb.mdf");
            Con = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True;");
        }
        public void populate() // Metoda wypełniania pola danymi z tabeli OrdersTbl
        {
            Con.Open();
            string query = "select * from OrdersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            OrdersGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e) // Zamykanie okna statystyk
        {
            this.Hide();
        }

        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            populate();
        }
    }
}
