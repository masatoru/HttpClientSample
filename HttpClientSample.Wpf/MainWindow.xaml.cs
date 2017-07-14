using HttpClientSample.Core.Models;
using HttpClientSample.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace HttpClientSample.Wpf
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnHttpClient_OnClick(object sender, RoutedEventArgs e)
        {
            var ser = new HttpGetService();
            var buf = await ser.GetTextFromUrl(edtUrl.Text);
            MessageBox.Show($"Content={buf}");
        }
        private void BtnWebRequest_OnClick(object sender, RoutedEventArgs e)
        {
            var ser = new HttpWebRequestService();
            ser.SaveFileFromUrl(edtUrl.Text,edtOutPath.Text);
            MessageBox.Show($"完了しました");
        }
    }
}
