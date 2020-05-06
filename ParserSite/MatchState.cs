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
        public string MatchScoreHome = "";
        public string MatchScoreAway = "";
        public List<string> HomePlayers = new List<string>();
        public List<string> AwayPlayers = new List<string>();
        public List<string[]> ReplacePlayers = new List<string[]>();
    }
}
