using System;
using AppKit;
using Foundation;
using HttpClientSample.Shared.ViewModels;

namespace HttpClientSample.Mac
{
    public partial class ViewController : NSViewController
    {
        ViewModel vm=new ViewModel();
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();



			// HttpClientを使ってURLを取得する
			//btnGet.Activated += async (sender, e) => {
			//var ser = new HttpGetService();
			//var buf = await ser.GetTextFromUrl(edtUrl.StringValue);

			//using (var alert =new NSAlert()){
			//    alert.MessageText = buf;
			//    alert.RunSheetModal(null);
			//}
			//};

			// ViewModelとBindさせる

            // 多分この方法がNG
			// http://monobook.org/wiki/ReactiveUI/Xamarin.Mac%E3%81%AENSTextField%E3%81%AB%E3%83%90%E3%82%A4%E3%83%B3%E3%83%89%E3%81%99%E3%82%8B
			edtUrl.Changed += (o, e) => vm.Url.Value = edtUrl.StringValue;
            edtOutPath.Changed += (o, e) => vm.DataDir.Value = edtOutPath.StringValue;
            //btnWebRequest.Enabled = vm.ExportFileCommand.CanExecute();

            // 条件を満たさない限りボタンを押せないようにする
            vm.ExportFileCommand.CanExecuteChanged += (sender, e)
                => { btnWebRequest.Enabled = vm.ExportFileCommand.CanExecute(); };

			// HttpWebRequestを使ってURLをファイルに出力する
			btnWebRequest.Activated += (o, e) => vm.ExportFileCommand.Execute();

			// 1.UIの初期値 初回がだめ
			edtOutPath.StringValue = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			edtUrl.StringValue = "https://yahoo.co.jp";

            // 2.値は入ってるのでCanExecuteは有効だけどUIが更新されない
			//vm.DataDir.Value = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			//vm.Url.Value = "https://yahoo.co.jp";
		}

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
