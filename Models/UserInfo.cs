using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainOnline.Models
{
    public class UserInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int Credit { get; set; }

        public Wallet[] Wallets { get; set; }
    }
}
