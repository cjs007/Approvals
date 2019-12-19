using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Approvals.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string ClientNumber { get; set; }

        public string ClientAcronym { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

    }
}