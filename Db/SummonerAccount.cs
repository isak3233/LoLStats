using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace LoLApi.Db
{
    internal class SummonerAccount
    {
        [Key]
        public string Puuid { get; set; }
        public int ProfileIconId { get; set; }
        public int SummonerLevel { get; set; }
        public long RevisionDate { get; set; }
        [JsonIgnore]
        public string Region {  get; set; }
    }
}
