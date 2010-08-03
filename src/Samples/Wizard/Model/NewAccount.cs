using System.ComponentModel;

namespace Wizard.Model
{
    public class NewAccount
    {
        [DisplayName("Full name")]
        public string FullName { get; set; }

        [DisplayName("Email address")]
        public string EmailAddress { get; set; }
        
        public string Password { get; set; }
        
        public ServerSettings Server { get; set; }
    }
}