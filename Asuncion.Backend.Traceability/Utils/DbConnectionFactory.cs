using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Asuncion.Backend.Traceability.Utils
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _config;
        public DbConnectionFactory(IConfiguration config) => _config = config;

        public IDbConnection Create()
        {
            var cs = _config.GetConnectionString("Default");
            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException("ConnectionStrings:Default no configurado.");
            return new NpgsqlConnection(cs);
        }
    }
}
