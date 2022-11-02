using Microsoft.Extensions.Configuration;
using System;

namespace DataAccess.Sql
{
    public static class Connections
    {
        /// <summary>
        /// 
        /// </summary>
        public static string StoreDatabase(IConfiguration configuration)
        {

            var connectionString = configuration["Database:Store:StringConnection"].ToString();
            var password = configuration["Database:Store:Password"].ToString();
            return connectionString + ";Pwd=" + password;
        }
    }
}
