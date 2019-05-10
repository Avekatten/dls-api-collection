using APICollection.Helpers;
using LeaderboardAPI_DLS.Models;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LeaderboardAPI_DLS.Controllers
{
    public class LeaderboardController : ApiController
    {
        // GET: api/Leaderboard
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Leaderboard/5
        public string Get(string id)
        {
            Leaderboard leaderboard = new Leaderboard();

            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                List<Score> gameScores = session
                    .Query<Score>()
                    .Where(x => x.GameID.Equals(id))
                    .ToList();                               // Load the Product and start tracking

                leaderboard.Scores = gameScores;

                // session.set(user, username, password);
                //
            }
            //List<Score> sortedTopTen = leaderboard.Scores.Sort(Score.;
            leaderboard.Scores.OrderBy(o => o.HighScore).ToList();
            
            return leaderboard.Scores.GetRange(0, 10).ToString();
            
        }

        // POST: api/Leaderboard
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Leaderboard/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Leaderboard/5
        public void Delete(int id)
        {
        }
    }
}
