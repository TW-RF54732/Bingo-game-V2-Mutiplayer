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
            public static int bingo_line = 0;
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
        private void button_Click(object sender, EventArgs e)//0-49號按鈕被案下
        {
            //((button)(sender)) = button陣列裡被按下的按鈕編號
            if (temp.playing == false) { input(Convert.ToInt32(((Button)sender).Name) + 1); }//填格子
            if (temp.playing == true)  { play (Convert.ToInt32(((Button)sender).Name) + 1); }//開始遊戲
        }
        int how_many_input = 0;
        private void input(int btn)//按鈕資料處理  按鈕從1開始
        {
            
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
                if (how_many_input >= 25) { Form2.data.whoReady[temp.id] = true; button2.Enabled = true; }//如果輸入25筆資料(填入完成)，增加whoready人數
            }
        }
        public void play(int btn)
        {
            Console.WriteLine(btn.ToString());
            int btnNum = Convert.ToInt32(btnArray[btn].Text) - 1;
            MessageBox.Show("你選擇填黑數字"+btnNum.ToString());
            Form2.data.fillblack[btnNum] = true;
            btnArray[btnNum + 24].BackColor = Color.Black;
            Bingo_detect();
            this.Visible = false;
        }
        public void display()
        {
            for(int k = 25;k<50;k++)
            {
                btnArray[k].Enabled = true;
            }
            for(int i = 0;i < 25;i++)
            {
                int j = 25;
                while (j <= 49){
                    if (Convert.ToInt32(btnArray[j].Text) == i) { break; }
                    j++;
                }
                if (Form2.data.fillblack[i] == true) { btnArray[j].BackColor = Color.Black; }
            }
        }
        public void Bingo_detect()
        {
            int line = 0;
            //橫向上到下
            if (btnArray[25].BackColor == Color.Black && btnArray[26].BackColor == Color.Black && btnArray[27].BackColor == Color.Black && btnArray[28].BackColor == Color.Black && btnArray[29].BackColor == Color.Black) { line++; }
            if (btnArray[30].BackColor == Color.Black && btnArray[31].BackColor == Color.Black && btnArray[32].BackColor == Color.Black && btnArray[28].BackColor == Color.Black && btnArray[29].BackColor == Color.Black) { line++; }
            if (btnArray[35].BackColor == Color.Black && btnArray[36].BackColor == Color.Black && btnArray[37].BackColor == Color.Black && btnArray[28].BackColor == Color.Black && btnArray[29].BackColor == Color.Black) { line++; }
            if (btnArray[40].BackColor == Color.Black && btnArray[41].BackColor == Color.Black && btnArray[42].BackColor == Color.Black && btnArray[28].BackColor == Color.Black && btnArray[29].BackColor == Color.Black) { line++; }
            if (btnArray[45].BackColor == Color.Black && btnArray[46].BackColor == Color.Black && btnArray[47].BackColor == Color.Black && btnArray[28].BackColor == Color.Black && btnArray[29].BackColor == Color.Black) { line++; }
            //豎向左到右
            if (btnArray[25].BackColor == Color.Black && btnArray[30].BackColor == Color.Black && btnArray[35].BackColor == Color.Black && btnArray[40].BackColor == Color.Black && btnArray[45].BackColor == Color.Black) { line++; }
            if (btnArray[26].BackColor == Color.Black && btnArray[31].BackColor == Color.Black && btnArray[36].BackColor == Color.Black && btnArray[41].BackColor == Color.Black && btnArray[46].BackColor == Color.Black) { line++; }
            if (btnArray[27].BackColor == Color.Black && btnArray[32].BackColor == Color.Black && btnArray[37].BackColor == Color.Black && btnArray[42].BackColor == Color.Black && btnArray[47].BackColor == Color.Black) { line++; }
            if (btnArray[28].BackColor == Color.Black && btnArray[33].BackColor == Color.Black && btnArray[38].BackColor == Color.Black && btnArray[43].BackColor == Color.Black && btnArray[48].BackColor == Color.Black) { line++; }
            if (btnArray[29].BackColor == Color.Black && btnArray[34].BackColor == Color.Black && btnArray[39].BackColor == Color.Black && btnArray[44].BackColor == Color.Black && btnArray[49].BackColor == Color.Black) { line++; }
            //斜線
            if (btnArray[25].BackColor == Color.Black && btnArray[31].BackColor == Color.Black && btnArray[37].BackColor == Color.Black && btnArray[43].BackColor == Color.Black && btnArray[49].BackColor == Color.Black) { line++; }
            if (btnArray[29].BackColor == Color.Black && btnArray[33].BackColor == Color.Black && btnArray[37].BackColor == Color.Black && btnArray[41].BackColor == Color.Black && btnArray[45].BackColor == Color.Black) { line++; }
            temp.bingo_line = line;
            label3.Text = line.ToString();
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
