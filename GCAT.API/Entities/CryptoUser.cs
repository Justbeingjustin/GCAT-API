using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCAT.API.Entities
{
    public class CryptoUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}