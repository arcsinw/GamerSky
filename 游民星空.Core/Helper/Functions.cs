using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

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

     

        /// <summary>
        /// 在UI对象中查找一个子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static T FindChildOfType<T>(DependencyObject root) where T : class
        {
            //创建一个队列结构来存放可视化树的对象
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            //循环查找类型
            while (queue.Count > 0)
            {
                DependencyObject current = queue.Dequeue();
                //查找子节点的对象类型
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typedChild = child as T;
                    if (typedChild != null)
                    {
                        return typedChild;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }

        /// <summary>
        /// 判断是否Mobile设备
        /// </summary>
        /// <returns></returns>
        public static bool IsMobile()
        {
            ResourceContext resContext = ResourceContext.GetForCurrentView();

            string value = resContext.QualifierValues["DeviceFamily"];
            return value == "Mobile";
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            var version = Package.Current.Id.Version;
            return version.Major.ToString() + "."+ version.Minor.ToString()+"."+
                version.Build.ToString() + "." + version.Revision.ToString();
        }

        /// <summary>
        /// 获取作者
        /// </summary>
        /// <returns></returns>
        public static string GetAuthor()
        {
            var author = Package.Current.PublisherDisplayName;
            return author;
        }

        /// <summary>
        /// 获取屏幕宽度
        /// </summary>
        /// <returns></returns>
        public static double GetScreenHeight()
        {
            return ApplicationView.GetForCurrentView().VisibleBounds.Height;
        }
    }
}
