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
    public partial class ItemsForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        public ItemsForm()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(itemNameTb.Text == "" || itemNumTb.Text == "" || itemPriceTb.Text == "")
            {
                MessageBox.Show("Fill all data");
            }
            else
            {
                Con.Open();
                string query = "insert into ItemTbl values('" + itemNumTb.Text + "','" + itemNameTb.Text + "','" + itemPriceTb.Text + "', '" + catCb.SelectedItem.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show($"Item \"{itemNameTb.Text}\" created");
                Con.Close();
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserOrder order = new UserOrder();
            order.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UsersForm Item = new UsersForm();
            Item.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (itemNameTb.Text == "")
            {
                MessageBox.Show("Select an item to delete");
            }
            else
            {
                Con.Open();
                string query = "delete from ItemTbl where ItemName = '" + itemNameTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show($"Item \"{itemNameTb.Text}\" deleted");
                Con.Close();
                populate();
            }
        }

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ItemsGV.Rows[e.RowIndex];
                itemNumTb.Text = row.Cells[0].Value?.ToString();
                itemNameTb.Text = row.Cells[1].Value?.ToString();
                itemPriceTb.Text = row.Cells[2].Value?.ToString();
                catCb.SelectedItem = row.Cells[3].Value?.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (itemNumTb.Text == "" || itemNameTb.Text == "" || itemPriceTb.Text == "")
            {
                MessageBox.Show("Fill all fields");
            }
            else
            {
                Con.Open();
                string query = "update ItemTbl set ItemNum = '" + itemNumTb.Text +
                                "', ItemCat = '" + catCb.SelectedItem.ToString() +
                                "', ItemPrice = '" + itemPriceTb.Text +
                                "' where ItemName = '" + itemNameTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item updated");
                Con.Close();
                populate();
            }
        }

        private void catCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
