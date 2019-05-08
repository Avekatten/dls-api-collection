using APICollection.Helpers;
using APICollection.Models;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;

namespace APICollection.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public User Get()
        {
            return new User();
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
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
