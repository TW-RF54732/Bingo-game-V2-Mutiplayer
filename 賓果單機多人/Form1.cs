using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 賓果單機多人
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "輸入遊玩人數";
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "0"||textBox1.Text == "1")
                {
                    MessageBox.Show("請輸入正確人數(>1)");
                    textBox1.Text = "輸入遊玩人數";
                }
                else
                {
                    int player = Convert.ToInt32(textBox1.Text);
                    this.Visible = false;//隱藏視窗
                    Form2 f2 = new Form2(player);
                    f2.Show();
                }
                
            }
            catch
            {
                MessageBox.Show("請輸入正確人數(>1)");
                textBox1.Text = "輸入遊玩人數";

            }
        }
    }
}
