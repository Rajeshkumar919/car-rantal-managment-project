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
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void fillcombo()
        {
            Con.Open();
            string query = "select  RegNum from CarTb";
            SqlCommand cmd = new SqlCommand(query,Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNum", typeof(String));
            dt.Load(rdr);
            CarRegCb.ValueMember = "RegNum";

            CarRegCb.DataSource = dt;
            Con.Close();
        }

        private void fillcustomer()
        {
            Con.Open();
            string query = "select  CustId from CustomerTb where Available'"+"Yes"+"'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(rdr);
            CustCb.ValueMember = "CustId";

            CustCb.DataSource = dt;
            Con.Close();
        }
        private void fatchCustName()
        {
            Con.Open();
            string query = "select * from CustomerTb where CustId= "+CustCb.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Con.Close();
            foreach(DataRow dr in dt.Rows)
            {
                CustName.Text = dr["CustName"].ToString();
            }
            Con.Close();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtBoxCID.Text == "" || CarRegCb.Text == "" || CustName.Text == "" || feesTb.Text == "")
            {
                MessageBox.Show("Please fill all fields !");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into RentalTb values(" + txtBoxCID.Text + ", '" + CarRegCb.SelectedValue.ToString() + "', '" + CustName.Text + "', '" + rentDate.Text + "', '" + returnDate.Text + "',"+feesTb.Text+ ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Rented!");
                    Con.Close();
                    UpdateonRent();
                    Populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }
        private void Populate()
        {
            Con.Open();
            string query = "select * from RentalTb";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void UpdateonRent()
        {
            Con.Open();
             string query = "update CarTb set Available='"+"No"+"'where RegNum='"+CarRegCb.SelectedValue.ToString()+"';";


        SqlCommand cmd = new SqlCommand(query, Con);
        cmd.ExecuteNonQuery();
                   // MessageBox.Show("Car Details Successfully Updated!");
                    Con.Close();
        }
        private void UpdateonRentDelete()
        {
            Con.Open();
            string query = "update CarTb set Available='" + "Yes" + "'where RegNum='" + CarRegCb.SelectedValue.ToString() + "';";


            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            // MessageBox.Show("Car Details Successfully Updated!");
            Con.Close();
        }
        private void Rental_Load(object sender, EventArgs e)
        {
            fillcombo();
            fillcustomer();
            Populate();

        }

        private void CustCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CustName_TextChanged(object sender, EventArgs e)
        {
            fatchCustName();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtBoxCID.Text == "")
            {
                MessageBox.Show("Record isn't available to be deleted !");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from RentalTb where RentID=" + txtBoxCID.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rental Details Deleted Successfully!");
                    Con.Close();
                    Populate();
                    UpdateonRentDelete();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /* CostumerId.Text = RentDGV.SelectedRows[0].Cells[1].Value.ToString();
       CarRegCb.SelectedValue=RentDGV.SelectedRows[0].Cells[0].Value.ToString();
           CustName.Text=RentDGV.SelectedRows[0].Cell[3].Value.ToString();
            FeesTb.Text=RentDGV.SelectedRows[0].Cells[5].Value.ToString();
        */
        }
    }
    }

