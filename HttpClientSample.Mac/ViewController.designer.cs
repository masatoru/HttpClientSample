// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace HttpClientSample.Mac
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        AppKit.NSButton btnGet { get; set; }

        [Outlet]
        AppKit.NSButton btnWebRequest { get; set; }

        [Outlet]
        AppKit.NSTextField edtOutPath { get; set; }

        [Outlet]
        AppKit.NSTextField edtUrl { get; set; }

        [Action ("GetUrlText:")]
        partial void GetUrlText (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (btnGet != null) {
                btnGet.Dispose ();
                btnGet = null;
            }

            if (btnWebRequest != null) {
                btnWebRequest.Dispose ();
                btnWebRequest = null;
            }

            if (edtUrl != null) {
                edtUrl.Dispose ();
                edtUrl = null;
            }

            if (edtOutPath != null) {
                edtOutPath.Dispose ();
                edtOutPath = null;
            }
        }
    }
}
