using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [MaxLength(100)]
        [Index(IsUnique = true)]
        [Required]
        public string Username { get; set; }

        [MaxLength(100)]
        [Index(IsUnique = true)]
        [Required]
        public string Email { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(500)]
        public string Md5HashOfPassword { get; set; }

        [DefaultValue(false)]
        public bool LdapEnabled { get; set; }

        [DefaultValue(false)]
        public bool UserAdministrator { get; set; }

        [DefaultValue(false)]
        public bool FleetAdministrator { get; set; }

        [DefaultValue(false)]
        public bool BrandingAdministrator { get; set; }
    }
}
