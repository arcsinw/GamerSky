﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using GamerSky.Http;
using GamerSky.Model;

namespace GamerSky.Helper
{
    /// <summary>
    /// Toast 通知
    /// </summary>
    public class ToastHelper
    {
        private const string toastXml = @"
                <toast launch='develop-defined-string'>
                    <visual>
                        <binding template='ToastGeneric'>
                            <text>{0}</text>
                            <image placement='appLogoOverride' src='ms-appx:///Assets/StoreLogo.png'/>
                        </binding>
                    </visual>
                    <actions>
                    </actions>
                </toast>";
         
        public static async void ShowToast()
        {
            //获取要闻
            List<Essay> essays = await ApiService.Instance.GetYaowen();
            if(essays!= null)
            {
                foreach (var item in essays)
                {
                    XmlDocument doc = new XmlDocument();
                    string.Format(toastXml, item.Title);
                    doc.LoadXml(toastXml);
                    ToastNotification notification = new ToastNotification(doc);
                    ToastNotifier notifier = ToastNotificationManager.CreateToastNotifier();
                    notifier.Show(notification);
                }
            }
            
        }
    }
}
