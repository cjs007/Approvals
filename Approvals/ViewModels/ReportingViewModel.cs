using Approvals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Approvals.ViewModels
{
    public class ReportingViewModel
    {
        public IEnumerable<PaymentRequest> PaymentRequests { get; set; }
        public PaymentRequest PaymentRequest { get; set; }
        public List<Client> Clients { get; set; }
        public ApplicationUser User { get; set; }
    }
}