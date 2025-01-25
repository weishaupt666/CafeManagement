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
        DataTable table = new DataTable();
        int flag = 0;
        int sum = 0;
        int num = 0;
        int orderNum = 1;
        int price, qty, total;
        string item, cat;

        public UserOrder()
        {
            InitializeComponent();
        }

        private void GetLastOrderNum()
        {
            Con.Open();
            string query = "SELECT ISNULL(MAX(OrderNum), 1) FROM OrdersTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            object result = cmd.ExecuteScalar(); 
            if (result != null)
            {
                orderNum = Convert.ToInt32(result) + 1;
            }
            else
            {
                orderNum = 1;
            }
            Con.Close();
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

        public void FilterByCategory()
        {
            Con.Open();
            string query = "select * from ItemTbl where Itemcat = '"+ categorycb.SelectedItem.ToString() +"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void UpdateItemQuantity(string itemName, int quantity)
        {
            try
            {
                Con.Open();
                string query = "UPDATE ItemTbl SET ItemNum = ItemNum - @Quantity WHERE ItemName = @ItemName";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@ItemName", itemName);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    
                }
                else
                {
                    MessageBox.Show("Failed to update inventory. Item not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
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
        private void UserOrder_Load(object sender, EventArgs e)
        {
            populate();
            GetLastOrderNum();
            DateLbl.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("Item", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("UnitPrice", typeof(int));
            table.Columns.Add("Total", typeof(int));
            OrdersGV.DataSource = table;
            SellerName.Text = Form1.user;
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
                qty = Int32.Parse(QtyTb.Text);
                num += qty;
                total = price * qty;
                table.Rows.Add(num, item, cat, price, total);
                OrdersGV.DataSource = table;
                UpdateItemQuantity(item, qty);
                flag = 0;
                num = 0;
                populate();
            }
            sum += total;
            LabelAmnt.Text = "Sum " + sum;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByCategory();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void categorycb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void QtyTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "insert into OrdersTbl values('" + orderNum + "','" + DateLbl.Text + "','" + SellerName.Text + "','" + sum + "')";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
            table.Clear();
            OrdersGV.DataSource = table;
            sum = 0;
            LabelAmnt.Text = "Sum ";
            orderNum++;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewOrders view = new ViewOrders();
            view.Show();
        }

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
