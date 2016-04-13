using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
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
                Debug.WriteLine("Function.cs Deserlizlize<T>"+e.Message);
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
        /// 从root中搜索T并加入results
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <param name="root"></param>
        public static void FindChildOfTypeAndInsert<T>(List<T> results,DependencyObject root) where
            T :DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(root);
            for(int i=0;i< count;i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(root, i);
                var typedChild = child as T;
                if(typedChild != null)
                {
                    results.Add(typedChild);
                }
                FindChildOfTypeAndInsert<T>(results, child);
            }
        }
        /// <summary>
        /// 判断是否Mobile设备
        /// </summary>
        /// <returns></returns>
        public static bool IsMobile()
        {
            ResourceContext resContext = ResourceContext.GetForCurrentView();
            string value = resContext.QualifierValues["DeviceFamily"];
            return value.Equals("Mobile");
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
        /// 增加任务栏右键菜单
        /// </summary>
        public static async void LoadJumpList()
        {
            if(ApiInformation.IsTypePresent("Windows.UI.StartScreen.JumpList"))
            {
                JumpList jumpList = await JumpList.LoadCurrentAsync();
                jumpList.Items.Add(JumpListItem.CreateWithArguments("Search", "搜索"));
                jumpList.Items.Add(JumpListItem.CreateWithArguments("Yaowen", "要闻"));
                await jumpList.SaveAsync();
            }
        }

        /// <summary>
        /// 显示MessageDialog
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        public static async void ShowMessage(string content, string title = "")
        {
            await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                await new MessageDialog(content, title).ShowAsync();
            });
        }
    }
}
