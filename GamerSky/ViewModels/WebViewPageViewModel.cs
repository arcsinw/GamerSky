using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GamerSky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.ViewModels
{
    public class WebViewPageViewModel : ViewModelBase
    {
        private string contentUrl;

        public string ContentUrl
        {
            get { return contentUrl; }
            set
            {
                contentUrl = value;
                RaisePropertyChanged("ContentUrl");
            }
        }

        
        public WebViewPageViewModel()
        {
            Messenger.Default.Register<Essay>(this, (Essay essay) =>
            {
                if (essay.Badges != null && essay.Badges.Contains("短讯"))
                {
                    ContentUrl = essay.ContentURL;
                }
                else if (essay.ContentType.Equals("URL", StringComparison.OrdinalIgnoreCase))
                {
                    ContentUrl = essay.ContentURL;
                }

                ContentUrl = "http://appapi2.gamersky.com/v1/ContentDetail/" + essay.ContentId + "/1?fontSize=1&nullImageMode=1&tag=1&deviceid=000000000000000&platform=android&nightMode=0&original=0&t=8424174&v=2";

                //else
                //{
                //    if (DataShareManager.Current.AppTheme == ElementTheme.Dark)
                //    {
                //        ContentUrl = "http://appapi2.gamersky.com/v1/ContentDetail/" + essay.ContentId + "/1?fontSize=1&nullImageMode=1&tag=1&deviceid=000000000000000&platform=android&nightMode=1&original=0&t=8424174&v=2";
                //    }
                //    else
                //    {
                //        ContentUrl = "http://appapi2.gamersky.com/v1/ContentDetail/" + essay.ContentId + "/1?fontSize=1&nullImageMode=1&tag=1&deviceid=000000000000000&platform=android&nightMode=0&original=0&t=8424174&v=2";
                //    }
                //}

            });
        }
    }
}
