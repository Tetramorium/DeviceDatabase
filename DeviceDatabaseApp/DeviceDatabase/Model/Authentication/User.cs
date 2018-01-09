using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceDatabase.Model.Authentication
{
    public class User
    {
        public User(string username, string email, List<string> roles)
        {
            Username = username;
            Email = email;
            Roles = roles;
        }
        public string Username
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public List<string> Roles
        {
            get;
            set;
        }
    }
}
