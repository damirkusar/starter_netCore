﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}