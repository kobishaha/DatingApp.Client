using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Client.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        [JsonIgnore, IgnoreDataMember]
        public override string? PasswordHash
        {
            get => base.PasswordHash;
            set => base.PasswordHash = value;
        }


        [NotMapped]
        public string Password { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }

        [JsonIgnore, IgnoreDataMember, NotMapped]
        public string Name
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }

        public ICollection<ApplicationRole>? Roles { get; set; }

        public int? TenantId { get; set; }

        [ForeignKey("TenantId")]
        public ApplicationTenant ApplicationTenant { get; set; }
    }
}