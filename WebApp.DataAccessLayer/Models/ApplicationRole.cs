using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApp.DataAccessLayer.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}