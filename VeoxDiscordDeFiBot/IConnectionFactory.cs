using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace VeoxDiscordDeFiBot
{
    public interface IConnectionFactory
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Creates the database connection.
        /// </summary>
        /// <returns>The connection to database.</returns>
        IDbConnection CreateConnection();
    }
}
