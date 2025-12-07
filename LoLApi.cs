using Azure;
using LoLApi.Db;
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
    internal class LoLApi
    {
        private HttpClient client;
        private JsonSerializerOptions options;
        public LoLApi()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Riot-Token", config["X-Riot-Token"]);
            options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<LoLAccount?> SearchForLoLAccount(string gameName, string tagLine)
        {
            string url = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); 

                string responseBody = await response.Content.ReadAsStringAsync();
                LoLAccount lolAccount = JsonSerializer.Deserialize<LoLAccount>(responseBody,options);
                return lolAccount;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            return null;
        }
        public async Task<SummonerAccount?> SearchForSummonerAccount(string puuid, string server)
        {
            string url = $"https://{server}.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                SummonerAccount summonerAccount = JsonSerializer.Deserialize<SummonerAccount>(responseBody, options);
                summonerAccount.Region = server;
                return summonerAccount;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            return null;
        }
        public async Task<string[]> GetLoLMatches(string puuid, string? type = null, int start = 0, int amountOfMatches = 20)
        {
            type = type == null ? "" : $"type={type}&";
            string url = $"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?{type}start={start}&count={amountOfMatches}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                string[] latestMatches = JsonSerializer.Deserialize<string[]>(responseBody, options);
                return latestMatches;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            return null;
        }
        public async Task<RankedInfo[]> GetRankedInfo(string puuid)
        {
            string url = $"https://euw1.api.riotgames.com/lol/league/v4/entries/by-puuid/{puuid}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                RankedInfo[] rankedInfo = JsonSerializer.Deserialize<RankedInfo[]>(responseBody, options);
                return rankedInfo;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            return null;
        }
    }
}
