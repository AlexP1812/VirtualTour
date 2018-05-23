using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VirtualTour.Models;

namespace VirtualTour.Controllers
{
    public class ManageController : Controller
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
            var images = db.Images.Where(c => c.UserId==id );
            return View(images.ToList());
        }


        [Authorize(Roles ="admin")]
        public ActionResult AdminIndex()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                EditModel model = new EditModel { Name = user.Name, Email=user.Email, Id=user.Id };
                return View(model);
            }
            return RedirectToAction("AdminIndex", "Manage");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(EditModel model)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("AdminIndex", "Manage");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }
    }
}