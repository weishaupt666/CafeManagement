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
using System.Text.RegularExpressions;

namespace asdf
{
    public partial class UserOrder : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        public UserOrder()
        {
            InitializeComponent();
        }
        public void populate()
        {
            Con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ItemsForm Item = new ItemsForm();
            Item.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UsersForm Item = new UsersForm();
            Item.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        DataTable table = new DataTable();
        int flag = 0;
        int sum = 0;
        private void UserOrder_Load(object sender, EventArgs e)
        {
            populate();
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("Item", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("UnitPrice", typeof(int));
            table.Columns.Add("Total", typeof(int));
            OrdersGV.DataSource = table;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")
            {
                MessageBox.Show("Add quantity");
            }
            else if(flag == 0)
            {
                MessageBox.Show("Select a product");
            }
            else
            {
                num += 1;
                total = price * Int32.Parse(QtyTb.Text);
                table.Rows.Add(num, item, cat, price, total);
                OrdersGV.DataSource = table;
                flag = 0;
            }
            sum += total;
            LabelAmnt.Text = "Sum " + sum;
        }

        int num = 0;
        int price, qty, total;
        string item, cat;

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ItemsGV.Rows[e.RowIndex];
                item = row.Cells[1].Value?.ToString();
                cat = row.Cells[3].Value?.ToString();
                price = Int32.Parse(row.Cells[2].Value?.ToString());
                flag = 1;
            }
        }

        
    }
}
