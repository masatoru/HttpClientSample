using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample.Shared.Models
{
    public class HttpWebRequestService
    {
        public static void SaveFileFromUrl(string url, string outpath)
        {
            //If-Rangeヘッダに渡すエンティティタグを指定するときは指定する
            string entityTag = "";

            //WebRequestの作成
            System.Net.HttpWebRequest webreq =
                (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

            //ファイルがあればダウンロードが途中であると判断し、
            //ファイルサイズを取得する
            long firstPos;
            if (System.IO.File.Exists(outpath))
                firstPos = (new System.IO.FileInfo(outpath)).Length;
            else
                firstPos = 0;
            if (firstPos > 0)
            {
                //バイトレンジを指定する
                webreq.AddRange((int)firstPos);
                //If-Rangeヘッダを追加
                if (entityTag != "")
                    webreq.Headers.Add("If-Range", entityTag);
            }
            //そのほかのヘッダを指定する
            webreq.KeepAlive = false;
            webreq.Headers.Add("Pragma", "no-cache");
            webreq.Headers.Add("Cache-Control", "no-cache");

            System.Net.HttpWebResponse webres = null;
            try
            {
                //サーバーからの応答を受信するためのWebResponseを取得
                webres = (System.Net.HttpWebResponse)webreq.GetResponse();
            }
            catch (System.Net.WebException e)
            {
                //HTTPプロトコルエラーを捕捉し、内容を表示する
                if (e.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    System.Net.HttpWebResponse errres =
                        (System.Net.HttpWebResponse)e.Response;
                    Console.WriteLine(errres.StatusCode);
                    Console.WriteLine(errres.StatusDescription);
                }
                else
                    Console.WriteLine(e.Message);

                return;
            }

            //エンティティタグの表示
            Console.WriteLine("ETag:" + webres.GetResponseHeader("ETag"));

            //応答データを受信するためのStreamを取得
            System.IO.Stream strm = webres.GetResponseStream();

            //ファイルに書き込むためのFileStreamを作成
            System.IO.FileStream fs = new System.IO.FileStream(outpath,
                System.IO.FileMode.OpenOrCreate,
                System.IO.FileAccess.Write);

            //ファイルに書き込む位置を決定する
            //206Partial Contentステータスコードが返された時はContent-Rangeヘッダを調べる
            //それ以外のときは、先頭から書き込む
            long seekPos = 0;
            if (webres.StatusCode == System.Net.HttpStatusCode.PartialContent)
            {
                string contentRange = webres.GetResponseHeader("Content-Range");
                System.Text.RegularExpressions.Match m =
                    System.Text.RegularExpressions.Regex.Match(
                        contentRange,
                        @"bytes\s+(?:(?<first>\d*)-(?<last>\d*)|\*)/(?:(?<len>\d+)|\*)");
                if (m.Groups["first"].Value == "")
                    seekPos = 0;
                else
                    seekPos = int.Parse(m.Groups["first"].Value);
            }
            //書き込み位置を変更する
            fs.SetLength(seekPos);
            fs.Position = seekPos;

            //応答データをファイルに書き込む
            byte[] readData = new byte[1024];
            int readSize = 0;
            for (;;)
            {
                readSize = strm.Read(readData, 0, readData.Length);
                if (readSize == 0)
                    break;
                fs.Write(readData, 0, readSize);
            }

            //閉じる
            fs.Close();
            strm.Close();
        }
    }
}
