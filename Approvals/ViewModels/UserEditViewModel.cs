using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Approvals.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Approvals.ViewModels
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ApplicationUser User { get; set; }
        public IdentityRole Roles { get; set; }
        public virtual ICollection<AssignedClientData> Clients { get; set; }
        public string UserId { get; set; }

        public UserEditViewModel()
        {
            
        }

        public class AssignedClientData
        {
            public int ClientId { get; set; }
            public string ClientAcronym { get; set; }
            public bool Assigned { get; set; }
        }
    }
}