using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Helper
{
    public static class Functions
    {
        /// <summary>
        /// 获取当前Unix timestamp
        /// </summary>
        /// <returns></returns>
        public static long getUnixTimeStamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
        
        /// <summary>
        /// 将T序列化成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonDataSerializer<T>(T t) 
        {
            var result = string.Empty;
            try
            {
                if (t == null) return result;
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, t);

                    byte[] dataBytes = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(dataBytes, 0, (int)stream.Length);
                    return Encoding.UTF8.GetString(dataBytes);

                }
            }
            catch (Exception)
            {
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
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(T));
                    result = deserializer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonText))) as T;

                }
            }
            catch (Exception e)
            {
                return result;
            }
            return result;
        }
    }
}
