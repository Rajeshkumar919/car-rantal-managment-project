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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CarId.Text = RentDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustNameTb.Text = RentDGV.SelectedRows[0].Cells[2].Value.ToString();
            ReturnDate.Text = RentDGV.SelectedRows[0].Cells[4].Value.ToString();
            DateTime d1 = ReturnDate.Value.Date;
            DateTime d2 = DateTime.Now;

            TimeSpan t = d2 - d1;
            int NrofDays = Convert.ToInt32(t.TotalDays);
            if (NrofDays <= 0)
            {
                txtBoxDelay.Text = "0";
                FineTb.Text = "No Fine";
            }
            else
            {
                txtBoxDelay.Text = "" + NrofDays;
                FineTb.Text = "" + (NrofDays * 250);

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
        private void PopulateRet()
        {
            Con.Open();
            string query = "select * from ReturnTb";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void DeleteOnReturn()
        {
            int rentId;
            rentId = Convert.ToInt32(RentDGV.SelectedRows[0].Cells[0].Value.ToString());
                
            Con.Open();
           string query = "delete from RentalTb where RentID=" + CarId.Text + ";";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Rental Details Deleted Successfully!");
            Con.Close();
            Populate();
            //UpdateonRentDelete();


        }
        private void Return_Load(object sender, EventArgs e)
        {
            Populate();
            PopulateRet();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CarId.Text == "" || CustNameTb.Text == "" || FineTb.Text == "" || txtBoxDelay.Text == "")
            {
                MessageBox.Show("Please fill all fields !");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into ReturnTb values(" + CarId.Text + ", '" + CarIdTb.Text + "', '" + CustNameTb.Text + "', '" + ReturnDate.Text + "', '" + txtBoxDelay.Text + "'," + FineTb.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car dully Returned!");
                    Con.Close();
                    // UpdateonRent();
                    PopulateRet();
                    DeleteOnReturn();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
