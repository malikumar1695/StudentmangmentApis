using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMangment.Common.Data.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string FirstName { get; set; }
    }
}
