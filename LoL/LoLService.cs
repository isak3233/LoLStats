using LoLApi.Db;
using LoLApi.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLApi.LoL
{
    public class LoLService
    {
        private readonly LoLApi _lolApi;
        private readonly AppDbContext _db;
        private readonly LoLDb _lolDb;
        public LoLService(AppDbContext db)
        {
            _db = db;
            _lolDb = new LoLDb(_db);
            _lolApi = new LoLApi();
            
        }
        public async Task<LoLAccount?> GetLoLAccount(string lolName)
        {
            string gameName = lolName.Split('#')[0];
            string tagLine = lolName.Split('#')[1];
            LoLAccount? dbLoLAccount = _lolDb.GetLoLAccount(gameName, tagLine);
            LoLAccount? lolAccount = dbLoLAccount ?? await _lolApi.SearchForLoLAccount(gameName, tagLine);
            if (lolAccount == null) return null;
            _lolDb.SaveLoLAccount(lolAccount);
            return lolAccount;
        }
        public async Task<SummonerAccount?> GetSummoner(string puuid, string region)
        {

            SummonerAccount? dbSummonerAccount = _lolDb.GetSummonerAccount(puuid);
            SummonerAccount? summonerAccount = dbSummonerAccount ?? await _lolApi.SearchForSummonerAccount(puuid, region);
            if (summonerAccount == null) return null;
            _lolDb.SaveSummonerAccount(summonerAccount);

            return summonerAccount;
        }
        public async Task<RankedInfo[]?> GetRankedInfo(string puuid)
        {
            RankedInfo[] dbRankedInfo = _lolDb.GetRankedInfo(puuid);
            
            RankedInfo[]? ranksInfo = dbRankedInfo.Length > 0 ? dbRankedInfo : await _lolApi.GetRankedInfo(puuid);
            foreach (var rankedInfo in ranksInfo)
            {
                _lolDb.SaveRankedInfo(rankedInfo);
            }


            return ranksInfo;
        }

    }
}
