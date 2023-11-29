using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 賓果單機多人
{
    public partial class 玩家介面 : Form
    {
        private Button[] btnArray = new Button[50];
        private Form2 f2 = new Form2();
        public 玩家介面(int id)
        {
            InitializeComponent();
            label2.Text = id.ToString();
            makebtn();//生成按鈕0-49
            label2.Visible = false;
            f2.info_creat();
            f2.info_show();
            f2.Show();
        }
        public void f2Close()
        {
            f2.Visible = false;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            
        }
        public class temp
        {
            public static int leftbtn = 0;
            public static int id = 0;
            public static bool playing = false;
        }
        private void makebtn()
        {
            int i = 0;
            for(int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    btnArray[i] = new Button();
                    btnArray[i].Name = $"{i}";
                    btnArray[i].Size = new Size(40, 40);
                    btnArray[i].Font = new Font("微軟正黑體", 12, FontStyle.Regular);
                    btnArray[i].Location = new Point(300 + x * 40, 50 + y * 40);
                    btnArray[i].Visible = true;
                    btnArray[i].Text = $"{i + 1}";
                    this.Controls.Add(btnArray[i]);
                    btnArray[i].Click += new EventHandler(button_Click);
                    
                    //if (Form2.data.input[Convert.ToInt32(label2.Text), i+ 1] != 0)
                    //{
                    //    btnArray[i + 25].Text = Convert.ToString(Form2.data.input[Convert.ToInt32(label2.Text), i + 1]);
                    //    btnArray[i].Visible = false;
                    //}
                    i++;
                }
            }
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    btnArray[i] = new Button();
                    btnArray[i].Name = $"{i}";
                    btnArray[i].Size = new Size(40, 40);
                    btnArray[i].Font = new Font("微軟正黑體", 12, FontStyle.Regular);
                    btnArray[i].Location = new Point(40 + x * 40, 50 + y * 40);
                    btnArray[i].Visible = true;
                    btnArray[i].Text = "";
                    this.Controls.Add(btnArray[i]);
                    btnArray[i].Click += new EventHandler(button_Click);
                    i++;
                    
                }
            }

        }
        private void button_Click(object sender, EventArgs e)
        {
            //((button)(sender)) = button陣列裡被按下的按鈕編號
            if (temp.playing == false) { input(Convert.ToInt32(((Button)sender).Name) + 1); }//填格子
            if (temp.playing == true)  { play (Convert.ToInt32(((Button)sender).Name) + 1); }//開始遊戲
        }
        private void input(int btn)//按鈕資料處理  按鈕從1開始
        {
            int how_many_input = 0;
            temp.id = Convert.ToInt32(label2.Text);
            if (btn < 26) { temp.leftbtn = btn; }//leftbtn 從1開始
            if (temp.leftbtn == 0) { label1.Text = "選取:無"; }
            else label1.Text = "選取:" + $"{temp.leftbtn}";
            if (btn > 25 && temp.leftbtn > 0)//回傳直到form2input資料陣列
            {
                btnArray[btn - 1].Text = temp.leftbtn.ToString();
                btnArray[temp.leftbtn - 1].Visible = false;
                Form2.data.input[temp.id, btn - 25] = temp.leftbtn;//第2格開始填入資料
                temp.leftbtn = 0; label1.Text = "選取:無";//選取顯示
                btnArray[btn - 1].Enabled = false;
                how_many_input++;//計算輸入資料幾筆
                if (how_many_input == 25) { Form2.data.whoReady[temp.id] = true ; }//如果輸入25筆資料(填入完成)，增加whoready人數
            }
        }
        public void play(int btn)
        {
            
            int btnNum = Convert.ToInt32(btnArray[btn].Text) - 1;
            MessageBox.Show(btnNum.ToString());
            Form2.data.fillblack[btnNum] = true;
            btnArray[btnNum + 24].BackColor = Color.Black;
            this.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)//auto fill
        {
            int how_many_input = 0;
            temp.id = Convert.ToInt32(label2.Text);
            for (int i = 25;i<50;i++)
            {
                btnArray[i].Text = (i - 24).ToString();
                btnArray[i - 25].Visible = false;
                Form2.data.input[temp.id, i - 24] = i - 24;
                btnArray[i].Enabled = false;
                how_many_input++;
                if (how_many_input >= 25) { Form2.data.whoReady[temp.id] = true; button2.Enabled = true; }
            }
        }

        private void button2_Click(object sender, EventArgs e)//完成
        {
            f2Close();
            temp.id = Convert.ToInt32(label2.Text) ;
            this.Visible = false;
        }

    }
}
