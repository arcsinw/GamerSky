using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Requests
{
    public class GameDetailRequestV4
    {
        public string gameId;

        public string gameModeFieldNames = "Title,GameType,GameMake,GameAuthor,ClubId,GameDir,Activity,Position,EnTitle,Intro,AllTimeT,AllTime,SteamVideos,SteamImages,Peizhi,DeputyNodeId,PCTime,PCTimeT,OfficialChinese,IsFree,OnLine,SteamPrice,SteamInitial,SteamFinal,DiscountPercent,DiscountText,PS4Time,PS4TimeT,Ps4Chinese,PS4HuiMian,XboxOneTime,XboxOneTimeT,XboxChinese,XboxHuiMian,NintendoSwitchTime,NintendoSwitchTimeT,NsChinese,iOSTime,AndroidTime,PS3Time,Xbox360Time,WiiUTime,DSTime,PSVitaTime";

        public string gameRelatedFieldNames = "gsScore,wantplayCount,gameTag,isMarket,playCount,expectCount,defaultPicUrl";

        public string gameTagsCount = "20";
    }
}
