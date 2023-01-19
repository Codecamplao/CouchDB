using System;
using System.Collections.Generic;
using System.Text;

namespace CouchDBService.DTO
{
    public class ReplicateRequest
    {
        public ReplicateRequest(string scheme, string srvAddr, string dbName, string username, string password, int? port = 5984)
        {
            DbName = dbName;
            ServerAddr = $"{scheme}://{username}:{password}@{srvAddr}:{port}";
        }
        /// <summary>
        /// http://username@password:target:port
        /// </summary>
        public string ServerAddr { get; set; }
        public bool? Continuous { get; set; }
        public bool? CreateTarget { get; set; }
        /// <summary>
        /// Target database name
        /// </summary>
        public string DbName { get; set; }
    }
}
