using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using GamerSky.Core.Helper;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 有攻略的游戏列表
    /// </summary>
    public class Strategy
    {
        public string favoriteCnt { get; set; }
        public int specialID { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string thumbnailUrl { get; set; }
        
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
    }
    
}
