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
    public partial class Form6 : Form
    {
        
        string zh;
        string id;//课程号
        int id1;//题目号
        int r;
        int w;
        public Form6(string id,int id1,string zh,int r,int w)
        {
            this.id = id;
            this.id1 = id1;
            this.zh=zh;
            this.r = r;
            this.w = w;
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            try
            {
                SqlConnection conn = new SqlConnection(constr);

                conn.Open();

                string cmdStr = "select * from xuanze  where 课程编号 ='" + id + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmdStr, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                textBox1.Text = ds.Tables[0].Rows[id1][1].ToString();
                textBox2.Text = ds.Tables[0].Rows[id1][2].ToString();
                textBox3.Text = ds.Tables[0].Rows[id1][3].ToString();
                textBox4.Text = ds.Tables[0].Rows[id1][4].ToString();
                textBox5.Text = ds.Tables[0].Rows[id1][5].ToString();
             //   if(ds.Tables[0].Rows[0][6]==textBox6.Text)用来增加成绩

                conn.Close();

            }
            catch (Exception error)
            {
                MessageBox.Show("" + error);
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show(this.textBox1, "填空不可以为空");
                return;

            }
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            try
            {
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string cmdStr = "select * from xuanze  where 课程编号 ='" + id + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmdStr, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (textBox6.Text.ToString() == ds.Tables[0].Rows[id1][6].ToString())
                {
                    MessageBox.Show("恭喜你回答正确！");
                    r++;
                }
                else
                {
                    MessageBox.Show(string.Format("很可惜回答错误，正确答案为{0}!", ds.Tables[0].Rows[id1][6].ToString()),"提示");
                    w++;
                }

                string sql = "SELECT COUNT(*) FROM xuanze where 课程编号='" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                //   MessageBox.Show(string.Format("数据库里共有{0}条记录", cmd.ExecuteScalar()), "提示");
                if (id1 >= (Convert.ToInt16(cmd.ExecuteScalar()) - 1))
                {
                    this.Hide();
                    Form3 form1 = new Form3(zh);
                    form1.Show();
                }
                else
                {
                    this.Hide();
            //  MessageBox.Show(comboBox1.ValueMember+1);
            Form6 form = new Form6(id, id1+1,zh,w,r);
            form.Show();
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("" + error);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form1 = new Form3(zh);
            form1.Show();
        }
    }
}
