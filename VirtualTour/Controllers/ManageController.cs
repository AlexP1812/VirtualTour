using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
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



        [Authorize(Roles ="admin")]
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var users = db.Users.ToList();
            return View(users.ToPagedList(pageNumber, pageSize));
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