using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using GamerSky.Core.Helper;
using Newtonsoft.Json;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 有攻略的游戏列表
    /// </summary>
    public class Strategy
    {
        [JsonProperty(PropertyName = "favoriteCnt")]
        public string FavoriteCnt { get; set; }

        [JsonProperty(PropertyName = "specialID")]
        public int SpecialID { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        [JsonProperty(PropertyName = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
        
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
    
}
