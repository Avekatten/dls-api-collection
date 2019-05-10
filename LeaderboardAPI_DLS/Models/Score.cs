using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaderboardAPI_DLS.Models
{
    public class Score
    {
        public String UserID { get; set; }
        public String GameID { get; set; }
        public float HighScore { get; set; }

        
    }
}