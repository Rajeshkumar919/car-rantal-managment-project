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

namespace FBSVProject
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        //Populate Method for Displaying Details of User on DataGridView
        private void Populate()
        {
            con.Open();
            string query = "select * from UserTb";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Users_Load(object sender, EventArgs e)
        {
            Populate();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm frm = new MainForm();
            frm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtName.Text =="" || txtUserID.Text=="" || txtPass.Text=="")
            {
                MessageBox.Show("Please fill all fields !");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into UserTb values(" + txtUserID.Text + ", '" + txtName.Text + "', '" + txtPass.Text + "')";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Added!");
                    con.Close();
                    Populate();
                }
                catch(Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtUserID.Text=="")
            {
                MessageBox.Show("Record isn't available to be deleted !");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from UserTb where Id=" + txtUserID.Text + ";";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully!");
                    con.Close();
                    Populate();
                }
                catch(Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*txtUserID.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtName.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtPass.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();*/
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtUserID.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Nothing to be Updated!");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update UserTb set UName='" + txtName.Text + "', UPass='" + txtPass.Text + "'  where Id= '" + txtUserID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Information Successfully Updated!");
                    con.Close();
                    Populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }
    }
}
