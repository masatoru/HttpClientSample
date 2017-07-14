using System;

using AppKit;
using Foundation;
using HttpClientSample.Core.Models;
using HttpClientSample.Shared.Models;

namespace HttpClientSample.Mac
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            edtUrl.StringValue = "https://yahoo.co.jp";

            var desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            edtOutPath.StringValue = System.IO.Path.Combine(desktopDir,"yahoo.htm");

            // HttpClientを使ってURLを取得する
            btnGet.Activated += async (sender, e) => {
				    var ser = new HttpGetService();
				    var buf = await ser.GetTextFromUrl(edtUrl.StringValue);

				    using (var alert =new NSAlert()){
				        alert.MessageText = buf;
				        alert.RunSheetModal(null);
				    }
			};

            // HttpWebRequestを使ってURLをファイルに出力する
            btnWebRequest.Activated += (sender, e) => {
                var ser = new HttpWebRequestService();
				ser.SaveFileFromUrl(edtUrl.StringValue, edtOutPath.StringValue);

				using (var alert = new NSAlert())
				{
					alert.MessageText = "完了しました";
					alert.RunSheetModal(null);
				}  
            };
        }

        //partial void GetUrlText(NSObject sender)
        //{
        //    var ser = new HttpGetService();
        //    var buf = ser.GetTextFromUrl(edtUrl.StringValue).Result;

        //    using (var alert =new NSAlert()){
        //        alert.MessageText = buf;
        //        alert.RunSheetModal(null);
        //    }
        //}

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
