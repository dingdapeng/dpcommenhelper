using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DpHttpHelper
{
    public static class DpHttp
    {   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ecode"></param>
        /// <returns></returns>
        public static string Get(string url,Encoding ecode)
        {
            string _StrResponse = "";
            HttpWebRequest _WebRequest = (HttpWebRequest)WebRequest.Create(url);
            _WebRequest.UserAgent = "MOZILLA/4.0 (COMPATIBLE; MSIE 7.0; WINDOWS NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
            _WebRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;//自动解压
            _WebRequest.Method = "GET";
            WebResponse _WebResponse = _WebRequest.GetResponse();
            StreamReader _ResponseStream = new StreamReader(_WebResponse.GetResponseStream(),ecode);
            _StrResponse = _ResponseStream.ReadToEnd();
            _WebResponse.Close();
            _ResponseStream.Close();
            return _StrResponse;
        }

        public static string Get(string url)
        {
            return Get(url,Encoding.UTF8);
        }

        public static string Post(string url, string param,Encoding ecode)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);


            Encoding encoding = ecode;

            byte[] bs = Encoding.ASCII.GetBytes(param);

            string responseData = String.Empty;

            req.Method = "POST";

            req.ContentType = "application/x-www-form-urlencoded";


            req.ContentLength = bs.Length;


            using (Stream reqStream = req.GetRequestStream())//发送post请求
            {


                reqStream.Write(bs, 0, bs.Length);


                reqStream.Close();


            }


            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {


                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {


                    responseData = reader.ReadToEnd().ToString();


                }


                return responseData;


            }


        }

        public static string Post(string url, string param)
        {
            return Post(url, param, Encoding.UTF8);
        }

        public static void DownLoadFile(string url, string path)
        {
            //保存文件
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream reader = response.GetResponseStream();
            FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buff = new byte[512];
            int c = 0; //实际读取的字节数
            while ((c = reader.Read(buff, 0, buff.Length)) > 0)
            {
                writer.Write(buff, 0, c);
            }
            writer.Close();
            writer.Dispose();
            reader.Close();
            reader.Dispose();
            response.Close();
           
        }
    }
}
