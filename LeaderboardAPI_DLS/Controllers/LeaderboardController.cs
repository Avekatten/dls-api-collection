using LeaderboardAPI_DLS.Helpers;
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
    [RoutePrefix("api/Leaderboard")]
    public class LeaderboardController : ApiController
    {
        // GET: api/Leaderboard
        public Score[] Get()
        {
            return null;
        }

        // Get all scores within a game
        // GET: api/Leaderboard/5
        public Score[] Get(string id) // Changed return type from string to an array of scores.
        {
            Leaderboard leaderboard = new Leaderboard();

            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                EnsureDatabaseExists.DatabaseExists(RavenDocumentStore.Store, "Scores");
                List<Score> gameScores = session
                    .Query<Score>()
                    .Where(x => x.GameID.Equals(id))
                    .ToList();                               // Load the Product and start tracking

                leaderboard.Scores = gameScores;

                // session.set(user, username, password);
                //
            }
            //List<Score> sortedTopTen = leaderboard.Scores.Sort(Score.;
            List<Score> temp = leaderboard.Scores.OrderByDescending(o => o.HighScore).ToList();

            return temp.GetRange(0, leaderboard.Scores.Count).ToArray();
            
        }

        // Get players specific score
        // GET: api/Leaderboard/5
        [HttpGet]
        [Route("{gameID}/{id}")]
        public Score GetPlayerScore(string gameID, string id) // Changed return type from string to an array of scores.
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                EnsureDatabaseExists.DatabaseExists(RavenDocumentStore.Store, "Scores");
                List<Score> gameScore = session
                    .Query<Score>()
                    .Where(x => x.GameID.Equals(gameID) && x.UserID.Equals(id))
                    .ToList();                               // Load the Product and start tracking

                if (gameScore.Count > 0)
                    return gameScore[0];
                else
                    return null;
            }
            //List<Score> sortedTopTen = leaderboard.Scores.Sort(Score.;



        }

        [HttpPost]
        [Route("")]
        // POST: api/Leaderboard
        public void Post([FromBody]Score value)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                EnsureDatabaseExists.DatabaseExists(RavenDocumentStore.Store, "Scores");
                session.Store(value);

                session.SaveChanges();

                // Needs check for valid credentials/data
            }
        }

        // PUT: api/Leaderboard/5
        public void Put(string gameID, string userID, [FromBody]float newScore)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                EnsureDatabaseExists.DatabaseExists(RavenDocumentStore.Store, "Scores");
                Score gameScore = (Score) session
                    .Query<Score>()
                    .Where(x => x.UserID.Equals(userID) && x.GameID.Equals(gameID));                               
                gameScore.HighScore = newScore;
            }
        }
    }
}
