using LoLApi.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLApi.LoL
{
    public class LoLDb
    {
        private readonly AppDbContext _db;
        public LoLDb(AppDbContext db)
        {
            _db = db;
        }

        public void SaveLoLAccount(LoLAccount account)
        {
                

            LoLAccount? dbLoLAccount = _db.LoLAccounts.Find(account.Puuid);
            
            
            if (dbLoLAccount == null)
            {
                _db.LoLAccounts.Add(account);
            }
            else
            {

                _db.Entry(dbLoLAccount).CurrentValues.SetValues(account);
            }
            _db.SaveChanges();

        }
        public void SaveSummonerAccount(SummonerAccount account)
        {

                
            SummonerAccount? dbSummonerAccount = _db.SummonerAccounts.Find(account.Puuid);

            if(dbSummonerAccount == null)
            {
                _db.SummonerAccounts.Add(account);
            }
            else
            {
                _db.Entry(dbSummonerAccount).CurrentValues.SetValues(account);
            }

            _db.SaveChanges();
            
        }
        public void SaveRankedInfo(RankedInfo rankInfo)
        {


                var dbRankedInfo = (from ri in _db.RankedInfo
                                    where ri.Puuid == rankInfo.Puuid && ri.QueueType == rankInfo.QueueType
                                    select ri).SingleOrDefault();
                if (dbRankedInfo == null)
                {
                    _db.RankedInfo.Add(rankInfo);
                }
                else 
                {
                    dbRankedInfo.LeagueId = rankInfo.LeagueId;
                    dbRankedInfo.Tier = rankInfo.Tier;
                    dbRankedInfo.Rank = rankInfo.Rank;
                    dbRankedInfo.LeaguePoints = rankInfo.LeaguePoints;
                    dbRankedInfo.Wins = rankInfo.Wins;
                    dbRankedInfo.Losses = rankInfo.Losses;


            }

                _db.SaveChanges();

        }
        public void SaveLatestMatches(string puuid, string[] matchIds)
        {

            
                string matchIdsString = "";
                foreach(var matchId in matchIds)
                {
                    matchIdsString += matchId + ",";
                }
                LoLMatch match = new LoLMatch()
                {
                    Puuid = puuid,
                    MatchIds = matchIdsString
                };
                var dbLatestMatches = (from lm in _db.LoLMatch
                                       where lm.Puuid == puuid
                                       select lm).SingleOrDefault();
                if(dbLatestMatches == null)
                {
                    _db.LoLMatch.Add(match);
                }
                else
                {
                    _db.Entry(dbLatestMatches).CurrentValues.SetValues(match);
                }
                _db.SaveChanges();
            
        }
        public string[]? GetLatestMatches(string puuid)
        {

            LoLMatch? dbLatestMatches = _db.LoLMatch.Find(puuid);
                return dbLatestMatches.MatchIds.Split(',');
            
        }
        public LoLAccount? GetLoLAccount(string gameName, string tagLine)
        {
            LoLAccount? dbLoLAccount = (from la in _db.LoLAccounts
                                        where la.GameName == gameName && la.TagLine == tagLine
                                        select la).SingleOrDefault();
            return dbLoLAccount;
        }
        public SummonerAccount? GetSummonerAccount(string puuid)
        {
            SummonerAccount? summonerAccount = _db.SummonerAccounts.Find(puuid);
            if (summonerAccount == null) return null;
            return summonerAccount;
        }
        public RankedInfo[] GetRankedInfo(string puuid)
        {
            var dbRankedInfo = _db.RankedInfo
                                .Where(ri => ri.Puuid == puuid)
                                .ToArray();
            return dbRankedInfo;

        }




    }
}
