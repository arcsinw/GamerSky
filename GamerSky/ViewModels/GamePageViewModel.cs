using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GamerSky.Interfaces;
using GamerSky.Models;
using GamerSky.Models.ResultDataModel;
using GamerSky.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.ViewModels
{
    public class GamePageViewModel : ViewModelBase
    {
        #region Properties
        
        public GameSpecial SelectedGameSpecial { get; set; }

        public GameSpecialDetail SelectedGameSpecialDetail { get; set; }

        /// <summary>
        /// 新游推荐
        /// </summary>
        public ObservableCollection<GameSpecialDetail> NewGames { get; set; } = new ObservableCollection<GameSpecialDetail>();
        
        /// <summary>
        /// 最近游戏
        /// </summary>
        public ObservableCollection<GameSpecialDetail> RecentGames { get; set; } = new ObservableCollection<GameSpecialDetail>();
        
        /// <summary>
        /// 特色专题
        /// </summary>
        public ObservableCollection<GameSpecial> GameSpecialList { get; set; } = new ObservableCollection<GameSpecial>();
        
        /// <summary>
        /// 即将上市
        /// </summary>
        public ObservableCollection<GameSpecialDetail> NewSellingGames { get; set; } = new ObservableCollection<GameSpecialDetail>();

        /// <summary>
        /// 最期待游戏
        /// </summary>
        public ObservableCollection<GameSpecialDetail> MostExpectedGames { get; set; } = new ObservableCollection<GameSpecialDetail>();

        /// <summary>
        /// 高分榜
        /// </summary>
        public ObservableCollection<GameSpecialDetail> HighRankedGames { get; set; } = new ObservableCollection<GameSpecialDetail>();

        public List<GamePageTopItem> GamePageTopItems { get; set; } = new List<GamePageTopItem>()
        {
            new GamePageTopItem(){ ImageUrl = "/Assets/Images/Game/game_tag_top_zyx.png", Description = "找游戏"},
            new GamePageTopItem(){ ImageUrl = "/Assets/Images/Game/game_tag_top_steamzk.png", Description = "Steam折扣"},
            new GamePageTopItem(){ ImageUrl = "/Assets/Images/Game/game_tag_top_psn_xm.png", Description = "PSN限免"},
            new GamePageTopItem(){ ImageUrl = "/Assets/Images/Game/game_tag_top_steamrm.png", Description = "Steam热门"},
            new GamePageTopItem(){ ImageUrl = "/Assets/Images/Game/game_tag_top_ps4_dz.png", Description = "PS4独占"},
            new GamePageTopItem(){ ImageUrl = "/Assets/Images/Game/game_tag_top_ns_dz.png", Description = "NS独占"},
            new GamePageTopItem(){ ImageUrl = "/Assets/Images/Game/game_tag_top_xbox_dz.png", Description = "XB1独占"},
            new GamePageTopItem(){ ImageUrl = "/Assets/Images/Game/game_tag_top_pcdr.png", Description = "PC多人"},
        };
        
        #endregion
        
        private readonly IMasterDetailNavigationService _navigationService;

        #region Commands
        /// <summary>
        /// 近期热门
        /// </summary>
        public RelayCommand GameSpecialDetailClickCommand { get; set; }

        /// <summary>
        /// 特色专题
        /// </summary>
        public RelayCommand GameSpecialClickCommand { get; set; }

        public RelayCommand NavigateToSearchPageCommand { get; set; }

        public RelayCommand NavigateToHistoryPageCommand { get; set; } 
        #endregion

        public GamePageViewModel(IMasterDetailNavigationService navigationService)
        {
            _navigationService = navigationService;

            if (IsInDesignModeStatic)
            {
                LoadDesignTimeData();
            }
            else
            {
                LoadData();
            }

            NavigateToSearchPageCommand = new RelayCommand(NavigationToSearchPage);

            GameSpecialDetailClickCommand = new RelayCommand(GameSpecialDetailClick);

            GameSpecialClickCommand = new RelayCommand(GameSpecialClick);
        }

        private void NavigationToSearchPage()
        {
            _navigationService.DetailNavigateTo("SearchPage");
        }

        public void GameSpecialDetailClick()
        {
            _navigationService.DetailNavigateTo("GameDetailPage", SelectedGameSpecialDetail);
        }

        public void GameSpecialClick()
        {
            _navigationService.DetailNavigateTo("SpecialSubjectContentPage", SelectedGameSpecial);
        }

        public void LoadDesignTimeData()
        {
            string gameSpecialListJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"nodeId\":158,\"title\":\"2018年PSN会免游戏\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180201_hzx_401_4.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180201_hzx_401_5.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":175,\"title\":\"水晶序曲\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180212_hzx_401_6.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180212_hzx_401_7.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":172,\"title\":\"汉末时期的群雄争霸\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180209_hzx_401_1.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180209_hzx_401_2.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":170,\"title\":\"从“猛汉”看世界\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180129_hzx_401_4.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180129_hzx_401_5.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":168,\"title\":\"精品漫改游戏推荐\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180124_hzx_401_3.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180124_hzx_401_4.jpg\",\"des\":\"\",\"hasSubList\":true}]}";
            var specialList = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecial>>>(gameSpecialListJson);
            foreach (var item in specialList.Result)
            {
                GameSpecialList.Add(item);
            }

            string gameSpecialDetailJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"Id\":390521,\"subgroup\":\"新游推荐\",\"Title\":\"真三国无双8\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_dynastywarriors9.jpg\",\"Position\":\"首页-发售表,活动\",\"gsScore\":\"5.6\",\"largeImage\":\"http://image.gamersky.com/gameshd/2018/20180209_hzx_401_3.jpg\",\"description\":\"《真三国无双8》作为系列最新作品，系列首次加入沙盒地图的模式以及打猎钓鱼等休闲元素，让玩法更加多元化起来。\"},{\"Id\":944928,\"subgroup\":\"新游推荐\",\"Title\":\"刀剑神域：夺命凶弹\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_swordartonlinefatalbulleti.jpg\",\"Position\":\"\",\"gsScore\":\"8\",\"largeImage\":\"http://image.gamersky.com/gameshd/2018/20180209_hzx_401_4.jpg\",\"description\":\"《刀剑神域：夺命凶弹》作为轻小说改编游戏的SAO系列最新作品，把原作中的“枪界”搬上了舞台，故事也从剑与魔法进化到了枪与科技。\"},{\"Id\":915326,\"subgroup\":\"新游推荐\",\"Title\":\"怪物猎人：世界\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_monsterhunterworld.jpg\",\"Position\":\"首页-发售表,活动\",\"gsScore\":\"9.5\",\"largeImage\":\"http://image.gamersky.com/gameshd/2018/20180128_hzx_401_1.jpg\",\"description\":\"《怪物猎人：世界》作为卡普空旗下的扛鼎之作，完美的动作系统加上次时代级别的画面，让这个系列开启了全新的篇章。\"},{\"Id\":734122,\"subgroup\":\"新游推荐\",\"Title\":\"深海迷航\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_subnautica.jpg\",\"Position\":\"\",\"gsScore\":\"9.2\",\"largeImage\":\"http://image.gamersky.com/gameshd/2018/20180128_hzx_401_3.jpg\",\"description\":\"《深海迷航》把充满奇幻和想象的海底世界完整的展现在了玩家眼前，丰富的水生生物与离奇的冒险组成了这款充满魅力的游戏。\"}]}";
            var gameSpecial = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecialDetail>>>(gameSpecialDetailJson);
            foreach (var item in gameSpecial.Result)
            {
                NewGames.Add(item);
            }

            string gameHomePageJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"Id\":390521,\"Title\":\"真三国无双8\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_dynastywarriors9.jpg\",\"Position\":\"首页-发售表,活动\",\"gsScore\":\"5.6\"},{\"Id\":369418,\"Title\":\"天国：拯救\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2014/ku_kingdomcomedeliverance.jpg\",\"Position\":\"首页-发售表\",\"gsScore\":\"8.3\"},{\"Id\":915326,\"Title\":\"怪物猎人：世界\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_monsterhunterworld.jpg\",\"Position\":\"首页-发售表,活动\",\"gsScore\":\"9.5\"},{\"Id\":862094,\"Title\":\"绝地求生\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_playbattlegrounds.jpg\",\"Position\":\"首页-发售表\",\"gsScore\":\"8.2\"},{\"Id\":944928,\"Title\":\"刀剑神域：夺命凶弹\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_swordartonlinefatalbulleti.jpg\",\"Position\":\"\",\"gsScore\":\"8\"}]}";
            var recentGames = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecialDetail>>>(gameHomePageJson);
            foreach (var item in recentGames.Result)
            {
                RecentGames.Add(item);
            }

            string mostExpectedGamesJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"Id\":390549,\"Title\":\"GTA6\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_grandtheftauto6.jpg\",\"Position\":\"\",\"wantplayCount\":4769},{\"Id\":345710,\"Title\":\"骑马与砍杀2：领主\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_mountblade2bannerlord.jpg\",\"Position\":\"首页-发售表\",\"wantplayCount\":2834},{\"Id\":481777,\"Title\":\"孤岛惊魂5\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_farcry5.jpg\",\"Position\":\"首页-发售表\",\"wantplayCount\":1473},{\"Id\":534517,\"Title\":\"战神4\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_godofwar4.jpg\",\"Position\":\"首页-发售表\",\"wantplayCount\":1056},{\"Id\":1001150,\"Title\":\"三国：全面战争\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_totalwarthreekingdoms.jpg\",\"Position\":\"\",\"wantplayCount\":720}]}";
            var expectedGames = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecialDetail>>>(mostExpectedGamesJson);
            foreach (var item in expectedGames.Result)
            {
                MostExpectedGames.Add(item);
            }

            string highRankedJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"Id\":293047,\"Title\":\"GTA5\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2013/ku_gta5.jpg\",\"Position\":\"\",\"gsScore\":\"9.4\"},{\"Id\":291570,\"Title\":\"上古卷轴5：天际\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_tes5.jpg\",\"Position\":\"\",\"gsScore\":\"9.2\"},{\"Id\":349504,\"Title\":\"侠客风云传\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_xiakefengyunzhuan.jpg\",\"Position\":\"\",\"gsScore\":\"8.9\"},{\"Id\":311602,\"Title\":\"真三国无双7：猛将传\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_shinsangokumusou7moushouden.jpg\",\"Position\":\"\",\"gsScore\":\"8.6\"},{\"Id\":293016,\"Title\":\"巫师3：狂猎\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_thewitcher3_pc.jpg\",\"Position\":\"\",\"gsScore\":\"9.7\"}]}";
            var highRankedGames = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecialDetail>>>(highRankedJson);
            foreach (var item in highRankedGames.Result)
            {
                HighRankedGames.Add(item);
            }

            string newSellingJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"Id\":915263,\"Title\":\"帝国时代：终极版\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_ageonede.jpg\",\"Position\":\"\",\"AllTimeT\":\"2018/2/20 0:00:00\",\"gsScore\":\"0\",\"wantplayCount\":245},{\"Id\":938541,\"Title\":\"少女与战车：战车梦幻大会战\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_girlsundpanzerdreamtankmatch.jpg\",\"Position\":\"\",\"AllTimeT\":\"2018/2/22 0:00:00\",\"gsScore\":\"0\",\"wantplayCount\":22},{\"Id\":795516,\"Title\":\"合金装备：幸存\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_metalgearsurvive.jpg\",\"Position\":\"首页-发售表\",\"AllTimeT\":\"2018/2/22 0:00:00\",\"gsScore\":\"0\",\"wantplayCount\":347},{\"Id\":314023,\"Title\":\"最终幻想15\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_ff15.jpg\",\"Position\":\"首页-发售表\",\"AllTimeT\":\"2018/3/7 0:00:00\",\"gsScore\":\"8.1\",\"wantplayCount\":384},{\"Id\":948306,\"Title\":\"人中北斗\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_hakutogagotoku.jpg\",\"Position\":\"\",\"AllTimeT\":\"2018/3/8 0:00:00\",\"gsScore\":\"0\",\"wantplayCount\":68}]}";
            var newSellingGames = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecialDetail>>>(newSellingJson);
            foreach (var item in newSellingGames.Result)
            {
                NewSellingGames.Add(item);
            }
        }

        public void LoadData()
        {
            string gameSpecialListJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"nodeId\":158,\"title\":\"2018年PSN会免游戏\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180201_hzx_401_4.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180201_hzx_401_5.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":175,\"title\":\"水晶序曲\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180212_hzx_401_6.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180212_hzx_401_7.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":172,\"title\":\"汉末时期的群雄争霸\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180209_hzx_401_1.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180209_hzx_401_2.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":170,\"title\":\"从“猛汉”看世界\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180129_hzx_401_4.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180129_hzx_401_5.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":168,\"title\":\"精品漫改游戏推荐\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180124_hzx_401_3.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180124_hzx_401_4.jpg\",\"des\":\"\",\"hasSubList\":true}]}";
            var specialList = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecial>>>(gameSpecialListJson);
            foreach (var item in specialList.Result)
            {
                GameSpecialList.Add(item);
            }

            string gameSpecialDetailJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"Id\":390521,\"subgroup\":\"新游推荐\",\"Title\":\"真三国无双8\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_dynastywarriors9.jpg\",\"Position\":\"首页-发售表,活动\",\"gsScore\":\"5.6\",\"largeImage\":\"http://image.gamersky.com/gameshd/2018/20180209_hzx_401_3.jpg\",\"description\":\"《真三国无双8》作为系列最新作品，系列首次加入沙盒地图的模式以及打猎钓鱼等休闲元素，让玩法更加多元化起来。\"},{\"Id\":944928,\"subgroup\":\"新游推荐\",\"Title\":\"刀剑神域：夺命凶弹\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_swordartonlinefatalbulleti.jpg\",\"Position\":\"\",\"gsScore\":\"8\",\"largeImage\":\"http://image.gamersky.com/gameshd/2018/20180209_hzx_401_4.jpg\",\"description\":\"《刀剑神域：夺命凶弹》作为轻小说改编游戏的SAO系列最新作品，把原作中的“枪界”搬上了舞台，故事也从剑与魔法进化到了枪与科技。\"},{\"Id\":915326,\"subgroup\":\"新游推荐\",\"Title\":\"怪物猎人：世界\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_monsterhunterworld.jpg\",\"Position\":\"首页-发售表,活动\",\"gsScore\":\"9.5\",\"largeImage\":\"http://image.gamersky.com/gameshd/2018/20180128_hzx_401_1.jpg\",\"description\":\"《怪物猎人：世界》作为卡普空旗下的扛鼎之作，完美的动作系统加上次时代级别的画面，让这个系列开启了全新的篇章。\"},{\"Id\":734122,\"subgroup\":\"新游推荐\",\"Title\":\"深海迷航\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_subnautica.jpg\",\"Position\":\"\",\"gsScore\":\"9.2\",\"largeImage\":\"http://image.gamersky.com/gameshd/2018/20180128_hzx_401_3.jpg\",\"description\":\"《深海迷航》把充满奇幻和想象的海底世界完整的展现在了玩家眼前，丰富的水生生物与离奇的冒险组成了这款充满魅力的游戏。\"}]}";
            var gameSpecial = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecialDetail>>>(gameSpecialDetailJson);
            foreach (var item in gameSpecial.Result)
            {
                NewGames.Add(item);
            }

            string gameHomePageJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"Id\":390521,\"Title\":\"真三国无双8\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_dynastywarriors9.jpg\",\"Position\":\"首页-发售表,活动\",\"gsScore\":\"5.6\"},{\"Id\":369418,\"Title\":\"天国：拯救\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2014/ku_kingdomcomedeliverance.jpg\",\"Position\":\"首页-发售表\",\"gsScore\":\"8.3\"},{\"Id\":915326,\"Title\":\"怪物猎人：世界\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_monsterhunterworld.jpg\",\"Position\":\"首页-发售表,活动\",\"gsScore\":\"9.5\"},{\"Id\":862094,\"Title\":\"绝地求生\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_playbattlegrounds.jpg\",\"Position\":\"首页-发售表\",\"gsScore\":\"8.2\"},{\"Id\":944928,\"Title\":\"刀剑神域：夺命凶弹\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_swordartonlinefatalbulleti.jpg\",\"Position\":\"\",\"gsScore\":\"8\"}]}";
            var recentGames = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecialDetail>>>(gameHomePageJson);
            foreach (var item in recentGames.Result)
            {
                RecentGames.Add(item);
            }

            string mostExpectedGamesJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"Id\":390549,\"Title\":\"GTA6\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_grandtheftauto6.jpg\",\"Position\":\"\",\"wantplayCount\":4769},{\"Id\":345710,\"Title\":\"骑马与砍杀2：领主\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_mountblade2bannerlord.jpg\",\"Position\":\"首页-发售表\",\"wantplayCount\":2834},{\"Id\":481777,\"Title\":\"孤岛惊魂5\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_farcry5.jpg\",\"Position\":\"首页-发售表\",\"wantplayCount\":1473},{\"Id\":534517,\"Title\":\"战神4\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_godofwar4.jpg\",\"Position\":\"首页-发售表\",\"wantplayCount\":1056},{\"Id\":1001150,\"Title\":\"三国：全面战争\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_totalwarthreekingdoms.jpg\",\"Position\":\"\",\"wantplayCount\":720}]}";
            var expectedGames = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecialDetail>>>(mostExpectedGamesJson);
            foreach (var item in expectedGames.Result)
            {
                MostExpectedGames.Add(item);
            }

            string highRankedJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"Id\":293047,\"Title\":\"GTA5\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2013/ku_gta5.jpg\",\"Position\":\"\",\"gsScore\":\"9.4\"},{\"Id\":291570,\"Title\":\"上古卷轴5：天际\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_tes5.jpg\",\"Position\":\"\",\"gsScore\":\"9.2\"},{\"Id\":349504,\"Title\":\"侠客风云传\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_xiakefengyunzhuan.jpg\",\"Position\":\"\",\"gsScore\":\"8.9\"},{\"Id\":311602,\"Title\":\"真三国无双7：猛将传\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_shinsangokumusou7moushouden.jpg\",\"Position\":\"\",\"gsScore\":\"8.6\"},{\"Id\":293016,\"Title\":\"巫师3：狂猎\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_thewitcher3_pc.jpg\",\"Position\":\"\",\"gsScore\":\"9.7\"}]}";
            var highRankedGames = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecialDetail>>>(highRankedJson);
            foreach (var item in highRankedGames.Result)
            {
                HighRankedGames.Add(item);
            }

            string newSellingJson = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"Id\":915263,\"Title\":\"帝国时代：终极版\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_ageonede.jpg\",\"Position\":\"\",\"AllTimeT\":\"2018/2/20 0:00:00\",\"gsScore\":\"0\",\"wantplayCount\":245},{\"Id\":938541,\"Title\":\"少女与战车：战车梦幻大会战\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_girlsundpanzerdreamtankmatch.jpg\",\"Position\":\"\",\"AllTimeT\":\"2018/2/22 0:00:00\",\"gsScore\":\"0\",\"wantplayCount\":22},{\"Id\":795516,\"Title\":\"合金装备：幸存\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_metalgearsurvive.jpg\",\"Position\":\"首页-发售表\",\"AllTimeT\":\"2018/2/22 0:00:00\",\"gsScore\":\"0\",\"wantplayCount\":347},{\"Id\":314023,\"Title\":\"最终幻想15\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_ff15.jpg\",\"Position\":\"首页-发售表\",\"AllTimeT\":\"2018/3/7 0:00:00\",\"gsScore\":\"8.1\",\"wantplayCount\":384},{\"Id\":948306,\"Title\":\"人中北斗\",\"DefaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_hakutogagotoku.jpg\",\"Position\":\"\",\"AllTimeT\":\"2018/3/8 0:00:00\",\"gsScore\":\"0\",\"wantplayCount\":68}]}";
            var newSellingGames = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecialDetail>>>(newSellingJson);
            foreach (var item in newSellingGames.Result)
            {
                NewSellingGames.Add(item);
            }
        }

        
    }
}
