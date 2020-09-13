using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Administrator
    {
        public string AdministratorId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNum { get; set; }
        public string EmailAddress { get; set; }
        public string Nickname { get; set; }
    }
}
