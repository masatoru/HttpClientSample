using System;
using Reactive.Bindings;
using System.Reactive.Linq;
using HttpClientSample.Shared.Models;
using AppKit;

namespace HttpClientSample.Shared.ViewModels
{
    public class ViewModel
    {
		public ReactiveProperty<string> Url { get; } = new ReactiveProperty<string>();
		public ReactiveProperty<string> DataDir { get; } = new ReactiveProperty<string>();
		public ReactiveCommand ExportFileCommand { get; }

        public ViewModel()
        {
            // URLに文字列があるかつディレクトリがあるなら実行可能
            ExportFileCommand = DataDir
                .CombineLatest(Url, (dataDir,url)=> !string.IsNullOrEmpty(url)
                               && !string.IsNullOrEmpty(dataDir)
                               && System.IO.Directory.Exists(dataDir))
                .ToReactiveCommand();

			ExportFileCommand
                .Subscribe(() => SaveFileFromUrl());
        }

        // URLの内容をファイルに保存する
        void SaveFileFromUrl()
        {
            var ser = new HttpWebRequestService();
            ser.SaveFileFromUrl(Url.Value, DataDir.Value);
            using (var alert = new NSAlert())
            {
                alert.MessageText = "完了しました";
                alert.RunSheetModal(null);
            }
		}
    }
}
