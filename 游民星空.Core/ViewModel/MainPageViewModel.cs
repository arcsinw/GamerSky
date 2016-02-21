using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;
using 游民星空.Core.ResultDataModel;

namespace 游民星空.Core.ViewModel
{
    public class MainPageViewModel:ViewModelBase
    {
        ApiService apiService = new ApiService();
        /// <summary>
        /// 频道 
        /// </summary>
        //public ObservableCollection<ChannelResult> Channels { get; set; }

        /// <summary>
        /// 文章列表
        /// </summary>
        //public ObservableCollection<EssayResult> Essays { get; set; }

        private ObservableCollection<PivotData> essayAndChannels;
        /// <summary>
        /// 同时提供频道和文章列表
        /// </summary>
        public ObservableCollection<PivotData> EssaysAndChannels
        {
            get
            {
                return essayAndChannels;
            }
            set
            {
                essayAndChannels = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 幻灯片上的文章
        /// </summary>
        //public ObservableCollection<EssayResult> HeaderEssays { get; set; }


        public MainPageViewModel()
        {
            //Channels = new ObservableCollection<ChannelResult>();
            //Essays = new ObservableCollection<EssayResult>();
            //EssaysDictionary = new ObservableDictionary<string, List<EssayResult>>();
            EssaysAndChannels = new ObservableCollection<PivotData>();
            //HeaderEssays = new ObservableCollection<EssayResult>();
            NavigateToEssayCommand = new RelayCommand((contentId) =>
            {
                (Window.Current.Content as Frame)?.Navigate(typeof(Essay), contentId);
            });

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;

            if (DesignMode.DesignModeEnabled)
            {
                LoadDesignTimeData();
            }
            else
            {
                LoadData();
            }
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }

        private ElementTheme appTheme;
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }
            set
            {
                appTheme = value;
                OnPropertyChanged();
            }
        }

        private async void LoadData()
        {
            List<Channel> channels = await apiService.GetChannelList();
            foreach (var item in channels)
            {
                EssaysAndChannels.Add(new PivotData { Channel = item });
            }

        }

        public RelayCommand NavigateToEssayCommand { get; set; }

        /// <summary>
        /// 加载更多数据
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task LoadMoreEssay(int nodeId,int pageIndex)
        {
            List<Essay> essays = await apiService.GetEssayList(nodeId, pageIndex);
            if (essays == null) return;
            foreach (var item in essays)
            {
                if (item.type.Equals("huandeng"))
                {
                    foreach (var c in item.childElements)
                    {
                        EssaysAndChannels.Where(x => x.Channel.nodeId == nodeId).First().HeaderEssays.Add(c);
                    }
                    continue;
                }
                EssaysAndChannels.Where(x => x.Channel.nodeId == nodeId).First().Essays.Add(item);
            }
            
        }

        
        public void NavigateToEssay(string contentId)
        {
           
        }

        private void LoadDesignTimeData()
        {
            string essaysData = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"type\":\"huandeng\",\"contentType\":\"huandeng\",\"title\":\"幻灯\",\"thumbnailURLs\":null,\"authorName\":null,\"authorHeadImageURL\":null,\"badges\":null,\"readingCount\":0,\"commentsCount\":0,\"contentId\":0,\"contentURL\":null,\"adId\":0,\"childElements\":[{\"type\":\"huandeng\",\"contentType\":\"news\",\"title\":\"《死侍》新剧照、预告 毁容照疤痕可怖\",\"thumbnailURLs\":[\"http://image.gamersky.com/gameshd/2016/20160127_lll_268_38.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":531,\"commentsCount\":0,\"contentId\":710307,\"contentURL\":null,\"adId\":0,\"dataPackage\":false,\"childElements\":null},{\"type\":\"huandeng\",\"contentType\":\"URL\",\"title\":\"网易天下X天下iOS公测\",\"thumbnailURLs\":[\"http://img1.gamersky.com/image2016/01/20160127_tw_1/slide_640.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":null,\"readingCount\":0,\"commentsCount\":0,\"contentId\":0,\"contentURL\":\"http://gad.netease.com/mmad/click?s=Ye8k4iFUQK8kMcEAV98bK4HCykY%3D&project_id=11006838&monitor_type=4\",\"adId\":374,\"dataPackage\":false,\"childElements\":null},{\"type\":\"huandeng\",\"contentType\":\"news\",\"title\":\"玩家三年在我的世界重建《刺客信条4》\",\"thumbnailURLs\":[\"http://image.gamersky.com/gameshd/2016/20160127_lll_268_13.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":173,\"commentsCount\":0,\"contentId\":710149,\"contentURL\":null,\"adId\":0,\"dataPackage\":false,\"childElements\":null},{\"type\":\"huandeng\",\"contentType\":\"news\",\"title\":\"面对Denuvo加密技术 是时候买正版了？\",\"thumbnailURLs\":[\"http://image.gamersky.com/gameshd/2016/20160127_lll_268_1.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":217,\"commentsCount\":0,\"contentId\":709991,\"contentURL\":null,\"adId\":0,\"dataPackage\":false,\"childElements\":null}]},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"《剑灵》美服在线突破100万人 油腻师姐了不得！\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271553539246.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":54,\"commentsCount\":7,\"contentId\":710482,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"日本网友称《海贼王》恐怖三桅帆船篇没用引热议\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271547507788.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":269,\"commentsCount\":4,\"contentId\":710478,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"《三国志13》赵云独家CG曝光 边娶三老婆边一统天下\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271643313325.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":182,\"commentsCount\":7,\"contentId\":710528,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"上厕所一定就会死？好莱坞电影中25条诡异定律\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/pic/2016/0127_zy_164_2.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":265,\"commentsCount\":19,\"contentId\":710456,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"santu\",\"contentType\":\"news\",\"title\":\"《孤岛惊魂：原始杀戮》演示 称霸原始世界\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/pic/2016/20160127_lll_268_27.jpg\",\"http://imgs.gamersky.com/pic/2016/20160127_lll_268_28.jpg\",\"http://imgs.gamersky.com/pic/2016/20160127_lll_268_29.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":289,\"commentsCount\":21,\"contentId\":710493,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"“玩儿趣奖”获奖玩家名单公布！\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271438574500.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":75,\"commentsCount\":14,\"contentId\":710394,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"3亿的摩托车有人会买吗？全世界最贵的十辆重机车\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/pic/2016/0127_zy_164_3.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":202,\"commentsCount\":20,\"contentId\":710472,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"《拳皇14》最新预告片 多位经典角色初登场\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271613225546.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":168,\"commentsCount\":56,\"contentId\":710495,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"秽土转生成摆设《火影忍者》已故的十大人气角色 \",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271607371217.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":338,\"commentsCount\":19,\"contentId\":710476,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"小米5真机与完整包装曝光 这次总算踏实了\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271423117695.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":2638,\"commentsCount\":79,\"contentId\":710414,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"日式冷吐槽：妻子的睡相简直是灾难\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271606494036.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":285,\"commentsCount\":16,\"contentId\":710489,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"腾讯手游进军日本 Aiming获《天天传奇》代理权\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271540546062.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":194,\"commentsCount\":26,\"contentId\":710467,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"《超能英雄重生之双子》显卡实测：970搭配VR更佳\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/pic/2016/20160127_zy_71_7.1.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":208,\"commentsCount\":9,\"contentId\":710401,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"《老炮儿》领跑内地影市 1月豆瓣电影口碑榜\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271141023141.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":193,\"commentsCount\":32,\"contentId\":710322,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"《战国BASARA：真田幸村传》繁体中文版公布\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271433216978.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":156,\"commentsCount\":29,\"contentId\":710424,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"santu\",\"contentType\":\"news\",\"title\":\"《仙剑3》真人版女性定妆照 不输杨幂唐嫣\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/pic/2016/20160127_lll_268_30.jpg\",\"http://imgs.gamersky.com/pic/2016/20160127_lll_268_31.jpg\",\"http://imgs.gamersky.com/pic/2016/20160127_lll_268_32.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":1647,\"commentsCount\":34,\"contentId\":710423,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"《星球大战：前线》新季票内容 预计明年初发布\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601270931136281.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":157,\"commentsCount\":11,\"contentId\":710166,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"关注《新飞飞》官方微信 赢取超萌新年“女朋友”\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271101143596.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":58,\"commentsCount\":2,\"contentId\":710275,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"分分钟上下几千万？现今世界上最赚钱的十大行业\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/pic/2016/0127_zy_164_1.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":232,\"commentsCount\":32,\"contentId\":710379,\"contentURL\":\"\",\"adId\":0,\"childElements\":null},{\"type\":\"xinwen\",\"contentType\":\"news\",\"title\":\"《轩辕剑外传：穹之扉》PS4版公布！增加众多新要素\",\"thumbnailURLs\":[\"http://imgs.gamersky.com/upimg/2016/201601271426455548.jpg\"],\"authorName\":\"\",\"authorHeadImageURL\":\"\",\"badges\":[],\"readingCount\":183,\"commentsCount\":43,\"contentId\":710422,\"contentURL\":\"\",\"adId\":0,\"childElements\":null}]}";
            string channelData = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"nodeId\":16,\"nodeName\":\"游戏\",\"isTop\":false},{\"nodeId\":32,\"nodeName\":\"趣味\",\"isTop\":false},{\"nodeId\":43,\"nodeName\":\"福利\",\"isTop\":false},{\"nodeId\":33,\"nodeName\":\"手游\",\"isTop\":false},{\"nodeId\":30,\"nodeName\":\"影视\",\"isTop\":false},{\"nodeId\":19,\"nodeName\":\"科技\",\"isTop\":false},{\"nodeId\":31,\"nodeName\":\"动漫\",\"isTop\":false},{\"nodeId\":6,\"nodeName\":\"点评\",\"isTop\":false},{\"nodeId\":4,\"nodeName\":\"产业\",\"isTop\":false}]}";
            EssayResult essay = Functions.Deserlialize<EssayResult>(essaysData);
            ChannelResult channel =  Functions.Deserlialize<ChannelResult>(channelData);

            foreach (var item in channel.result)
            {
                EssaysAndChannels.Add(new PivotData { Channel = item });
                //Channels.Add(new ChannelResult() { isTop = item.isTop, nodeId = item.nodeId, nodeName = item.nodeName });
            }

            foreach (var item in essay.result)
            {
                EssaysAndChannels.Where(x => x.Channel.nodeId == 0).First().Essays.Add(item);
                //Essays.Add(item);
            }
        }

        public async Task Refresh(int channelId)
        {
            //Essays.Clear();
            EssaysAndChannels.Where(x => x.Channel.nodeId == channelId).First().Essays.Clear();
            EssaysAndChannels.Where(x => x.Channel.nodeId == channelId).First().HeaderEssays.Clear();
            await LoadMoreEssay(channelId, 1);
        }
    }
}
