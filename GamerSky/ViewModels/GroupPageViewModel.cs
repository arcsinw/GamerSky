using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GamerSky.Interfaces;
using GamerSky.Models.Group;
using GamerSky.Models.ResultDataModel;
using GamerSky.Services;
using GamerSky.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GamerSky.ViewModels
{
   public class GroupPageViewModel : ViewModelBase
    {
        /// <summary>
        /// 话题
        /// </summary>
        public ObservableCollection<Club> Subjects { get; set; } = new ObservableCollection<Club>();

        public RelayCommand SubjectSelectedCommand { get; set; }

        private readonly IMasterDetailNavigationService _navigationService;


        public GroupPageViewModel()
        {
            if (IsInDesignMode)
            {
                LoadDesignTimeData();
            }

            LoadSubjects();
        }

        public void LoadDesignTimeData()
        {
            // 全部话题
            string data = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":[{\"id\":3,\"name\":\"#\u6652#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171209_fyj_406_2.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u8FD9\u662F\u4E00\u4E2A\u5206\u4EAB\u6652\u56FE\u7684\u5C0F\u5929\u5730\",\"managerId\":\"38627,1961834\",\"usersCount\":35426,\"topicsCount\":3517,\"recommendLevel\":1},{\"id\":14,\"name\":\"#\u6BCF\u65E5\u58C1\u7EB8\u63A8\u8350#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171212_fyj_406_7.jpg\",\"backgroundImageURL\":\"\",\"description\":\"JR\u6BCF\u5929\u90FD\u4F1A\u5728\u8FD9\u91CC\u63A8\u8350\u4E00\u5F20\u7CBE\u9009\u58C1\u7EB8\uFF0C\u66F4\u591A\u597D\u770B\u58C1\u7EB8\u8BF7\u5173\u6CE8\u7F51\u7AD9\u5468\u516D\u53D1\u5E03\u7684\u201C\u6BCF\u5468\u7CBE\u9009\u58C1\u7EB8\u201D~\",\"managerId\":\"1961834,87078,174757\",\"usersCount\":7984,\"topicsCount\":1071,\"recommendLevel\":1},{\"id\":41,\"name\":\"#\u4ECA\u65E5\u4EFD\u7684\u8F6F\u59B9#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2018/20180224_cks_170_1.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u5171\u8D4F\u79C0\u8272\uFF0C\u8BF7\u52FF\u53D1\u8F66\",\"managerId\":\"1961834,174757,38627\",\"usersCount\":5509,\"topicsCount\":491,\"recommendLevel\":1},{\"id\":13,\"name\":\"#\u6E38\u6C11\u8868\u60C5\u5305\u4ED3\u5E93#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171212_fyj_406_5.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u5C5E\u4E8E\u54B1\u6E38\u6C11\u73A9\u5BB6\u7684\u8868\u60C5\u5305\u4ED3\u5E93~\",\"managerId\":\"1961834\",\"usersCount\":3212,\"topicsCount\":424,\"recommendLevel\":1},{\"id\":51,\"name\":\"#\u9006\u6C34\u5BD2\u798F\u5229#\",\"thumbnailURL\":\"https://imgs.gamersky.com/pic/2018/20180606_fyj_406_30.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u7F51\u6613\u65D7\u8230\u7EA7\u6B66\u4FA0\u6E38\u620F\u300A\u9006\u6C34\u5BD2\u300B\u5C06\u4E8E6\u670829\u65E5\u5F00\u542F\u4E0D\u9650\u53F7\u5185\u6D4B\u3002\u53C2\u4E0E\u9006\u6C34\u5BD2\u6BCF\u65E5\u8BDD\u9898\u8BA8\u8BBA\uFF0C\u5373\u6709\u673A\u4F1A\u9886\u53D6\u6E38\u6C11\u661F\u7A7A\u4E13\u5C5E\u7684\u6E38\u620F\u793C\u5305\uFF01\",\"managerId\":\"1961834,1381627\",\"usersCount\":3154,\"topicsCount\":129,\"recommendLevel\":1},{\"id\":36,\"name\":\"#\u4ECA\u65E5\u306E\u5947\u5947\u602A\u602A\u5185\u5BB9#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2018/20180209_lq_52_2.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u4EBA\u5BB6\u6D3B\u5F97\u662F\u6545\u4E8B\uFF0C\u6211\u751F\u751F\u6D3B\u6210\u4E86\u6BB5\u5B50\",\"managerId\":\"136759,1961834,38627\",\"usersCount\":1154,\"topicsCount\":120,\"recommendLevel\":1},{\"id\":32,\"name\":\"#\u4ECA\u5929\u7684\u8F66\u5230\u4E86#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171225_cks_170_1.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u8BF7\u75AF\u72C2\u5B89\u5229\uFF01\",\"managerId\":\"174757,1961834\",\"usersCount\":2650,\"topicsCount\":119,\"recommendLevel\":1},{\"id\":50,\"name\":\"#\u96BE\u5FD8\u7684\u7AE5\u5E74\u6E38\u620F#\",\"thumbnailURL\":\"https://imgs.gamersky.com/pic/2018/20180531_fyj_406_3.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u8BA9\u601D\u7EEA\u56DE\u5230\u7AE5\u5E74\uFF0C\u60F3\u8D77\u662F\u513F\u65F6\u73A9\u8FC7\u7684\u54EA\u4E00\u6B3E\u6E38\u620F\u8BA9\u4F60\u81F3\u4ECA\u8BB0\u5FC6\u72B9\u65B0\uFF1F\",\"managerId\":\"1961834,38627,1292637\",\"usersCount\":596,\"topicsCount\":84,\"recommendLevel\":1},{\"id\":49,\"name\":\"#\u6218\u610F\u798F\u5229#\",\"thumbnailURL\":\"https://imgs.gamersky.com/pic/2018/20180522_fyj_406_16.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u7F51\u6613\u65D7\u8230\u7EA7\u51B7\u5175\u5668\u9A91\u6218\u7F51\u6E38\u300A\u6218\u610F\u300B\u5C06\u4E8E6\u67088\u65E5\u5F00\u542F\u4E0D\u5220\u6863\u6D4B\u8BD5\uFF0C\u53C2\u4E0E\u6218\u610F\u6BCF\u65E5\u8BDD\u9898\u8BA8\u8BBA\uFF0C\u5373\u6709\u673A\u4F1A\u9886\u53D6\u6218\u610F\u4E0D\u5220\u6863\u6FC0\u6D3B\u7801\uFF01\",\"managerId\":\"1961834,402046,38627,1292637,1381627,633526\",\"usersCount\":8683,\"topicsCount\":76,\"recommendLevel\":1},{\"id\":37,\"name\":\"#G\u5A18\u4E07\u4E8B\u5C4B#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171218_cks_170_1.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u4F5C\u4E3A\u6E38\u6C11\u65B0\u4EFB\u5409\u7965\u7269\uFF0CG\u5A18\u8FEB\u5207\u5730\u60F3\u548C\u5927\u5BB6\u591A\u591A\u4E86\u89E3\uFF0C\u4E5F\u60F3\u77E5\u9053\u5927\u5BB6\u5BF9\u6E38\u6C11\u661F\u7A7A\u6709\u54EA\u4E9B\u610F\u89C1\u548C\u5EFA\u8BAE\u3002\u5F53\u7136\u6709\u4EFB\u4F55\u95EE\u9898\u4E5F\u90FD\u6B22\u8FCE\u5728\u8FD9\u91CC\u7559\u8A00\uFF0C\u6211\u4F1A\u5C3D\u5FEB\u5E2E\u52A9\u5927\u5BB6\u89E3\u51B3\uFF01\",\"managerId\":\"1961834\",\"usersCount\":457,\"topicsCount\":35,\"recommendLevel\":1},{\"id\":42,\"name\":\"#\u6E38\u4E9B\u77E5\u8BC6#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2018/20180302_cks_170_1.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u4EAB\u4F60\u6240\u60F3\uFF0C\u8FF0\u4F60\u6240\u77E5\",\"managerId\":\"1961834,174757\",\"usersCount\":338,\"topicsCount\":27,\"recommendLevel\":1},{\"id\":15,\"name\":\"#2017\u5E74\u5EA6\u6700\u4F73\u6E38\u620F#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171215_fyj_406_1.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u6295\u7968\u9009\u51FA\u4F60\u5FC3\u4E2D\u7684\u5E74\u5EA6\u6700\u4F73\u6E38\u620F\uFF0C\u8FD8\u6709\u673A\u4F1A\u83B7\u5F97\u5956\u54C1\u54E6~\",\"managerId\":\"87078,174757,107943\",\"usersCount\":1506,\"topicsCount\":998,\"recommendLevel\":0},{\"id\":6,\"name\":\"#\u8BF4\u8BF4\u4F60\u5FC3\u4E2D\u7684\u5E74\u5EA6\u6700\u4F73Top6\u6E38\u620F#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171209_fyj_406_5.jpg\",\"backgroundImageURL\":\"\",\"description\":\"2017\u5E74\u9A6C\u4E0A\u5C31\u8981\u8FC7\u53BB\u4E86\uFF0C\u6765\u804A\u804A\u4ECA\u5E74\u4F60\u5FC3\u76EE\u4E2D\u7684\u516D\u6B3E\u6700\u4F73\u6E38\u620F\u5427\uFF01\u5982\u679C\u4E0E\u6211\u4EEC\u73A9\u8DA3\u5956\u7684\u5E74\u5EA6\u6700\u4F73\u5019\u9009\u63D0\u540D\u5B8C\u5168\u4E00\u81F4\u8FD8\u6709\u673A\u4F1A\u8D62\u53D6\u4E3B\u673A\u54E6\",\"managerId\":\"87078,38627\",\"usersCount\":1093,\"topicsCount\":869,\"recommendLevel\":0},{\"id\":45,\"name\":\"#\u300A\u5B64\u5C9B\u60CA\u9B425\u300BAMD\u56DB\u5927\u9ED1\u79D1\u6280\uFF0C\u54EA\u4E2A\u662F\u4F60\u7684\u83DC#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2018/20180327_fyj_406_1.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u300A\u5B64\u5C9B\u60CA\u9B425\u300B\u5DF2\u7ECF\u6B63\u5F0F\u53D1\u552E\u4E86\uFF0CG\u5A18\u8054\u5408AMD\u73A9\u5BB6\u4FF1\u4E50\u90E8\u4E3A\u5927\u5BB6\u5E26\u6765\u4E00\u5927\u6CE2\u798F\u5229\uFF0C\u5305\u62EC\u300A\u5B64\u5C9B\u60CA\u9B425\u300B\u7684\u6E38\u620F\u6FC0\u6D3B\u78013\u4E2A\uFF0C\u4EE5\u53CA\u300A\u5B64\u5C9B\u60CA\u9B425\u300B\u7684\u7CBE\u7F8E\u624B\u529E1\u4E2A\u7B49\u4F60\u6765\u62FF\uFF01\",\"managerId\":\"1961834,38627,1292637\",\"usersCount\":1136,\"topicsCount\":805,\"recommendLevel\":0},{\"id\":39,\"name\":\"#\u73A9\u5BB6\u8FC7\u5E74\u90A3\u4E9B\u4E8B\u513F#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2018/20180213_fyj_406_6.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u6625\u8282\u300C\u5E74\u5473\u300D\u6652\uFF0C\u53C2\u4E0E\u8BDD\u9898\u8BA8\u8BBA\u8D62\u53D6\u798F\u5229\u5956\u54C1\",\"managerId\":\"1961834\",\"usersCount\":2626,\"topicsCount\":535,\"recommendLevel\":0},{\"id\":16,\"name\":\"#2017\u5E74\u5EA6\u52A8\u4F5C\u5192\u9669\u6E38\u620F#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171215_fyj_406_2.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u6295\u7968\u9009\u51FA\u4F60\u5FC3\u4E2D\u7684\u5E74\u5EA6\u52A8\u4F5C\u5192\u9669\u6E38\u620F\uFF0C\u8FD8\u6709\u673A\u4F1A\u83B7\u5F97\u5956\u54C1\u54E6~\",\"managerId\":\"87078,174757,107943\",\"usersCount\":540,\"topicsCount\":435,\"recommendLevel\":0},{\"id\":18,\"name\":\"#2017\u5E74\u5EA6RPG\u6E38\u620F#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171215_fyj_406_4.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u6295\u7968\u9009\u51FA\u4F60\u5FC3\u4E2D\u7684\u5E74\u5EA6RPG\u6E38\u620F\uFF0C\u8FD8\u6709\u673A\u4F1A\u83B7\u5F97\u5956\u54C1\u54E6~\",\"managerId\":\"87078,174757,107943\",\"usersCount\":594,\"topicsCount\":386,\"recommendLevel\":0},{\"id\":22,\"name\":\"#2017\u5E74\u5EA6\u5931\u671B\u6E38\u620F#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171215_fyj_406_8.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u6295\u7968\u9009\u51FA\u4F60\u5FC3\u4E2D\u7684\u5E74\u5EA6\u5931\u671B\u6E38\u620F\uFF0C\u8FD8\u6709\u673A\u4F1A\u83B7\u5F97\u5956\u54C1\u54E6~\",\"managerId\":\"87078,174757,107943\",\"usersCount\":603,\"topicsCount\":342,\"recommendLevel\":0},{\"id\":24,\"name\":\"#2017\u5E74\u5EA6\u5927\u4E8B\u4EF6\u4E3B\u64AD#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171215_fyj_406_10.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u6295\u7968\u9009\u51FA\u4F60\u5FC3\u4E2D\u7684\u5E74\u5EA6\u5927\u4E8B\u4EF6\u4E3B\u64AD\uFF0C\u8FD8\u6709\u673A\u4F1A\u83B7\u5F97\u5956\u54C1\u54E6~\",\"managerId\":\"87078,174757,107943\",\"usersCount\":689,\"topicsCount\":334,\"recommendLevel\":0},{\"id\":17,\"name\":\"#2017\u5E74\u5EA6\u5C04\u51FB\u6E38\u620F#\",\"thumbnailURL\":\"http://imgs.gamersky.com/pic/2017/20171215_fyj_406_3.jpg\",\"backgroundImageURL\":\"\",\"description\":\"\u6295\u7968\u9009\u51FA\u4F60\u5FC3\u4E2D\u7684\u5E74\u5EA6\u5C04\u51FB\u6E38\u620F\uFF0C\u8FD8\u6709\u673A\u4F1A\u83B7\u5F97\u5956\u54C1\u54E6~\",\"managerId\":\"87078,174757,107943\",\"usersCount\":506,\"topicsCount\":332,\"recommendLevel\":0}]}";
            var subjectsList = JsonHelper.Deserlialize<ResultDataTemplate<List<Club>>>(data);
            foreach (var item in subjectsList.Result)
            {
                Subjects.Add(item);
            }
        }

        public async void LoadSubjects()
        {
            var result = await ApiService.Instance.GetSubjectsListAsync(1);

            if (result != null)
            {
                foreach (var item in result)
                {
                    Subjects.Add(item);
                }
            }
        }
    }
}
