using LoLApi.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLApi
{
    internal class LoLDb
    {
        static public void SaveLoLAccount(LoLAccount account)
        {
            using (var db = new AppDbContext())
            {
                var dbLoLAccount = (from la in db.LoLAccounts
                                    where la.Puuid == account.Puuid
                                    select la).SingleOrDefault();
                if (dbLoLAccount == null)
                {
                    db.LoLAccounts.Add(account);
                }
                else
                {
                    dbLoLAccount = account;
                }
                db.SaveChanges();
            }
        }
        static public void SaveSummonerAccount(SummonerAccount account)
        {
            using (var db = new AppDbContext())
            {
                var dbSummonerAccount = (from sb in db.SummonerAccounts
                                    where sb.Puuid == account.Puuid
                                    select sb).SingleOrDefault();
                if (dbSummonerAccount == null)
                {
                    db.SummonerAccounts.Add(account);
                }
                else
                {
                    dbSummonerAccount = account;
                }
                db.SaveChanges();
            }
        }
        static public void SaveRankedInfo(RankedInfo rankInfo)
        {
            using (var db = new AppDbContext())
            {

                var dbRankedInfo = (from ri in db.RankedInfo
                                    where ri.Puuid == rankInfo.Puuid && ri.QueueType == rankInfo.QueueType
                                    select ri).SingleOrDefault();


                if (dbRankedInfo == null)
                {
                    db.RankedInfo.Add(rankInfo);
                }
                else 
                {
                    dbRankedInfo = rankInfo;
                }

                db.SaveChanges();
            }
        }
        static public void SaveLatestMatches(string puuid, string[] matchIds)
        {
            using (var db = new AppDbContext())
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
                var dbLatestMatches = (from lm in db.LoLMatch
                                       where lm.Puuid == puuid
                                       select lm).SingleOrDefault();
                if(dbLatestMatches == null)
                {
                    db.LoLMatch.Add(match);
                }
                else
                {
                    dbLatestMatches = match;
                }
                db.SaveChanges();
            }
        }
        static public string[]? GetLatestMatches(string puuid)
        {
            using (var db = new AppDbContext())
            {
                var dbLatestMatches = (from lm in db.LoLMatch
                                       where lm.Puuid == puuid
                                       select lm).SingleOrDefault();
                return dbLatestMatches.MatchIds.Split(',');
            }
        }




    }
}
