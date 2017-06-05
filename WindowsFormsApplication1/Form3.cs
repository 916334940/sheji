using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        int right = 0;
        int wrong = 0;
        string zh="";
        public Form3(string id)
        {
            this.zh = id;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            try
            {
                SqlConnection conn = new SqlConnection(constr);
                int id = this.comboBox1.Items.Count + 1;
                conn.Open();
                string cmdStr = "select 名称 from course";
                SqlDataAdapter da = new SqlDataAdapter(cmdStr, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox1.Items.Insert(i, dt.Rows[i][0]);
                }
                comboBox1.SelectedIndex = 0;
                conn.Close();


            }
            catch (Exception error)
            {
                MessageBox.Show("" + error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            //  MessageBox.Show(comboBox1.ValueMember+1);
            Form6 form = new Form6(Convert.ToString(comboBox1.SelectedIndex+1),0,zh,right,wrong);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            Form7 form = new Form7(Convert.ToString(comboBox1.SelectedIndex+1), 0,zh,right,wrong);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if((right+wrong)==0)
                MessageBox.Show("当前正确率为0" );
            else
            MessageBox.Show("当前正确率为{0}"+ (right / (right + wrong)));
        }
    }
}
