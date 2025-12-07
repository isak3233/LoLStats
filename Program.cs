namespace LoLApi
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Db;
    using System.Text.Json;

    class Program
    {
        //string[] lolServer = { "EUW1", "NA1", "EUN1", "KR", "BR1" , "JP1" , "RU" , "OC1" , "TR1" , "LA1" , "LA2", "SG2" , "TW2" , "VN2" , "ME1" };
        // info -> participants -> teamId
        static async Task Main()
        {
            LoLApi lolApi = new LoLApi();
            using (var db = new AppDbContext())
            {
                var me = db.SummonerAccounts.ToList()[0];
                string[]? matchIds = LoLDb.GetLatestMatches(me.Puuid);
                if (matchIds == null) return;
                foreach(var matchId in matchIds)
                {
                    Console.WriteLine(matchId);
                }


            }
            
            //Console.Write("Skriv ditt league namn: ");
            //string gameName = Console.ReadLine();
            //Console.Write("Skriv din league tag: ");
            //string tagLine = Console.ReadLine();
            //for(int i = 0; i < lolServer.Length; i++)
            //{
            //    Console.WriteLine($"{i+1}: {lolServer[i]}");
            //}
            //Console.Write("Vilken server är ditt konto regsterat i?: ");
            //int serverIndex = int.Parse(Console.ReadLine()) - 1;


            //LoLAccount? searchedLoLAccount = await lolApi.SearchForLoLAccount(gameName, tagLine);
            //if(searchedLoLAccount == null)
            //{
            //    Console.WriteLine("Kontot hittades inte");
            //    return;
            //}
            //SummonerAccount? summonerAccount = await lolApi.SearchForSummonerAccount(searchedLoLAccount.Puuid, lolServer[serverIndex]);
            //if(summonerAccount == null)
            //{
            //    Console.WriteLine("Summoner kontot hittades inte");
            //    return;
            //}
            //Console.WriteLine(summonerAccount.SummonerLevel);
            //Console.WriteLine(summonerAccount.Region);
            //LoLDb.SaveLoLAccount(searchedLoLAccount);
            //LoLDb.SaveSummonerAccount(summonerAccount);
            //Console.ReadLine();





        }
    }

}
