using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLApi.Db
{
    internal class LoLAccount
    {
        [Key]
        public string Puuid { get; set; }
        public string? GameName { get; set; }
        public string? TagLine { get; set; }
    }
}
