using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample.Core.Models
{
    public class HttpGetService
    {
        HttpClient _client=new HttpClient();
        public async Task<string> GetTextFromUrl(string url)
        {
            return await _client.GetStringAsync(url);
        }
    }
}