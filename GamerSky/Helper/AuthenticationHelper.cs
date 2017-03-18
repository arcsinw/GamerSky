using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace GamerSky.Helper
{
    public class AuthenticationHelper
    {
        private const string SinaWeiboUri = "https://api.weibo.com/oauth2/authorize?response_type=code&client_id=1585983764&redirect_uri=http%3A%2F%2Fi.gamersky.com%2Foauth%2Fsinacallback";
        //private const string SinaWeiBoUri = "https://api.weibo.com/oauth2/authorize?client_id=3341101057&response_type=code&redirect_uri=http://daily.ibaozou.com/sina_weibo/auth&display=mobile";
        //private const string SinaWeiBoUri = "https://api.weibo.com/oauth2/access_token?client_id=3341101057&client_secret=0d0fe859e31afdc487d89fa3f3f0fe40&grant_type=authorization_code&redirect_uri=http://daily.ibaozou.com/sina_weibo/auth&code=deb007a86f240a1bf298d9c3e7016888";
        //private const string SinaCallBackUri = "http://www.gamersky.com/sina_weibo/auth";
        private const string SinaCallbackUri = "http://i.gamersky.com/oauth/sinacallback";

        //private const string TencentWeiBoUri = "https://open.t.qq.com/cgi-bin/oauth2/authorize?client_id=801427997&response_type=code&redirect_uri=http://daily.ibaozou.com/sina_weibo/auth&wap=false";
        //private const string TencentWeiBoCallBackUri = "http://daily.ibaozou.com/sina_weibo/auth";

        public static async Task<string> SinaAuthenticationAsync()
        {
            string sina = await Authentication(SinaWeiboUri, SinaCallbackUri);
            return sina;
        }

        //public static async Task<string> TencentAuthentication()
        //{
        //    string tecent = await Authentication(TencentWeiBoUri, TencentWeiBoCallBackUri);
        //    return tecent;
        //}

        public static async Task<string> Authentication(string requestUri, string callbackUri)
        {
            try
            {
                WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync(
                    WebAuthenticationOptions.None,
                    new Uri(requestUri),
                    new Uri(callbackUri));

                if (result.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    return result.ResponseData.ToString();
                }
                else if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    return string.Empty;
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                Debug.WriteLine("AuthenticationHelper : "+e.Message);
                return string.Empty;
            }
        }
    }
}

