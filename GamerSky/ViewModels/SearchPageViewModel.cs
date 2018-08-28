using GalaSoft.MvvmLight;
using GamerSky.Interfaces;
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
    public class SearchPageViewModel : ViewModelBase
    {
        private readonly IMasterDetailNavigationService navigationService;

        /// <summary>
        /// 热门搜索
        /// </summary>
        public ObservableCollection<string> HotKeys { get; set; } = new ObservableCollection<string>();

        public SearchPageViewModel(IMasterDetailNavigationService _navigationService)
        {
            navigationService = _navigationService;

            if (IsInDesignModeStatic || IsInDesignMode)
            {
                LoadDesignTimeData();
            }

            LoadDesignTimeData();
        }

        public void LoadDesignTimeData()
        {
            string json = "{\"errorCode\":0,\"errorMessage\":\"\",\"watchTime\":0.0,\"result\":[\"魔兽世界\",\"怪物猎人：世界\",\"天命奇御\",\"如龙0\",\"无人深空\",\"八方旅人\",\"宝可梦探险寻宝\",\"飙酷车神2\",\"侏罗纪世界：进化\",\"吸血鬼\",\"黑暗之魂：重制版\",\"GTA5\",\"黑暗之魂3\",\"底特律：变人\",\"腐烂国度2\",\"战神4\",\"孤岛惊魂5\",\"真三国无双8\",\"堡垒之夜\",\"绝地求生\",\"刺客信条：起源\",\"恶灵附身2\",\"FIFA18\",\"NBA2K18\",\"PES2018\",\"实况足球2018\",\"尼尔：机械纪元\",\"质量效应：仙女座\",\"饥荒\",\"生化危机7\",\"文明6\",\"侠客风云传前传\",\"黑暗之魂3\",\"三国志13\",\"合金装备5：幻痛\",\"侠客风云传\",\"巫师3\",\"我的世界\"]}";
            var result = JsonHelper.Deserlialize<ResultDataTemplate<List<string>>>(json);
            foreach (var item in result?.Result)
            {
                HotKeys.Add(item);
            }
        }

        public async Task LoadSearchHotDic()
        {
            var hotKeys = await ApiService.Instance.GetSearchHotKey();
            foreach (var item in hotKeys)
            {
                HotKeys.Add(item);
            }
        }
    }
}
