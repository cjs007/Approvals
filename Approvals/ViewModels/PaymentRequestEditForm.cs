using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Approvals.Models;

namespace Approvals.ViewModels
{
    public class PaymentRequestEditForm
    {
        public PaymentRequest PaymentRequest { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Address Line 1")]
        public string AddressOne { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressTwo { get; set; }

        [Display(Name = "Address Line 3")]
        public string AddressThree { get; set; }

        public string Amount { get; set; }

        [Display(Name = "General Ledger Line")]
        public string GLLine { get; set; }

        public bool Approved { get; set; }

        public bool Paid { get; set; }

        public DateTime SubmitDateTime { get; set; }

        public string FilePath { get; set; }

        public string Client { get; set; }

        public string Status { get; set; }

        [Display(Name = "Check Number")]
        public string CheckNumber { get; set; }

        [Display(Name = "Check Date")]
        public string PaidDate { get; set; }

        public PaymentRequestEditForm(PaymentRequest paymentRequest)
        {
            Id = paymentRequest.Id;
            CheckNumber = paymentRequest.CheckNumber;
            PaidDate = paymentRequest.PaidDate;
        }
    }
}