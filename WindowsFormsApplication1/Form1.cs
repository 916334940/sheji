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
    public partial class Form1 : Form
    {
        int count = 0;
        String id = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show(this.textBox1, "账号不可以为空");
                return;

            }


            if (textBox2.Text == "")
            {
                MessageBox.Show(this.textBox2, "密码不可以为空");
                return;
            }
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            try
            {
                SqlConnection conn = new SqlConnection(constr);

                conn.Open();
                if (radioButton1.Checked == true)
                    {
                string cmdStr = "select * from student where 编号='" + this.textBox1.Text + "' and 密码='" + this.textBox2.Text + "'";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);

                SqlDataReader sqlDr = cmd.ExecuteReader();
                if (sqlDr.Read())
                {
                    
                        Form3 form = new Form3(this.textBox1.Text);

                        this.Hide();
                        id = this.textBox1.Text;
                        MessageBox.Show(id + "登录成功");
                        form.Show();
                    }
                }
                else 

                    if (radioButton2.Checked == true)
                    {
                         string cmdStr1 = "select * from teacher where 编号='" + this.textBox1.Text + "' and 密码='" + this.textBox2.Text + "'";
                SqlCommand cmd1 = new SqlCommand(cmdStr1, conn);

                SqlDataReader sqlDr1 = cmd1.ExecuteReader();
                if (sqlDr1.Read())
                {
                        Form2 form = new Form2(this.textBox1.Text);

                        this.Hide();
                        id = this.textBox1.Text;
                        MessageBox.Show(id + "登录成功");
                        form.Show();
                    }
                   

                }

                else 
                {
                    count++;

                    if (count >= 5)
                    {
                        MessageBox.Show("您已连续输错密码5次，被迫退出！", "【警告】");

                        System.Threading.Thread.Sleep(1000);//等待1秒  

                        Application.Exit();
                    }

                    MessageBox.Show("用户名、密码、登录类型不匹配，请重试！", "【提示】");

                    this.textBox2.Text = "";
                    textBox2.Focus();

                }
            }
            catch (Exception error)
            {
                MessageBox.Show("" + error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
