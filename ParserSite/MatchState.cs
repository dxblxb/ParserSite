using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserSite
{
    class MatchState
    {
        public string Date = "";
        public string CommandHome = "";
        public string CommandAway = "";
        public string TotalFirstTimeHome = "0";
        public string TotalFirstTimeAway = "0";
        public string MatchScoreHome = "0";
        public string MatchScoreAway = "0";
        public string TrainerHome = "";
        public string TranerAway = "";
        public string TimeFirstTime = "45";
        public string Stadion = "";
        public string Orbitr = "";
        public List<string> HomePlayers = new List<string>();
        public List<string> AwayPlayers = new List<string>();
        public List<string[]> ReplacePlayers = new List<string[]>();
        public List<string[]> YCard = new List<string[]>();
        public List<string[]> RCard = new List<string[]>();
        public List<string[]> Ball = new List<string[]>();
        public List<string[]> AutoBall = new List<string[]>();
        public List<string[]> Stats = new List<string[]>();
    }
}
