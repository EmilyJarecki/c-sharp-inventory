using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory
{
    public class User
    {
        public string userId;
        public string password;

        public User(string user, string pwd)
        {
            this.userId = user;
            this.password = pwd;
        }

    }
}
