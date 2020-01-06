using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainOnline.Models
{
    public class TransactionViewModel
    {
        public LinkedList<Transaction> transactions = new LinkedList<Transaction>();
        public string sender_key { get; set; }
        public string recepient_key { get; set; }
        public string amount { get; set; }
        public string result { get; set; }
    }
}
