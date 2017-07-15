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
            edtUrl.Changed += (o, e) => vm.Url.Value = edtUrl.StringValue;
            edtOutPath.Changed += (o, e) => vm.DataDir.Value = edtOutPath.StringValue;
            btnWebRequest.Enabled = vm.ExportFileCommand.CanExecute();

            // 条件を満たさない限りボタンを押せないようにする
            vm.ExportFileCommand.CanExecuteChanged += (sender, e)
                => { btnWebRequest.Enabled = vm.ExportFileCommand.CanExecute(); };

			// HttpWebRequestを使ってURLをファイルに出力する
			btnWebRequest.Activated += (o, e) => vm.ExportFileCommand.Execute();

			// UIの初期値
			edtOutPath.StringValue = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			edtUrl.StringValue = "https://yahoo.co.jp";

            // TWOWAYでないので以下はだめ(調査中)
			vm.DataDir.Value = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			vm.Url.Value = "https://yahoo.co.jp";
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
