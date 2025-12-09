using Microsoft.AspNetCore.Mvc;
using LoLApi.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoLApi.LoL;

namespace LoLApi.Controllers
{
    [ApiController]
    [Route("api/accountoverview")]
    public class AccountOverviewController : ControllerBase
    {
        private readonly LoLService _lolService;
        public AccountOverviewController(LoLService lolService)
        {
            _lolService = lolService;
        }
        [HttpGet("{lolName}")]
        public async Task<IActionResult> GetAccountOverview(string lolName, [FromQuery] string region, [FromQuery] bool updateProfile)
        {
       
            LoLAccount? lolAccount = await _lolService.GetLoLAccount(lolName, updateProfile);
            if (lolAccount == null) return NotFound();


            // Skulle kunna göra så dessa funktioner samtidigt om man ger dom nya dbcontext objekt
            SummonerAccount? summonerAccount = await _lolService.GetSummoner(lolAccount.Puuid, region, updateProfile);
            if (summonerAccount == null) return NotFound();
            RankedInfo[]? ranksInfo = await _lolService.GetRankedInfo(lolAccount.Puuid, updateProfile);

            var filteredRanks = ranksInfo.Select(r => new
            {
                r.QueueType,
                r.Tier,
                r.Rank,
                r.LeaguePoints,
                r.Wins,
                r.Losses
            }).ToArray();

            var result = new
            {
                puuid = lolAccount.Puuid,
                gameName = lolAccount.GameName,
                tagLine = lolAccount.TagLine,
                profileIconId = summonerAccount.ProfileIconId,
                SummonerLevel = summonerAccount.SummonerLevel,
                RevisionDate = summonerAccount.RevisionDate,
                RanksInfo = ranksInfo,

            };
            return Ok(result);
        }
    }
}
