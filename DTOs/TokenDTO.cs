using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainOnline.DTOs
{
    public class TokenDTO
    {
        [JsonProperty("tokenString")]
        public string tokenString { get; set; }
    }
}
