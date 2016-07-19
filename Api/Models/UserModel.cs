namespace Api.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public bool LdapEnabled { get; set; }

        public bool UserAdministrator { get; set; }

        public bool FleetAdministrator { get; set; }

        public bool BrandingAdministrator { get; set; }

        public string Token { get; set; }
    }
}