using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainOnline.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Sender_username { get; set; }
        public string Recepient_username { get; set; }

        public string Sender_publicKey { get; set; }
        public string Recepient_publicKey { get; set; }

        public long Credit_Amount { get; set; }
        public long Ether_Amount { get; set; }

        public DateTime TransactionDateTime { get; set; }
    }
}
