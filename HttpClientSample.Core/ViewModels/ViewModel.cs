﻿using System;
using Reactive.Bindings;
using System.Reactive.Linq;
using System.Windows;
using HttpClientSample.Shared.Models;
using Reactive.Bindings.Extensions;
#if __WPF__
#endif
#if __MAC__
using AppKit;
#endif

namespace HttpClientSample.Shared.ViewModels
{
    public class ViewModel
    {
		public ReactiveProperty<string> Url { get; }
		public ReactiveProperty<string> DataDir { get; }
		public ReactiveCommand ExportFileCommand { get; }
        MainModel Model { get; } = new MainModel();

        public ViewModel()
        {
            Url = this.Model
            	.ObserveProperty(x => x.Url)
            	.ToReactiveProperty();
            DataDir = this.Model
                .ObserveProperty(x => x.DataDir)
                .ToReactiveProperty();

            //Url = this.Model
            //          .ToReactivePropertyAsSynchronized(x => x.Url);
            //DataDir = this.Model
                          //.ToReactivePropertyAsSynchronized(x => x.DataDir);

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
            var savePath = System.IO.Path.Combine(DataDir.Value, "yahoo.htm");
            ser.SaveFileFromUrl(Url.Value, savePath);
            var msg = $"完了しました\n{savePath}";
#if __WPF__
            MessageBox.Show(msg);
#endif
#if __MAC__
            using (var alert = new NSAlert())
            {
                alert.MessageText = msg;
                alert.RunSheetModal(null);
            }
#endif
        }
    }
}
