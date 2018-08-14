using GamerSky.Models;
using GamerSky.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Test
{
    [TestClass]
    public class ApiTest
    {
        private ApiService apiService = ApiService.Instance;

        [TestMethod]
        public async void GetAllChannel()
        {
            List<Channel> channels = await apiService.GetChannelList();
            Assert.IsNotNull(channels);
        }
    }
}
