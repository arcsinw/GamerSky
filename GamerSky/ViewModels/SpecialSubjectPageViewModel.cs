using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GamerSky.Controls;
using GamerSky.Interfaces;
using GamerSky.Models;
using GamerSky.Models.ResultDataModel;
using GamerSky.Services;
using GamerSky.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.ViewModels
{
    public class SpecialSubjectPageViewModel : ViewModelBase
    {
        private readonly IMasterDetailNavigationService _navigationService;
        
        public SpecialSubjectPageViewModel(IMasterDetailNavigationService navigationService)
        {
            _navigationService = navigationService;

            ItemClickCommand = new RelayCommand(NavigateToSubject);

            if (IsInDesignMode)
            {
                LoadDesignTimeData();
            }

            GameSpecials = new IncrementalLoadingCollection<GameSpecial>(LoadData,
                () => { },
                () => { },
                (err) => { ToastService.SendToast(err.Message); });
        }

        public IncrementalLoadingCollection<GameSpecial> GameSpecials { get; set; }

        //public ObservableCollection<GameSpecial> GameSpecials { get; set; } = new ObservableCollection<GameSpecial>();

        public GameSpecial SelectedGameSpecial { get; set; }

        public RelayCommand ItemClickCommand { get; set; }

        public void LoadDesignTimeData()
        {
            string json = "{\"errorCode\":0,\"errorMessage\":\"\",\"watchTime\":0.0,\"result\":[{\"nodeId\":253,\"title\":\"GC 2018参展游戏\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180706_zw_104_2.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180706_zw_104_3.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":158,\"title\":\"2018年PSN会免游戏\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180802_lhj_437_4.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180802_lhj_437_3.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":260,\"title\":\"GCA评选E3 2018最佳参展游戏\",\"image\":\"https://image.gamersky.com/gameshd/2018/20180716_lhj_437_1.jpg\",\"smallImage\":\"https://image.gamersky.com/gameshd/2018/20180716_lhj_437_2.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":255,\"title\":\"十大震撼人心的电影向游戏\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180706_zw_104_4.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180706_zw_104_5.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":216,\"title\":\"IGN评选E3 2018最佳游戏\",\"image\":\"https://image.gamersky.com/gameshd/2018/20180623_zw_104_3.jpg\",\"smallImage\":\"https://image.gamersky.com/gameshd/2018/20180623_zw_104_4.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":207,\"title\":\"E3 2018发布会游戏盘点\",\"image\":\"https://image.gamersky.com/gameshd/2018/20180614_zw_104_2.jpg\",\"smallImage\":\"https://image.gamersky.com/gameshd/2018/20180614_zw_104_3.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":200,\"title\":\"E32018参展游戏预测\",\"image\":\"https://image.gamersky.com/gameshd/2018/20180519_zw_104_2.jpg\",\"smallImage\":\"https://image.gamersky.com/gameshd/2018/20180519_zw_104_3.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":202,\"title\":\"“真香”预警 那些画过的大饼\",\"image\":\"https://image.gamersky.com/gameshd/2018/20180606_zw_104_4.jpg\",\"smallImage\":\"https://image.gamersky.com/gameshd/2018/20180606_zw_104_5.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":198,\"title\":\"五月病减压游戏推荐\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180512_zw_104_1.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180512_zw_104_2.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":196,\"title\":\"IGN评最好玩的PS4游戏TOP25\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180428_zw_104_3.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180428_zw_104_4.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":194,\"title\":\"中年大叔专题游戏\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180425_zw_104_2.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180425_zw_104_1.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":192,\"title\":\"游戏中的动物好伙伴\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180412_zw_104_1.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180412_zw_104_2.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":191,\"title\":\"IGN评史上最棒100款游戏2018\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180408_zw_104_1.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180408_zw_104_2.jpg\",\"des\":\"IGN评选的标准是：在这款游戏推出时，它对当时的业界以及作为玩家的我们产生的影响力大小。在评选开头，IGN就明确表示，游戏和其他产品一样，是时代的产物。因此在IGN并未将重心放在几十年前的老游戏能否“hold住”现代3A游戏的挑战上，他们在评选时关注更多的是：这款游戏在自己诞生的时代，给人带来体验的惊艳程度究竟有多高。\",\"hasSubList\":false},{\"nodeId\":187,\"title\":\"2018年精品独立游戏推荐(1)\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180327_hzx_401_3.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180327_hzx_401_4.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":185,\"title\":\"末日启示录\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180321_hzx_401_5.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180321_hzx_401_4.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":138,\"title\":\"战火中的基友情\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180319_hzx_401_5.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180319_hzx_401_4.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":183,\"title\":\"惊艳无比的女神们\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180308_hzx_401_5.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180308_hzx_401_6.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":181,\"title\":\"Switch一周年精品游戏推荐\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180306_hzx_401_7.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180306_hzx_401_6.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":179,\"title\":\"那些所谓的开放世界\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180304_hzx_401_1.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180304_hzx_401_2.jpg\",\"des\":\"\",\"hasSubList\":true},{\"nodeId\":177,\"title\":\"治愈节后综合症的佛系游戏\",\"image\":\"http://image.gamersky.com/gameshd/2018/20180226_hzx_401_4.jpg\",\"smallImage\":\"http://image.gamersky.com/gameshd/2018/20180226_hzx_401_5.jpg\",\"des\":\"\",\"hasSubList\":true}]}";
            var result = JsonHelper.Deserlialize<ResultDataTemplate<List<GameSpecial>>>(json);
            foreach (var item in result.Result)
            {
                GameSpecials.Add(item);
            }
        }
        

        private async Task<IEnumerable<GameSpecial>> LoadData(uint count, int pageIndex)
        {
            var result = await ApiService.Instance.GetGameSpecialList(pageIndex, 20);
            return result?.Result;
        }

        public void NavigateToSubject()
        {
            _navigationService.DetailNavigateTo("SpecialSubjectContentPage", SelectedGameSpecial);
        }
    }
}
