using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLApi.JsonModels
{
    public class Participant
    {
        public int TeamId { get; set; }
        public string ChampionName { get; set; }
        public string RiotIdGameName { get; set; }
    }
    public class Team
    {
        public int TeamId { get; set; }
        public bool Win { get; set; }
    }

    public class Info
    {
        public List<Participant> Participants { get; set; }
        public List<Team> Teams { get; set; }
    }

    public class MatchInfoRoot
    {
        public Info Info { get; set; }
    }
}
