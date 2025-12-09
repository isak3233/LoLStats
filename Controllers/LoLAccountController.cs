using LoLApi.Db;
using LoLApi.LoL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLApi.Controllers
{
    [ApiController]
    [Route("api/lolaccount")]
    public class LoLAccountController : ControllerBase
    {
        private readonly LoLService _lolService;

        public LoLAccountController(LoLService lolService)
        {
            _lolService = lolService;
        }

        [HttpGet("{LoLName}")]
        public async Task<IActionResult> GetSummoner(string lolName, [FromQuery] string region, [FromQuery] bool updateProfile)
        {
            LoLAccount? lolAccount = await _lolService.GetLoLAccount(lolName, updateProfile);
            if (lolAccount == null) return NotFound();
            SummonerAccount ? summonerAccount = await _lolService.GetSummoner(lolAccount.Puuid, region, updateProfile);
            if(summonerAccount == null) return NotFound();

            return Ok(summonerAccount);
        }
    }
}
