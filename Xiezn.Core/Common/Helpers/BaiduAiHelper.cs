using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xiezn.Core.Models.ViewModel;

namespace Xiezn.Core.Common.Helpers
{
    public static class BaiduAiHelper
    {
        public static String TOKEN = "";

        public static String clientId = "";
        public static String clientSecret = "";

        public static String APP_ID = "49214550";
        public static String API_KEY = "7Otjpv2kn0ljQk45qXOXh5MO";
        public static String SECRET_KEY = "BMfbXRbTIVaB4C3SbRTtGqDv1wHDvyXS";

        /// <summary>
        /// 获取access_token
        /// </summary>
        public static void GetAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            AccessTokenViewModel resObj = JsonConvert.DeserializeObject<AccessTokenViewModel>(result);

            TOKEN = resObj.access_token;
        }

        /// <summary>
        /// 人脸对比
        /// </summary>
        /// <param name="imageInfo">对比的图片相关信息</param>
        /// <returns></returns>
        public static string FaceMatch(string imageInfo)
        {
            string host = "https://aip.baidubce.com/rest/2.0/face/v3/match?access_token=" + TOKEN;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;
            byte[] buffer = encoding.GetBytes(imageInfo);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();

            return result;
        }

        /// <summary>
        /// 文字识别
        /// </summary>
        /// <param name="imageInfo">图片路径</param>
        /// <returns></returns>
        public static string StringGeneral(string imageInfo)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 60000;

            var image = System.IO.File.ReadAllBytes(imageInfo);
            var result = client.GeneralBasic(image);

            return result["words_result"][0]["words"].ToString();
        }

        /// 汽车识别
        /// </summary>
        /// <param name="imageInfo">图片路径</param>
        /// <returns></returns>
        public static object CarDetect(string imageInfo)
        {
            var client = new Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY);
            client.Timeout = 60000;

            var image = System.IO.File.ReadAllBytes(imageInfo);
            var options = new Dictionary<string, object>{
                {"baike_num", 1}
            };
            var result = client.CarDetect(image, options);

            return result["result"][0];
        }

        ///  通识识别
        /// </summary>
        /// <param name="imageInfo">图片路径</param>
        /// <returns></returns>
        public static object AdvancedGeneral(string imageInfo)
        {
            var client = new Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY);
            client.Timeout = 60000;

            var image = System.IO.File.ReadAllBytes(imageInfo);
            var options = new Dictionary<string, object>{
                {"baike_num", 1}
            };
            var result = client.AdvancedGeneral(image, options);

            return result["result"][0];
        }

        /// <summary>
        /// 菜品识别
        /// </summary>
        /// <param name="imageInfo">图片路径</param>
        /// <returns></returns>
        public static object DishDetect(string imageInfo)
        {
            var client = new Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY);
            client.Timeout = 60000;

            var image = System.IO.File.ReadAllBytes(imageInfo);
            var options = new Dictionary<string, object>{
                {"baike_num", 1}
            };
            var result = client.DishDetect(image, options);

            return result["result"][0];
        }

        /// <summary>
        /// 动物识别
        /// </summary>
        /// <param name="imageInfo">图片路径</param>
        /// <returns></returns>
        public static object AnimalDetect(string imageInfo)
        {
            var client = new Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY);
            client.Timeout = 60000;

            var image = System.IO.File.ReadAllBytes(imageInfo);
            var options = new Dictionary<string, object>{
                {"baike_num", 1}
            };
            var result = client.AnimalDetect(image, options);

            return result["result"][0];
        }

        /// <summary>
        /// 植物识别
        /// </summary>
        /// <param name="imageInfo">图片路径</param>
        /// <returns></returns>
        public static object PlantDetect(string imageInfo)
        {
            var client = new Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY);
            client.Timeout = 60000;

            var image = System.IO.File.ReadAllBytes(imageInfo);
            var options = new Dictionary<string, object>{
                {"baike_num", 1}
            };
            var result = client.PlantDetect(image, options);

            return result["result"][0];
        }

        public static AddressViewModel GetAddress(string ak, double lng, double lat)
        {
            string url = @"http://api.map.baidu.com/geocoder/v2/?ak=" + ak + "&callback=renderReverse&location=" + lat + "," + lng + @"&output=xml&pois=1";
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            XmlDocument xmlDoc = new XmlDocument();
            string sendData = xmlDoc.InnerXml;
            byte[] byteArray = Encoding.Default.GetBytes(sendData);

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, System.Text.Encoding.GetEncoding("utf-8"));
            string responseXml = reader.ReadToEnd();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(responseXml);
            string status = xml.DocumentElement.SelectSingleNode("status").InnerText;
            if (status == "0")
            {
                AddressViewModel addressViewModel = new AddressViewModel();
                XmlNodeList formatted_address = xml.DocumentElement.GetElementsByTagName("formatted_address");
                XmlNodeList province = xml.DocumentElement.GetElementsByTagName("province");
                XmlNodeList city = xml.DocumentElement.GetElementsByTagName("city");
                XmlNodeList district = xml.DocumentElement.GetElementsByTagName("district");
                XmlNodeList street = xml.DocumentElement.GetElementsByTagName("street");
                if (formatted_address.Count > 0)
                {
                    //return formatted_address[0].InnerText;
                }
                if (province.Count > 0)
                {
                    addressViewModel.Province = province[0].InnerText;
                }
                if (city.Count > 0)
                {
                    addressViewModel.City = city[0].InnerText;
                }
                if (district.Count > 0)
                {
                    addressViewModel.District = district[0].InnerText;
                }
                if (street.Count > 0)
                {
                    addressViewModel.Street = street[0].InnerText;
                }

                return addressViewModel;
            }

            return null;
        }
    }
}
