using System;

using AppKit;
using Foundation;
using HttpClientSample.Core.Models;

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

            btnGet.Activated += async (sender, e) => {
				    var ser = new HttpGetService();
				    var buf = await ser.GetTextFromUrl(edtUrl.StringValue);

				    using (var alert =new NSAlert()){
				        alert.MessageText = buf;
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
