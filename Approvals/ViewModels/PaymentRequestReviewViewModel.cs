using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Approvals.Models;

namespace Approvals.ViewModels
{
    public class PaymentRequestReviewViewModel
    {
        public PaymentRequest PaymentRequest { get; set; }
        public ApplicationUser User { get; set; }
    }
}