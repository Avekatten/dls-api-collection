﻿using APICollection.Helpers;
using APICollection.Models;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using UserAPI_DLS.Helpers;

namespace APICollection.Controllers
{

    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {

        public bool Get()
        {
            return false;
        }

        // Get specific user
        // GET: api/User/5
        public User Get(string id)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                EnsureDatabaseExists.DatabaseExists(RavenDocumentStore.Store, "Users");
                User user = session.Load<User>("users/" + id + "-A");                               // Load the Product and start tracking

                session.SaveChanges();
                return user;
            }
        }

        // Creates a new user
        // POST: api/User
        public User Post([FromBody]User value)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {

                session.Store(value);


                value.UserID = session.Advanced.GetDocumentId(value);

                value.UserID = Regex.Replace(value.UserID, "[^.0-9]", "");


                session.SaveChanges();

                // Needs check for valid credentials/data
            }

            return value;
        }

        // Updates a user with a given id
        // PUT: api/User/5
        public void Put(int id, [FromBody]User value)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                EnsureDatabaseExists.DatabaseExists(RavenDocumentStore.Store, "Users");
                User user = session.Load<User>("users/" + id + "-A");                               // Load the Product and start tracking

                //session.Delete(user);
                user.Username = value.Username;
                user.Password = value.Password;
                user.UserMail = value.UserMail;
                user.OwnedGames = value.OwnedGames;


                // session.set(user, username, password);
                session.SaveChanges();
            }
        }

        // Deletes a user with a given id
        // DELETE: api/User/5
        public void Delete(int id)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())  // Open a session for a default 'Database'
            {
                EnsureDatabaseExists.DatabaseExists(RavenDocumentStore.Store, "Users");
                User user = session.Load<User>("users/" + id + "-A");                               // Load the Product and start tracking

                session.Delete(user);

                session.SaveChanges();
            }
        }



        // Queries for the username and password in the DB, returns true/false
        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login(HttpRequestMessage request, [FromBody]User user)
        {
            using (IDocumentSession session = RavenDocumentStore.Store.OpenSession())
            {
                EnsureDatabaseExists.DatabaseExists(RavenDocumentStore.Store, "Users");
                List<User> matchingUser = session
                    .Query<User>()
                    .Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password))
                    .ToList();

                if (matchingUser.Count > 0)
                {
                    return request.CreateResponse(HttpStatusCode.OK, matchingUser);
                }

                return request.CreateResponse(HttpStatusCode.Unauthorized, matchingUser);
            }
        }
    }
}
