using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APICollection.Models
{
    public class User
    {
        public string UserID   { get; set; }
        public string Username { get; set; }
        public string UserMail { get; set; }
        public string Password { get; set; }
        public List<string> OwnedGames { get; set; }
    }
}