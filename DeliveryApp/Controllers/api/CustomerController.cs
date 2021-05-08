using DeliveryApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DeliveryApp.Controllers.api
{
    public class CustomerController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IHttpActionResult GetAllCustomers()
        {

            //return list of users that have role = customer
            var allUsers = db.Users.ToList().Where(x => x.Roles.Select(role => role.RoleId).Equals(1)).ToList();
            return Ok(allUsers);
        }

        public IHttpActionResult GetCustomerById(string id)
        {
            //return user that has id
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            return Ok(user);
        }

        [HttpPost]
        public IHttpActionResult AddCustomer(ApplicationUser u)
        {
            //Add user
            db.Users.Add(u);
            db.SaveChanges();
            return Ok(u);
        }

        public IHttpActionResult GetAllStores()
        {
            var stores = db.Branches.ToList();
            return Ok(stores);
        }

        public IHttpActionResult GetStoreBySearch(string location)
        {
            if (location != null)
            {
                var findByGov = db.Branches.Where(b => b.BranchGovernorate.ToLower() == location.ToLower()).ToList();
                var findByCity = db.Branches.Where(b => b.BranchCity.ToLower() == location.ToLower()).ToList();
                var findByName = db.Branches.Where(b => b.BranchName.ToLower() == location.ToLower()).ToList();
                if (findByGov.Count > 0)
                    return Ok(findByGov);
                else if (findByCity.Count > 0)
                    return Ok(findByCity);
                else if (findByName.Count > 0)
                    return Ok(findByName);
                else
                    return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult GetAllOrders(string customerID)
        {
            var allOrders = db.Orders.Where(o => o.Customer.Id == customerID).ToList();

            return Ok(allOrders);
        }

        public IHttpActionResult GetNewOrder(string customerID)
        {
            var allOrders = db.Orders.Where(o => o.Customer.Id == customerID).ToList();

            var NewOrder = allOrders.Where(o => o.OrderDateTime == DateTime.Now).ToList();

            return Ok(NewOrder);
        }

        public IHttpActionResult GetOldOrder(string customerID)
        {
            var allOrders = db.Orders.Where(o => o.Customer.Id == customerID).ToList();

            var OldOrders = allOrders.Where(o => o.OrderDateTime != DateTime.Now).ToList();

            return Ok(OldOrders);
        }
    }
}
