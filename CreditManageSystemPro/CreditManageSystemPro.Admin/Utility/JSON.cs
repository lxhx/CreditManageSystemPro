using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace CreditManageSystemPro.Admin.Utility
{
    public class JSON
    {
        /// <summary>
        /// 将对象序列化为JSON字符串
        /// </summary>
        /// <param name="obj">要转换为JSON的对象</param>
        /// <returns>JSON字符串</returns>
        public static string ConvertToJson(object obj)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                ex.Message.Trim();
                throw ex;
            }
        }
        /// <summary>
        /// 将
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T ConvertFromJson<T>(string jsonStr)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(jsonStr);
        }
    }
}
