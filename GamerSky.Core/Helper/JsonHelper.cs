using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GamerSky.Core.Helper
{
    public static class JsonHelper
    {
        /// <summary>
        /// 将T序列化成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string Serializer<T>(T t)
        {
            var result = string.Empty;
            try
            {
                if (t == null) return result;
                string json = JsonConvert.SerializeObject(t);
                return json;
            }
            catch (Exception e)
            {
                Debug.WriteLine("JsonHelper Serializer<T>" + e.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 将json反序列化为T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T Deserlialize<T>(string jsonText) where T : class
        {
            T result = default(T);
            try
            {
                if (!string.IsNullOrEmpty(jsonText))
                {
                    result = JsonConvert.DeserializeObject<T>(jsonText);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("JsonHelper Deserlizlize<T>" + e.Message);
                return result;
            }
            return result;
        }
    }
}
