using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LoLApi.Db
{
    public class LoLMatch
    {
        [Key]
        public string Puuid { get; set; }
        public string MatchIds { get; set; }
    }
}
