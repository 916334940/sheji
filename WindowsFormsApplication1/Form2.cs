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
    public partial class Form2 : Form
    {
        SqlConnection sqlCon = new SqlConnection();//连接  
        SqlDataAdapter da = null;
        DataSet ds = null;
        DataSet ds1 = null;
        string zh="";
        public Form2(string id )
        {
            this.zh = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            try
            {
                SqlConnection conn = new SqlConnection(constr);
                int id = this.comboBox1.Items.Count + 1;
                conn.Open();
                string cmdStr = "insert into course values( '" + id + "','" + textBox1.Text.Trim() + "')";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("未知错误!");
                    return;
                }
                else
                {
                    MessageBox.Show("增加课程成功!");
                    this.Hide();
                    Form2 form = new Form2( zh);
                    form.Show();
                }
                conn.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("" + error);
            }

        }




        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            try
            {
                SqlConnection conn = new SqlConnection(constr);
                int id = this.comboBox1.Items.Count + 1;
                conn.Open();
                string cmdStr = "select 名称 from course";
                 da = new SqlDataAdapter(cmdStr, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox1.Items.Insert(i,dt.Rows[i][0]);
                }
                conn.Close();


            }
            catch (Exception error)
            {
                MessageBox.Show("" + error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            try
            {
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                int id = Convert.ToInt16(comboBox1.SelectedIndex+1);
                string cmdStr = "select * from xuanze where 课程编号 ='"+ id + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmdStr, conn);
                 ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

                string cmdStr1 = "select * from judge where 课程编号 ='" + id + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(cmdStr1, conn);
                 ds1 = new DataSet();
                da1.Fill(ds1);
                dataGridView2.DataSource = ds1.Tables[0];
                conn.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("" + error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            DataTable dt = ds.Tables[0];
            if (dataGridView1.Rows.Count <= 0 || dataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }
            try
            {
                DialogResult result = MessageBox.Show("确认删除吗？", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    //定义一个数组保存所选中的行 
                    int[] sel_rows = new int[dataGridView1.SelectedRows.Count];
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        sel_rows[i] = dataGridView1.SelectedRows[i].Index;
                    }

                    //根据数组选择所得到的行号删除数据表 
                    for (int i = 0; i < sel_rows.Length; i++)
                    {

                        int cid = Convert.ToInt32(dt.Rows[sel_rows[i]].ItemArray.First());
                        int id = Convert.ToInt16(comboBox1.ValueMember+1);

                      //  string del1 = "delete  from xc where 选择题编号='" + cid + "'and 课程编号 ='" + id + "'";
                     //   SqlCommand cmd1 = new SqlCommand(del1, conn);
                     //   cmd1.ExecuteNonQuery();
                        MessageBox.Show("1");
                        string del = "delete from xuanze where 编号='" + cid + "'";
                        SqlCommand cmd = new SqlCommand(del, conn);
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            MessageBox.Show("未知错误!");
                            return;
                        }
                        else
                        {
                            this.Close();
                            Form2 form1 = new Form2(zh);
                            form1.Show();
                            MessageBox.Show("课程删除成功");
                        }
                    }
                }
                
            }
            catch (Exception error)
            {
                MessageBox.Show("删除失败" + error);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            DataTable dt = ds1.Tables[0];
            if (dataGridView2.Rows.Count <= 0 || dataGridView2.SelectedRows.Count <= 0)
            {
                return;
            }
            try
            {
                DialogResult result = MessageBox.Show("确认删除吗？", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    //定义一个数组保存所选中的行 
                    int[] sel_rows = new int[dataGridView2.SelectedRows.Count];
                    for (int i = 0; i < dataGridView2.SelectedRows.Count; i++)
                    {
                        sel_rows[i] = dataGridView2.SelectedRows[i].Index;
                    }

                    //根据数组选择所得到的行号删除数据表 
                    for (int i = 0; i < sel_rows.Length; i++)
                    {

                        int cid = Convert.ToInt32(dt.Rows[sel_rows[i]].ItemArray.First());
                        int id = Convert.ToInt16(comboBox1.ValueMember + 1);

                        //  string del1 = "delete  from xc where 选择题编号='" + cid + "'and 课程编号 ='" + id + "'";
                        //   SqlCommand cmd1 = new SqlCommand(del1, conn);
                        //   cmd1.ExecuteNonQuery();
                        MessageBox.Show("1");
                        string del = "delete from judge where 编号='" + cid + "'";
                        SqlCommand cmd = new SqlCommand(del, conn);
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            MessageBox.Show("未知错误!");
                            return;
                        }
                        else
                        {
                            this.Close();
                            Form2 form1 = new Form2(zh);
                            form1.Show();
                            MessageBox.Show("课程删除成功");
                        }
                    }
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("删除失败" + error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
          //  MessageBox.Show(comboBox1.ValueMember+1);
            Form4 form = new Form4(Convert.ToString(comboBox1.SelectedIndex+1), zh);
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            //  MessageBox.Show(comboBox1.ValueMember+1);
            Form5 form = new Form5(comboBox1.SelectedItem.ToString(), zh);
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
 


