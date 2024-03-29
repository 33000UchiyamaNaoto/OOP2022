﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumberGame
{
    public partial class Form1 : Form
    {
        private Random rand = new Random(); //乱数オブジェクト生成

        //乱数保存用
        private int randomNumber;

        public Form1()
        {
            InitializeComponent();
        }
        //アプリケーション起動時に一度だけ呼ばれるイベントハンドラ
        private void Form1_Load(object sender, EventArgs e)
        {
            getRandom(); //乱数取得
        }
        //判定ボタンイベントハンドラ
        private void Judge_Click(object sender, EventArgs e)
        {
            //入力した値とあらかじめ取得した乱数を比較し結果を表示
            if(randomNumber == Number.Value)
            {
                toolStripStatusLabel1.Text = "正解！";
            }
            else if(randomNumber > Number.Value)
            {
                toolStripStatusLabel1.Text = "入力した値より大きいです！";
            }
            else if (randomNumber < Number.Value)
            {
                toolStripStatusLabel1.Text = "入力した値より小さいです！";
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            getRandom(); //乱数取得
        }

        //乱数取得
        private void getRandom()
        {            
            randomNumber = rand.Next(1, (int)maxNum.Value);
            //this.Text = randomNumber.ToString();
        }
    }
}
