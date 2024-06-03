using System;
using System.Collections.Generic;
using Entity.Address;
using Microsoft.AspNetCore.Identity;


namespace Entity.UserApp
{
    public class userapp
    {
        public class UserApp : IdentityUser
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public List<Address.Address> Address { get; set; }
        }

    }
}
