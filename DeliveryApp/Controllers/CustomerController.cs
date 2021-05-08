using DeliveryApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace DeliveryApp.Controllers
{
    //[Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        //Order O = new Order();
        //Branch B = new Branch();
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Location)
        {
            if(Location != null)
            {
                var findByGov = db.Branches.Where(b => b.BranchGovernorate.ToLower() == Location.ToLower()).ToList();
                var findByCity = db.Branches.Where(b => b.BranchCity.ToLower() == Location.ToLower()).ToList();
                var findByName = db.Branches.Where(b => b.BranchName.ToLower() == Location.ToLower()).ToList();
                if (findByGov.Count > 0)
                    return View("SearchResult", findByGov);
                else if (findByCity.Count > 0)
                    return View("SearchResult", findByCity);
                else if (findByName.Count > 0)
                    return View("SearchResult", findByName);
            }
            else
            {
                //search by lng and lat
            }
            return View();
        }

        public ActionResult showBranch(int id)
        {
            var branchSelected = db.Branches.FirstOrDefault(b => b.BranchId == id);
            var bestSellingPoduct = branchSelected.Products.OrderByDescending(a => a.ProductCount).FirstOrDefault();
            ViewBag.bestselling = bestSellingPoduct;
            return View(branchSelected);
        }

        public ActionResult ShowProfile()
        {
            //if (O != null)
            //{
            //    if (O.OrderDateTime >= DateTime.Now)
            //        ViewBag.newOrder = O;
            //    else
            //        ViewBag.oldOrder = O;
            //}
            return View(user);
        }

        public ActionResult AllBranches()
        {
            return View(db.Branches.ToList());
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            return View(user);
        } 
        
        [HttpPost]
        public async Task<ActionResult> EditProfile(ApplicationUser _user)
        {
            ////get current user and update
            //var userCurnt = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //userCurnt.Image = _user.Image;
            //userCurnt.UserName = _user.UserName;
            //userCurnt.Email = _user.Email;

            //var updateResult = await UserManager.UpdateAsync(user);
            //if (updateResult.Succeeded)
            //{
            //    //do something and return
            //    return RedirectToAction("showprofile");
            //}
            ////failed - do something else and return
            //return View();


            if (!ModelState.IsValid)
            {
                return View(_user);
            }

            var userStore = new UserStore<ApplicationUser>(new
                                          ApplicationDbContext());
            var appManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(userStore);

            var currentUser = appManager.FindByEmail(_user.Email);

            // here you can assign the updated values
            currentUser.Image = _user?.Image;
            currentUser.Email = _user?.Email;
            currentUser.UserName = _user?.UserName;

            // and rest fields are goes here
            await appManager.UpdateAsync(currentUser);

            var ctx = userStore.Context;
            ctx.SaveChanges();

            // now you can redirect to some other method or-else you can return 
            // to this view itself by returning the data

            return RedirectToAction("showProfile");

        }


        public ActionResult Cart()
        {
            return View();
        }
    }
}