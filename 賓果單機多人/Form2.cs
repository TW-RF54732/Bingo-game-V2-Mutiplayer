﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 賓果單機多人
{
    public partial class Form2 : Form
    {
        private 玩家介面[] playerform;//宣告playerform是玩家介面
        private Label[] labelArray;
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
        public static class data
        {
            public static int player_amount = 0;//從1開始//遊玩人數
            public static int[,] input;
            public static bool[] whoReady;
            public static bool[] fillblack = new bool[26];//從0開始//紀錄被填黑的數字
        }
        public Form2(int playerCount)//接收form1人數//主要程式
        {
            /*此處主程式執行有form1回傳直playerCount =(玩家總數/data.player_amount)
             各個子程式在此處被呼叫
             更直觀的了解整個流程不需被內部瑣碎程式干擾
             */
            InitializeComponent();
            this.Show();//顯示資料庫介面
            data.player_amount = playerCount;//儲存人數到名為data(數據)的類別之player_amount(玩家總數)的靜態成員 
            data.input = new int[data.player_amount, 27];//在知道玩家總數後創建每個玩家的數字處存陣列
            data.whoReady = new bool[data.player_amount];//用於確認玩家是否完成填入數字
            info_creat();//生成動態物件 例:label !!!內有宣告新增實體
                         //勿重複執行，且優先執行於info系列動作
            playerform_creat();//生成遊戲介面(隱形) + 輸入模式
            MessageBox.Show("填完格子後可以準備開始遊戲，由玩家1先選擇你要填黑的格子");
            玩家介面.temp.playing = true;//告訴玩家介面遊戲開始
            startPlay();//開始遊玩


            info_show();//呼叫顯示模組
        }
        private void startPlay()
        {
            bool win = false;
            int j = 0;
            while (win == false)
            {
                for (int i = 0; i < data.player_amount; i++)
                {
                    info_show();
                    playerform[i].display();
                    playerform[i].ShowDialog();
                    if (玩家介面.temp.bingo_line >= 3)
                    {
                        win = true;
                        MessageBox.Show($"玩家{i + 1}贏了這場遊戲");
                        break;
                    }
                }
                j++;
            }
        }
        
        private void playerform_creat()
        {
            playerform = new 玩家介面[data.player_amount];//生成等同玩家數量個的遊戲介面
            for (int i = 0; i < playerform.Length; i++)
            {
                data.input[i, 0] = i + 1;//陣列的編號
                playerform[i] = new 玩家介面(i);
                playerform[i].Text = $"玩家 {i + 1}"; // 設定每個 Form 的標題
                info_show();
                playerform[i].ShowDialog(this);
                if(i + 2 <= data.player_amount)MessageBox.Show($"輪到玩家{i + 2}\n 關閉此通知打開下一個遊戲視窗");
            }
            bool allReady = false;
            while(allReady == false)
            {
                allReady = true;
                for (int i = 0; i < data.player_amount; i++)
                {
                    if (data.whoReady[i] == false)
                    {
                        MessageBox.Show($"玩家{i+1}尚未完成填滿空格\n請完成表格以繼續遊戲");
                        playerform[i].ShowDialog();
                        allReady = false;
                    }
                }
            }


        }//介面生成
        public void info_creat()//控制form2資料庫顯示
        {
            label2.Text = $"{data.player_amount}";//人數
            label3.Text = Convert.ToString(data.input.GetUpperBound (0)+1);//顯示長
            label6.Text = Convert.ToString(data.input.GetUpperBound (1)+1);//顯示高
            labelArray = new Label[data.player_amount];//創建用來顯示是否完填數字成的label
            for(int i = 0;i < labelArray.Length; i++)//生成label
            {
                labelArray[i] = new Label();
                labelArray[i].Size = new Size(48, 24);
                labelArray[i].Text = "玩家" + (i+1);
                labelArray[i].Font = new Font("微軟正黑體", 9, FontStyle.Regular);
                labelArray[i].Location = new Point(110 + i *45, 64);
                labelArray[i].Visible = true;
                labelArray[i].ForeColor = data.whoReady[i] == true ? Color.LimeGreen : Color.Red;
                this.Controls.Add(labelArray[i]);
            }//同上
            listBox1.Items.Clear();
            for(int i = 0; i < data.player_amount; i++)//第i行
            {
                
                string show = "";
                for(int j = 0; j < 26; j++)//地j個
                {
                    show += ",";
                    show += Convert.ToString(data.input[i, j]);
                }
                listBox1.Items.Add(show);

            }//listbox1顯示
            int how_maney_ready = 0;
            for(int i = 0;i < data.player_amount; i++)
            {
                if (data.whoReady[i] == true) {  how_maney_ready++; }
            }
            label8.Text = "數字填充完成人數:" + how_maney_ready;
            
        }
        public void info_show()
        {
            label2.Text = $"{data.player_amount}";//人數
            label3.Text = Convert.ToString(data.input.GetUpperBound(0) + 1);//顯示長
            label6.Text = Convert.ToString(data.input.GetUpperBound(1) + 1);//顯示高
            for (int i = 0; i < labelArray.Length; i++)//label顏色
            {
                labelArray[i].ForeColor = data.whoReady[i] == true ? Color.LimeGreen : Color.Red;

            }//label資料更新
            listBox1.Items.Clear();
            for (int i = 0; i < data.player_amount; i++)//第i行
            {

                string show = "";
                for (int j = 0; j < 25; j++)//地j個
                {
                    if (j != 0) { show += ","; }
                    show += Convert.ToString(data.input[i, j]);
                }
                listBox1.Items.Add(show);

            }//listbox1顯示
            int how_maney_ready = 0;
            for (int i = 0; i < data.player_amount; i++)
            {
                if (data.whoReady[i] == true) { how_maney_ready++; }
            }//完成填數字
            label8.Text = "數字填充完成人數:" + how_maney_ready;
            label11.Text = "填黑:";
            for (int i = 0;i < 26; i++)
            {                
                if (data.fillblack[i] == true) { label11.Text += (i).ToString() + ","; }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            info_show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button[] btnArray = new Button[25];
            int k = 0;
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    btnArray[k] = new Button();
                    btnArray[k].Name = $"{k}";
                    btnArray[k].Size = new Size(40, 40);
                    btnArray[k].Font = new Font("微軟正黑體", 12, FontStyle.Regular);
                    btnArray[k].Location = new Point(300 + x * 40, 50 + y * 40);
                    btnArray[k].Visible = true;
                    for (int i = 0; i < 25; i++)
                    {
                        btnArray[k].Text = $"{data.input[listBox1.SelectedIndex, i]}";
                        if (data.fillblack[k]) { 
                            btnArray[k].ForeColor = Color.Black; 
                        }
                    }
                    this.Controls.Add(btnArray[k]);
                    k++;
                }
            }
        }
    }
}
