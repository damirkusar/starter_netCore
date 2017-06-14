using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.DataAccessLayer.JsonConverter;

namespace WebApp.DataAccessLayer.Models
{
    [JsonConverter(typeof(ApplicationUserJsonConverter))]
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }

        [NotMapped] 
        public ICollection<string> AssignedRoles { get; set; }
        [NotMapped]
        public string GeneratedPassword { get; set; }

        [JsonIgnore]
        public override string ConcurrencyStamp { get; set; }
        [JsonIgnore]
        public override string PasswordHash { get; set; }
        [JsonIgnore]
        public override string SecurityStamp { get; set; }
        [JsonIgnore]
        public override string NormalizedEmail { get; set; }
        [JsonIgnore]
        public override string NormalizedUserName { get; set; }
        [JsonIgnore]
        public override bool LockoutEnabled { get; set; }
        [JsonIgnore]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [JsonIgnore]
        public override ICollection<IdentityUserLogin<Guid>> Logins { get; }
        [JsonIgnore]
        public override ICollection<IdentityUserRole<Guid>> Roles { get; }
        [JsonIgnore]
        public override ICollection<IdentityUserClaim<Guid>> Claims { get; }
        [JsonIgnore]
        public override int AccessFailedCount { get; set; }
    }
}