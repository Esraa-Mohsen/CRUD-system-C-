using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace D2
{
    public partial class Form1 : Form
    {

        SqlConnection con;
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["iticon"].ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlCommand selectcmd = new SqlCommand("select * from Course", con);

            SqlDataAdapter adpter = new SqlDataAdapter();
            adpter.SelectCommand = selectcmd;

            dt = new DataTable();

            adpter.Fill(dt);

            dataGridView1.DataSource = dt;

          //  btn_update.Visible = false;





        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();
            dr["Crs_Name"] = textBox1.Text;
            dr["Crs_Duration"] = textBox2.Text;
            dt.Rows.Add(dr);

            dataGridView1.DataSource = dt;
            textBox1.Text = textBox2.Text = "";
            MessageBox.Show("done");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmdinsert = new SqlCommand("insert into course (Crs_Name,Crs_Duration) values(@Crs_Name,@Crs_Duration)", con);
            cmdinsert.Parameters.Add("Crs_Name", SqlDbType.NVarChar, 50, "Crs_Name");
            cmdinsert.Parameters.Add("Crs_Duration", SqlDbType.Int, 10, "Crs_Duration");

            SqlCommand updatecmd = new SqlCommand("update Course set Crs_Name=@Crs_Name ,Crs_Duration=@Crs_Duration where Crs_Id=@Crs_Id", con);
            updatecmd.Parameters.Add("Crs_Id", SqlDbType.Int, 4, "Crs_Id");
            updatecmd.Parameters.Add("Crs_Name", SqlDbType.NVarChar, 50, "Crs_Name");
            updatecmd.Parameters.Add("Crs_Duration", SqlDbType.Int, 10, "Crs_Duration");

            SqlCommand deletecmd = new SqlCommand("delete from Course where Crs_Id=@Crs_Id", con);
            deletecmd.Parameters.Add("Crs_Id", SqlDbType.Int, 4, "Crs_Id");

            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.InsertCommand = cmdinsert;
            adpt.UpdateCommand = updatecmd;
            adpt.DeleteCommand = deletecmd;
            adpt.Update(dt);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            foreach (DataRow item in dt.Rows)
            {
                if (((int)item["Crs_Id"]) == Crs_Id)
                {
                    item["Crs_Name"] = textBox1.Text;
                    item["Crs_Duration"] = textBox2.Text;

                    button1.Visible = true;
                    button3.Visible = false;
                   textBox2.Text = textBox1.Text = "";
                    dataGridView1.DataSource = dt;
                }
            }





        }
        int Crs_Id;
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Crs_Id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;

            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();


            button3.Visible = true;
            
            button1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataRow item in dt.Rows)
            {
                if (((int)item["Crs_Id"]) == Crs_Id)
                {
                    item.Delete();

                }
            }





        }
    }
}