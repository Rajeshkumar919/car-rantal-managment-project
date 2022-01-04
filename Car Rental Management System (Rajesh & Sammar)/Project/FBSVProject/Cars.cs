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
    public partial class Cars : Form
    {
        public Cars()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        //Populate Method for Displaying Details of User on DataGridView
        private void Populate()
        {
            con.Open();
            string query = "select * from CarTb";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void fillAvailble()
        {
            con.Open();
            string query = "select  Available from CarTb";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Available", typeof(String));
            dt.Load(rdr);
            comboBoxRefresh.ValueMember = "RegNum";

            comboBoxRefresh.DataSource = dt;
            con.Close();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm fm = new MainForm();
            fm.Show();
        }

        private void Cars_Load(object sender, EventArgs e)
        {
            Populate();
            fillAvailble();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtBoxReg.Text == "" || txtBoxBrnd.Text == "" || txtBoxMod.Text == "" || txtBoxPrc.Text == "")
            {
                MessageBox.Show("Please fill all fields !");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into CarTb values(" + txtBoxReg.Text + ", '" + txtBoxBrnd.Text + "', '" + txtBoxMod.Text + "', '" + txtBoxPrc.Text + "', '" + AvailableCb.SelectedItem.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Added!");
                    con.Close();
                    Populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtBoxReg.Text == "")
            {
                MessageBox.Show("Record isn't available to be deleted !");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from CarTb where RegNum=" + txtBoxReg.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Details Deleted Successfully!");
                    con.Close();
                    Populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (txtBoxReg.Text == "" || txtBoxBrnd.Text == "" || txtBoxMod.Text == "" || txtBoxPrc.Text == "")
            {
                MessageBox.Show("Nothing to be Updated!");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update CarTb set RegNum='" + txtBoxReg.Text + "', Brand='" + txtBoxBrnd.Text + "', Model='" + txtBoxMod.Text + "', Price='" + txtBoxPrc.Text + "', Available='" + AvailableCb.SelectedItem.ToString() + "'  where RegNum= '" + txtBoxReg.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Details Successfully Updated!");
                    con.Close();
                    Populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void CarDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBoxReg.Text = CarDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtBoxBrnd.Text = CarDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtBoxMod.Text = CarDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtBoxPrc.Text = CarDGV.SelectedRows[0].Cells[4].Value.ToString();
            AvailableCb.SelectedItem = CarDGV.SelectedRows[3].Cells[3].Value.ToString();
        }

        private void lblRefresh_Click(object sender, EventArgs e)
        {
            Populate();
        }

        private void s(object sender, EventArgs e)
        {

        }

        private void comboBoxRefresh_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string flag = "";
            if(comboBoxRefresh.SelectedItem.ToString()=="Available")
            {

                flag = "Yes";
            }
            else
            {
                flag = "No";
            }

            con.Open();
            string query = "select * from CarTb where Available='"+flag+"'";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarDGV.DataSource = ds.Tables[0];
            con.Close();
        }
    }
}
