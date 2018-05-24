using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTest.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private readonly ApplicationContext db = new ApplicationContext();


        [Authorize]
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ApplicationUser user = db.Users.FirstOrDefault(t => t.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.Images = db.Images.Where(c => c.UserId == id);
            return View(user);
        }

        [Authorize]
        public ActionResult UserGallery()
        {

            string id = User.Identity.GetUserId();
            var images = db.Images.Where(c => c.UserId == id);
            return View(images.ToList());
        }
    }
}