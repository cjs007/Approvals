using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Approvals.Models;

namespace Approvals.ViewModels
{
    public class NewPaymentRequestViewModel
    {
        public PaymentRequest PaymentRequest { get; set; }
        public List<Client> Clients { get; set; }
        public ApplicationUser User { get; set; }
    }
}