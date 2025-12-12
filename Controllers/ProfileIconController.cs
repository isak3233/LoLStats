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
    [Route("api/profileicon")]
    public class ProfileIconController : ControllerBase
    {
        private readonly LoLService _lolService;

        public ProfileIconController(LoLService lolService)
        {
            _lolService = lolService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileIcon(int id)
        {
            byte[]? image = _lolService.GetImageFile(id);
            if(image == null) return NotFound();

            return File(image, "image/jpeg");
        }
    }
}
