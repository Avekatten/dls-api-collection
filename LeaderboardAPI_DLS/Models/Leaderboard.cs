using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaderboardAPI_DLS.Models
{
    public class Leaderboard
    {
        public string GameID { get; set; }

        public List<Score> Scores { get; set; }
    }
}