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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblRefresh_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm fm = new MainForm();
            fm.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string queryCar = "Select Count(*) from CarTb";
            SqlDataAdapter sda = new SqlDataAdapter(queryCar, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Carlbl.Text = dt.Rows[0][0].ToString();

            string queryCust= "Select Count(*) from CustomerTb";
            SqlDataAdapter sdal = new SqlDataAdapter(queryCust, Con);
            DataTable dt1 = new DataTable();
            sdal.Fill(dt1);
            Custlbl.Text = dt1.Rows[0][0].ToString();


            string queryUser = "Select Count(*) from UserTb";
            SqlDataAdapter sda2 = new SqlDataAdapter(queryUser, Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            Userlbl.Text = dt2.Rows[0][0].ToString();
        }
    }
}
