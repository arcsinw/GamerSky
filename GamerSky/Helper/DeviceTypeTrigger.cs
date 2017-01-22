using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace GamerSky.Helper
{
    /// <summary>
    /// 设备类型触发器 Desktop时触发
    /// </summary>
    public class DeviceTypeTrigger : StateTriggerBase
    {
        public enum DeviceType { Unknown = 0, Desktop = 1, Mobile = 2 };

        public DeviceType PlatformType
        {
            get { return (DeviceType)GetValue(PlatformTypeProperty); }
            set { SetValue(PlatformTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlatformType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlatformTypeProperty =
            DependencyProperty.Register("PlatformType", typeof(DeviceType), typeof(DeviceTypeTrigger), new PropertyMetadata(DeviceType.Unknown,OnDeviceTypePropertyChanged));

        private static void OnDeviceTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (DeviceTypeTrigger)d;
            var val = (DeviceType)e.NewValue;
            var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;

            if (qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Desktop")
                obj.SetActive(true);
        }
    }
}
