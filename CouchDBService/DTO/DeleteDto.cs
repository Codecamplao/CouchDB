using System;
using System.Collections.Generic;
using System.Text;

namespace CouchDBService.DTO
{
    public class DeleteDto
    {
        public string ID { get; set; }
        public string Rev { get; set; }
    }
}
