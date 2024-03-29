﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CollarChecker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        List<MyColor> colorList = new List<MyColor>();

        public MainWindow() {
            InitializeComponent();
            DataContext = GetColorList();
        }

        /// <summary>
        /// すべての色を取得するメソッド
        /// </summary>
        /// <returns></returns>
        private MyColor[] GetColorList() {
            return typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(i => new MyColor() { Color = (Color)i.GetValue(null), Name = i.Name }).ToArray();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            setColor();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            setColor(); //起動時に初期状態の設定値（R:0 G:0 B:0）から色を設定
        }

        //テキストボックスの値を元に色を設定
        private void setColor() {
            var r = byte.Parse(rValue.Text);
            var g = byte.Parse(gValue.Text);
            var b = byte.Parse(bValue.Text);
            Color color = Color.FromRgb(r, g, b);
            colorArea.Background = new SolidColorBrush(color);

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            rSlider.Value = ((MyColor)((ComboBox)sender).SelectedItem).Color.R;
            gSlider.Value = ((MyColor)((ComboBox)sender).SelectedItem).Color.G;
            bSlider.Value = ((MyColor)((ComboBox)sender).SelectedItem).Color.B;
            setColor();
        }

        //STOCKボタンクリック
        private void stockButton_Click(object sender, RoutedEventArgs e) {

            var stColor = getMyColor(byte.Parse(rValue.Text), byte.Parse(gValue.Text), byte.Parse(bValue.Text));

            stockList.Items.Insert(0, stColor.Name 
                            ?? "R：" + stColor.Color.R + "　G：" + stColor.Color.G + "　B：" + stColor.Color.B);
            colorList.Insert(0, stColor);
        }

        //DELETEボタンクリック
        private void deleteButton_Click(object sender, RoutedEventArgs e) {
            if (stockList.SelectedIndex == -1) return;

            colorList.RemoveAt(stockList.SelectedIndex);
            stockList.Items.RemoveAt(stockList.SelectedIndex);
        }

        //設定されている色情報のインスタンスを生成して返却
        private MyColor getMyColor(byte r, byte g, byte b) {
            
            return new MyColor
            {
                Color = Color.FromRgb(r, g, b),
                Name = ((IEnumerable<MyColor>)DataContext)
                                .Where(c => c.Color.R == r &&
                                            c.Color.G == g &&
                                            c.Color.B == b)
                                .Select(c => c.Name).FirstOrDefault(),
            };
        }

        private void stockList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (stockList.SelectedIndex == -1) return;

            rSlider.Value = colorList[stockList.SelectedIndex].Color.R;
            gSlider.Value = colorList[stockList.SelectedIndex].Color.G;
            bSlider.Value = colorList[stockList.SelectedIndex].Color.B;
            setColor();
        }

    }

    /// <summary>
    /// 色と色名を保持するクラス
    /// </summary>
    public class MyColor {
        public Color Color { get; set; }
        public string Name { get; set; }
    }
}
