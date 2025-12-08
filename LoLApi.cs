using Azure;
using LoLApi.Db;
using LoLApi.JsonModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoLApi
{
    public class LoLApi
    {
        private HttpClient client;
        private JsonSerializerOptions options;
        public LoLApi()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Riot-Token", config["X-Riot-Token"]);
            options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<T?> SendGetAndDeserialize<T>(string url)
        {
            string? response = await SendGetRequest(url);
            if (response is null)
                return default;

            return JsonSerializer.Deserialize<T>(response, options);
        }
        private async Task<string?> SendGetRequest(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
        public async Task<LoLAccount?> SearchForLoLAccount(string gameName, string tagLine)
        {
            string url = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}";

            LoLAccount lolAccount = await SendGetAndDeserialize<LoLAccount>(url);
            return lolAccount;
            

        }
        public async Task<SummonerAccount?> SearchForSummonerAccount(string puuid, string server)
        {
            string url = $"https://{server}.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}";
            

            SummonerAccount summonerAccount = await SendGetAndDeserialize<SummonerAccount>(url);
            summonerAccount.Region = server;
            return summonerAccount;
            
        }
        public async Task<string[]> GetLoLMatches(string puuid, string? type = null, int start = 0, int amountOfMatches = 20)
        {
            type = type == null ? "" : $"type={type}&";
            string url = $"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?{type}start={start}&count={amountOfMatches}";

            string[] latestMatches = await SendGetAndDeserialize<string[]>(url);
            return latestMatches;
            
            
        }
        public async Task<MatchInfoRoot> GetMatchInfo(string matchId)
        {
            string url = $"https://europe.api.riotgames.com/lol/match/v5/matches/{matchId}";

            dynamic matchInfo = await SendGetAndDeserialize<MatchInfoRoot>(url);
            return matchInfo;
        }
        public async Task<RankedInfo[]?> GetRankedInfo(string puuid)
        {
            string url = $"https://euw1.api.riotgames.com/lol/league/v4/entries/by-puuid/{puuid}";

            RankedInfo[]? rankedInfo = await SendGetAndDeserialize<RankedInfo[]?>(url);
            if (rankedInfo == null) return null;
            return rankedInfo;
        }
    }
}
