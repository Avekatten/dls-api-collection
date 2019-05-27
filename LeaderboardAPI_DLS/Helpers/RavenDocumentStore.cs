using Raven.Client.Documents;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LeaderboardAPI_DLS.Helpers
{
    public class RavenDocumentStore
    {

        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            string ravenDBIP = ConfigurationManager.AppSettings["RavenDBEndPoint"];
            IDocumentStore store = new DocumentStore()
            {
                Urls = new[] { ravenDBIP },
                Database = "Scores"
            }.Initialize();
            
            return store;
        }
    }
    
}