using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APICollection.Models
{
    public class User
    {
        public string Username { get; set; }
        public string UserMail { get; set; }
        public string Password { get; set; }
        List<string> OwnedGames { get; set; }
    }
}