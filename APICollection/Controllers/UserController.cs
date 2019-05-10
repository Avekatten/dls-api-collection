using APICollection.Helpers;
using APICollection.Models;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APICollection.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        // GET: api/User
        public User Get()
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                User user = session.Load<User>("1");                               // Load the Product and start tracking



                session.SaveChanges();
                return user;
            }
        }

        // GET: api/User/5
        public User Get(string id)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                User user = session.Load<User>("users/" + id + "-A");                               // Load the Product and start tracking

               

                session.SaveChanges();
                return user;
            }
            
            
        }

        // POST: api/User
        public void Post([FromBody]User value)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {

                session.Store(value);    
                
                session.SaveChanges();                             
            }
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]User value)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                User user = session.Load<User>("users/" + id + "-A");                               // Load the Product and start tracking

                session.Delete(user);

                session.SaveChanges();
            }
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                User user = session.Load<User>("users/" + id + "-A");                               // Load the Product and start tracking

                session.Delete(user);

                session.SaveChanges();
            }
        }

        [HttpPost]
        [Route("Login")]
        public bool Login([FromBody]User user)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())
            {
                List<User> matchingUser = session
                    .Query<User>()                               
                    .Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password))
                    .ToList();

                if (matchingUser.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
