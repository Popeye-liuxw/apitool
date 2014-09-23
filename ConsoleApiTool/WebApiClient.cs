using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleApiTool
{
    public class HttpFun
    {

        public class Cfg
        {
            public string Url;
            //public System.Collections.Specialized.NameValueCollection data=new System.Collections.Specialized.NameValueCollection();
            public string data = "";
            public string ContentType;
            public string Accept = "";
            public string AcceptEncoding;
            public string Cookie = "";
            public string Header = "";
        }


        public string Post(Cfg cfg)
        {
            string method = "POST";
            return CoreRequest(cfg, method);
        }

        public string Put(Cfg cfg)
        {
            string method = "Put";
            return CoreRequest(cfg, method);
        }

        public string Delete(Cfg cfg)
        {
            string method = "Delete";
            return UWebRequest(cfg, method);
        }

        public string Get(Cfg cfg)
        {
            string method = "Get";
            return UWebRequest(cfg, method);
        }

        private string CoreRequest(Cfg cfg,string method)
        {
            var Result = "";
            if (string.IsNullOrEmpty(cfg.AcceptEncoding))
            {
                cfg.AcceptEncoding = "utf-8";// "GB2312";
            }
            if (string.IsNullOrEmpty(cfg.ContentType))
            {
                cfg.ContentType = "application/x-www-form-urlencoded";
            }
            if (string.IsNullOrEmpty(cfg.Accept))
            {
                cfg.Accept = "application/json";
            }
            try
            {
                var request = WebRequest.Create(cfg.Url);
                request.Method = method;  //  "POST";
                request.ContentType = cfg.ContentType + "; charset=utf-8";// 发送的内容  text/xml; charset=utf-8
                // Key	Value Cookie	ASP.NET_SessionId=pjsbqa55ypt0b2yom5nqaijz
                request.Headers.Add("Cookie", cfg.Cookie); //"ASP.NET_SessionId=" +
                //request.Headers.Add("accept", cfg.Accept + "; charset=utf-8");
                
                if (cfg.data.Length>0)
                {
                    //Encoding encoding = Encoding.GetEncoding("utf-8");
                    //byte[] data = encoding.GetBytes(cfg.data);
                    //request.ContentLength = data.Length;
                    //Stream requestStream = request.GetRequestStream();
                    //requestStream.Write(data, 0, data.Length);
                    //requestStream.Close();
                    using (var writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(cfg.data);
                    }
                }
                ((HttpWebRequest)request).Accept = cfg.Accept + "; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    System.IO.Stream receiveStream = response.GetResponseStream();
                    System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, System.Text.Encoding.GetEncoding(cfg.AcceptEncoding));
                    Result = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                }
                else
                {
                    Result = string.Format("{0} ({1})", (int)response.StatusCode, response.StatusDescription);
                }
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse)e.Response;
                if (response == null) return e.Message;
                return string.Format("{0} ({1})", (int)response.StatusCode, response.StatusDescription);
            }
            catch (Exception e)
            {
                Result = e.Message;
            }
            return Result;
        }

        public string CoreCliest(string strUrl, string data, string method)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(strUrl, method, data);
                    return HtmlResult;
                }
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse)e.Response;
                return string.Format("{0} ({1})", (int)response.StatusCode, response.StatusDescription);
               // return string.Format("{0} ({1})", (int)e.Status, e.Status.ToString());
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public string CoreCliest1(string strUrl, byte[] data, string method)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                   
                    wc.Headers["fileName"] = "1.png";
                   // wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    var r = wc.UploadData(strUrl, method, data).ToString();
                    return r;
                }
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse)e.Response;
                return string.Format("{0} ({1})", (int)response.StatusCode, response.StatusDescription);
                // return string.Format("{0} ({1})", (int)e.Status, e.Status.ToString());
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public string UWebRequest(Cfg cfg,string method)
        {
            try
            {
                var Result = "";
                if (string.IsNullOrEmpty(cfg.AcceptEncoding))
                {
                    cfg.AcceptEncoding = "utf-8";// "GB2312";
                }
                if (string.IsNullOrEmpty(cfg.ContentType))
                {
                    cfg.ContentType = "application/x-www-form-urlencoded";
                }
                if (string.IsNullOrEmpty(cfg.Accept))
                {
                    cfg.Accept = "application/json";
                }

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(cfg.Url);
                request.Headers.Add("Cookie",cfg.Cookie);
                request.Accept = cfg.Accept + "; charset=utf-8";
                // request.MaximumAutomaticRedirections = 4;
                // request.MaximumResponseHeadersLength = 4;
                request.ContentType = cfg.ContentType + "; charset=utf-8";

                //request.Timeout = (new TimeSpan(0, 0, 20)).Milliseconds;
                request.Method = method; 
                //request.Credentials = System.Net.CredentialCache.DefaultCredentials;
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    System.IO.Stream receiveStream = response.GetResponseStream();
                    System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, System.Text.Encoding.GetEncoding(cfg.AcceptEncoding));
                    Result = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                }
                else
                {
                    Result = string.Format("{0} ({1})", (int)response.StatusCode, response.StatusDescription);
                }
                return Result;
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse)e.Response;
                return string.Format("{0} ({1})", (int)response.StatusCode, response.StatusDescription);
  
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
