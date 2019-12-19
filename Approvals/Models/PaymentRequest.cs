using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Approvals.Models
{
    public class PaymentRequest
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Address Line 1")]
        [Required]
        public string AddressOne { get; set; }

        [Display(Name = "Address Line 2")]
        [Required]
        public string AddressTwo { get; set; }

        [Display(Name = "Address Line 3")]
        public string AddressThree { get; set; }

        [Required]
        public string Amount { get; set; }

        [Required]
        [Display(Name = "General Ledger Line")]
        public string GLLine { get; set; }

        public bool Approved { get; set; }

        public bool Paid { get; set; }

        public DateTime SubmitDateTime { get; set; }

        public string Submitter { get; set; }

        public DateTime? ApproveDateTime { get; set; }

        public string Approver { get; set; }

        public string Payor { get; set; }

        public string FilePath { get; set; }

        public Client Client { get; set; }

        [Required]
        [Display(Name = "Client Id")]
        public int ClientId { get; set; }

        public string Status { get; set; }

        [Display(Name = "Check Number")]
        public string CheckNumber { get; set; }

        [Display(Name = "Check Date")]
        public string PaidDate { get; set; }
    }
}