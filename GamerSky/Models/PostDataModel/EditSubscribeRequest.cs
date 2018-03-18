using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.PostDataModel
{
    /// <summary>
    /// 对订阅进行操作
    /// </summary>
    public class EditSubscribeRequest
    {
        /// <summary>
        /// minus  |  add
        /// </summary>
        public string operate;
        public string subscribeId;
    }
}
