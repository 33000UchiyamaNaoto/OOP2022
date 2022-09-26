using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace CarReportSystem
{
    public partial class Form1 : Form
    {
        //設定情報保存用オブジェクト
        Settings settings = new Settings();

        //カーレポート管理用リスト
        //BindingList<CarReport> listCarReports = new BindingList<CarReport>();

        int mode = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            //アプリケーションの終了
            Application.Exit();
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //色設定ダイアログの表示
            if (cdColorSelect.ShowDialog() == DialogResult.OK)
            {
                BackColor = cdColorSelect.Color;
                settings.MainFormColor = cdColorSelect.Color.ToArgb();   //設定オブジェクトへセット
            }
        }

        private void pbModeSelect_Click(object sender, EventArgs e)
        {
            pbPicture.SizeMode = (PictureBoxSizeMode)mode;
            mode = mode < 4 ? ++mode : 0;

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            //設定ファイルをシリアル化（P305）
            using (var writer = XmlWriter.Create("settings.xml"))
            {
                var serializer = new XmlSerializer(settings.GetType());
                serializer.Serialize(writer, settings);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: このコード行はデータを 'infosys202200DataSet.CarReportDB' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.carReportDBTableAdapter.Fill(this.infosys202200DataSet.CarReportDB);
            try
            {
                //設定ファイルを逆シリアル化（P307）して背景の色を設定
                using (var reader = XmlReader.Create("settings.xml"))
                {
                    var serializer = new XmlSerializer(typeof(Settings));
                    settings = serializer.Deserialize(reader) as Settings;
                    BackColor = Color.FromArgb(settings.MainFormColor);
                }
            }
            catch (Exception)
            {

            }

            EnabledCheck(); //マスク処理呼び出し
        }

        private void btAddReport_Click(object sender, EventArgs e)
        {
            //氏名が未入力なら登録しない
            if (String.IsNullOrWhiteSpace(cbAuther.Text))
            {
                MessageBox.Show("氏名が入力されていません");
                return;
            }

            //CarReport carReport = new CarReport
            //{
            //    Date = dtpDate.Value,
            //    Auther = cbAuther.Text,
            //    Maker = GetRadioButtonMaker(),
            //    CarName = cbCarName.Text,
            //    Report = tbReport.Text,
            //    Picture = pbPicture.Image,
            //};
            //listCarReports.Add(carReport);

            //EnabledCheck(); //マスク処理呼び出し
            setCbAuther(cbAuther.Text);
            setCbCarName(cbCarName.Text);

        }
        //設定されているメーカーを返す
        private string GetRadioButtonMaker()
        {
            if (rbToyota.Checked)
            {
                return "トヨタ";
            }
            if (rbNissan.Checked)
            {
                return "日産";
            }
            if (rbHonda.Checked)
            {
                return "ホンダ";
            }
            if (rbSubaru.Checked)
            {
                return "スバル";
            }
            if (rbImport.Checked)
            {
                return "外国車";
            }

            return "その他";
        }

        private void btModifyReport_Click(object sender, EventArgs e)
        {
            carReportDBDataGridView.CurrentRow.Cells[3].Value = GetRadioButtonMaker();

        }

        private void btDeleteReport_Click(object sender, EventArgs e)
        {
            //listCarReports.RemoveAt(dgvCarReport.CurrentRow.Index);
            EnabledCheck(); //マスク処理呼び出し

        }

        private void EnabledCheck()
        {
            //btModifyReport.Enabled = btDeleteReport.Enabled = listCarReports.Count() > 0 ? true : false;

        }
        //コンボボックスに記録者を登録する（重複なし）
        private void setCbAuther(string company)
        {
            if (!cbAuther.Items.Contains(company))
            {
                //まだ登録されていなければ登録処理
                cbAuther.Items.Add(company);
            }
        }
        //コンボボックスに車名を登録する（重複なし）
        private void setCbCarName(string company)
        {
            if (!cbCarName.Items.Contains(company))
            {
                //まだ登録されていなければ登録処理
                cbCarName.Items.Add(company);
            }
        }

        private void btPictureOpen_Click(object sender, EventArgs e)
        {
            if (ofdCarReportOpen.ShowDialog() == DialogResult.OK)
            {
                pbPicture.Image = Image.FromFile(ofdCarReportOpen.FileName);
            }
        }

        private void btPictureDelete_Click(object sender, EventArgs e)
        {
            pbPicture.Image = null;
        }

        private void btOpenReport_Click(object sender, EventArgs e)
        {

        }

        private void btSaveReport_Click(object sender, EventArgs e)
        {
        
        }

        private void carReportDBBindingNavigatorSaveItem_Click(object sender, EventArgs e) {
            this.Validate();
            this.carReportDBBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.infosys202200DataSet);

        }
        //データグリッドビューのクリックイベント
        private void carReportDBDataGridView_Click(object sender, EventArgs e) {

            setMakerRadioSet(carReportDBDataGridView.CurrentRow.Cells[3].Value.ToString());
            
        }

        private void setMakerRadioSet(string maker) {
            switch (maker)
            {
                case "トヨタ":
                    rbToyota.Checked = true;
                    break;

                case "日産":
                    rbNissan.Checked = true;
                    break;



            }

        }
    }
}
