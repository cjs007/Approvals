using Approvals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Approvals.ViewModels;

namespace Approvals.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext _context;

        public ClientsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var clients = _context.Clients.ToList();
            return View(clients);
        }

        public ActionResult New()
        {
            var viewModel = new NewClientViewModel
            {

            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();

            return RedirectToAction("Index", "Clients");
        }

        public ActionResult Delete(int id)
        {
            var client = _context.Clients.Single(m => m.Id == id);

            _context.Clients.Remove(client);
            _context.SaveChanges();
            return RedirectToAction("Index", "Clients");
        }
    }
}