using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample0607
{
    public partial class Form1 : Form
    {
        Random rand = new Random(); //乱数オブジェクト生成

        //コンストラクタ
        public Form1()
        {
            InitializeComponent();
        }
        //ボタンクリックイベントハンドラ
        private void btRandom_Click(object sender, EventArgs e)
        {
            Number.Value = rand.Next(minValue: (int)Min.Value, maxValue: (int)Max.Value + 1);

        }
        //イベントハンドラ（起動時に一度だけ実行）
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
