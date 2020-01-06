using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainOnline.DTOs
{
    public class ExecuteTransactionDTO
    {
        public string Sender_publicKey { get; set; }
        public string Recepient_publicKey { get; set; }
        public long Ether_Amount { get; set; }
    }
}
