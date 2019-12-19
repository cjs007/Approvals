using Approvals.Models;
using Approvals.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Approvals.Controllers
{
    public class ReportingController : Controller
    {

        private ApplicationDbContext _context;

        public ReportingController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Reporting
        public ActionResult Download()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            var viewModel = new ReportingViewModel
            {
                PaymentRequests = _context.PaymentRequests.ToList(),
                Clients = _context.Clients.ToList(),
                User = currentUser
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ExportToExcel(PaymentRequest paymentRequest)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            var clientId = paymentRequest.ClientId;
            Client client;
            string searchClient;
            string searchStatus = paymentRequest.Status;
            

            if(clientId != 0)
            { 
                client = _context.Clients.Find(clientId);
                searchClient = client.ClientNumber;
            }
            else
            {
                searchClient = null;
            }
            
            if (currentUser.IsOwner)
            {   

                var gv = new GridView();
                //gv.DataSource = _context.PaymentRequests.Include(m =>m.Client).ToList();

                var originalGv = (from e in _context.PaymentRequests
                                  select new
                                  {
                                      Client = e.Client.ClientNumber,
                                      Payee = e.Name,
                                      Address = e.AddressOne + " " + e.AddressTwo + " " + e.AddressThree,
                                      Amount = e.Amount,
                                      GL = e.GLLine,
                                      Status = e.Status,
                                      SubmitTime = e.SubmitDateTime,
                                      Submitter = e.Submitter,
                                      ApproveTime = e.ApproveDateTime,
                                      Approver = e.Approver,
                                  }).ToList();

                var clientGv = searchClient != null ? originalGv.Where(i => i.Client == searchClient) : originalGv;
                var statusGv= searchStatus != null ? clientGv.Where(i => i.Status == searchStatus) : clientGv;

                gv.DataSource = statusGv;
                //The above line is not working - cliendId is returning null for some reason.

                //IEnumerable<Thing> whateverEnumerable = yourThing;
                //whateverEnumerable = filter != null
                //    ? whateverEnumerable.Where(i => i.Property == filter)
                //    : whateverEnumerable;
                //var result = whateverEnumerable.ToList();


                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                var filename = DateTime.Today.ToString("ddMMyyyy") + ".xls";
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
                return View("Index");
            }

            return RedirectToAction("Access", "Home");
        }
    }
}