using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.Extensions;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserAPI_DLS.Helpers
{
    public static class EnsureDatabaseExists
    {
        private static bool createDatabaseIfNotExists;

        public static void DatabaseExists(this IDocumentStore documentStore, string databaseName)
        {
            databaseName = databaseName ?? documentStore.Database;

            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(databaseName));

            try
            {
                documentStore.Maintenance.ForDatabase(databaseName).Send(new GetStatisticsOperation());
            }
            catch (DatabaseDoesNotExistException)
            {
                if (createDatabaseIfNotExists == false)
                {
                    throw;
                }

                try
                {
                    documentStore.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(databaseName)));
                }
                catch (ConcurrencyException)
                {
                    // The database was already created before calling CreateDatabaseOperation
                }
            }
        }
    }
}