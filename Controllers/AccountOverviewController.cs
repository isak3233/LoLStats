using LoLApi.Db;
using LoLApi.LoL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (!lolName.Contains('#') || region == "null" || region == null) return BadRequest();
            
            LoLAccount ? lolAccount = await _lolService.GetLoLAccount(lolName, updateProfile);
            if (lolAccount == null) return NotFound();


            

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
                region = summonerAccount.Region,
                profileIconId = summonerAccount.ProfileIconId,
                SummonerLevel = summonerAccount.SummonerLevel,
                RevisionDate = summonerAccount.RevisionDate,
                RanksInfo = ranksInfo,

            };
            return Ok(result);
        }
    }
}
