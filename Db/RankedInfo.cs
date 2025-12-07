using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLApi.Db
{
    internal class RankedInfo
    {
        [Key]
        public int Id { get; set; }
        public string Puuid { get; set; }
        public string LeagueId { get; set; }
        
        public string QueueType { get; set; }
        public string Tier { get; set; }
        public string Rank { get; set; }
        public int LeaguePoints { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

    }
}
