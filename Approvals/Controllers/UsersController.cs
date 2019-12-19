using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Approvals.Models;
using System.Collections.ObjectModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Approvals.ViewModels;
using System.Data.Entity;
using System.Threading.Tasks;

using static Approvals.ViewModels.UserEditViewModel;

namespace Approvals.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Index()
        {
            var user = _context.Users.Include(c => c.Roles).ToList();

            return View(user);
        }

        public ActionResult Edit(string id)
        {
            var user = _context.Users.SingleOrDefault(c => c.Id == id);
            var clients = _context.Clients.ToList();

            var viewModel = new UserEditViewModel
            {
                User = user,
            };
            return View(viewModel);
        }

        public ActionResult AddClient(string id)
        {
            var user = _context.Users.SingleOrDefault(c => c.Id == id);
            var clients = _context.Clients.ToList();

            var userProfileViewModel = ToViewModel(user, clients);

            return View(userProfileViewModel);
        }

        private ICollection<AssignedClientData> PopulateClientData()
        {
            var clients = _context.Clients;
            var assignedClients = new List<AssignedClientData>();

            foreach (var item in clients)
            {
                assignedClients.Add(new AssignedClientData
                {
                    ClientId = item.Id,
                    ClientAcronym = item.ClientAcronym,
                    Assigned = false
                });
            }
            return assignedClients;
        }

        [HttpPost]
        public ActionResult Save(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new UserEditViewModel

                {
                    User = user,
                };

                return View("Edit", viewModel);
            }

            if (user.Id == null)
                _context.Users.Add(user);

            else
            {
                var userInDb = _context.Users.Single(c => c.Id == user.Id);
                userInDb.FirstName = user.FirstName;
                userInDb.LastName = user.LastName;
                userInDb.IsAdmin = user.IsAdmin;
                userInDb.IsApprover = user.IsApprover;
                userInDb.IsOwner = user.IsOwner;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public ActionResult SaveAddClient(UserEditViewModel userEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var userInDb = _context.Users.Single(c => c.Id == userEditViewModel.User.Id);

                // Add or update new courses
                // AddOrUpdateCourses(originalUserProfile, userProfileViewModel.Courses);

                // Add or update courses keeping original
                AddOrUpdateClients(userInDb, userEditViewModel.Clients);

               // _context.Entry(userInDb).CurrentValues.SetValues(userEditViewModel);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(userEditViewModel);
        }

        private void AddClient(ApplicationUser user, IEnumerable<AssignedClientData> assignedClients)
        {


            //if (assignedClients == null) return;

            if (user.Id != null)
            {
                // Existing user - drop their existing courses and add the new ones if any
                foreach (var client in user.Clients.ToList())
                {
                    user.Clients.Remove(client);
                }

                foreach (var client in assignedClients.Where(c => c.Assigned))
                {
                    user.Clients.Add(_context.Clients.Find(client.ClientId));
                }
            }
            else
            {
                // New user
                foreach (var assignedClient in assignedClients.Where(c => c.Assigned))
                {
                    var client = new Client { Id = assignedClient.ClientId };
                    _context.Clients.Attach(client);
                    user.Clients.Add(client);
                }
            }
        }

        public UserEditViewModel ToViewModel(ApplicationUser user, ICollection<Client> allDbClients)
        {
            var userEditViewModel = new UserEditViewModel
            {
                User = user,
                Name = user.LastName,
                UserId = user.Id
            };

            ICollection<AssignedClientData> allClients = new List<AssignedClientData>();

            foreach (var item in allDbClients)
            {
                // Create new AssignedClientData for each course and set Assigned = true if user already has course
                var assignedCourse = new AssignedClientData
                {
                    ClientId = item.Id,
                    ClientAcronym = item.ClientAcronym,
                    Assigned = user.Clients.FirstOrDefault(x => x.Id == item.Id) != null
                };

                allClients.Add(assignedCourse);
            }

            userEditViewModel.Clients = allClients;

            return userEditViewModel;
        }

        private void AddOrUpdateClients(ApplicationUser user, IEnumerable<AssignedClientData> assignedClients)
        {
            if (assignedClients == null) return;

            if (user.Id != null)
            {
                // Existing user - drop their existing courses and add the new ones if any
                foreach (var client in user.Clients.ToList())
                {
                    user.Clients.Remove(client);
                }

                foreach (var client in assignedClients.Where(c => c.Assigned))
                {
                    user.Clients.Add(_context.Clients.FirstOrDefault(x => x.Id == client.ClientId));
                }
            }
        }
    }
}