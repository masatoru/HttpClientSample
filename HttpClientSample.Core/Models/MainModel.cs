using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HttpClientSample.Shared.Models
{
    public class MainModel : BindableBase
    {
		private string dataDir;
		public string DataDir
		{
			get { return this.dataDir; }
			set { this.SetProperty(ref this.dataDir, value); }
		}

		private string url;
		public string Url
		{
			get { return this.url; }
			set { this.SetProperty(ref this.url, value); }
		}
    }

	public class BindableBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
		{
			if (Equals(field, value)) { return false; }
			field = value;
			var h = this.PropertyChanged;
			if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
			return true;
		}
	}
}
