using System;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Inha.Commons.Log.Utils
{
    public static class LogUtilExtensions
    {
        /// <summary>
        ///     SerializeObject to json by  Newtonsoft.Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                return JsonConvert.SerializeObject(obj, new HttpPostedFileConverter());
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///     Tìm transactionId trong tham số truyền vào. Mặc định tham số cuối cùng là dãy data bao gồm transactionId,
        ///     siteId,...
        ///     TransactionId có dạng ::transactionId=xxxxxx::
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDefaultTransactionId(this object[] obj)
        {
            return obj.GetDefaultValueByKey("transactionId");
        }

        /// <summary>
        ///     Tìm siteId trong tham số truyền vào. Mặc định tham số cuối cùng là dãy data bao gồm transactionId, siteId,...
        ///     SiteId có dạng ::siteId=xxxxxx::
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDefaultSiteId(this object[] obj)
        {
            return obj.GetDefaultValueByKey("siteId");
        }

        /// <summary>
        ///     Tìm value dc gán với key trong cú pháp ::key1=value1::key2=value2::key3=value3::.......
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetDefaultValueByKey(this object[] obj, string key)
        {
            if (obj == null
               || !obj.Any())
                return string.Empty; // không tìm thấy kết quả

            if (obj[obj.Length - 1] == null)
            {
                return string.Empty;
            }

            var val = obj[obj.Length - 1]
                    .ToString();

            //"transactionId=123455::orderId=100==asdasd::"
            Regex r = new Regex($@"{key}=([A-Za-z0-9\-]+)::");
            if (r.Match(val)
                .Success)
            {
                return r.Match(val)
                        .Groups[1]
                        .Value;
            }

            return string.Empty;
        }
    }
}
