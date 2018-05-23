using VirtualTour.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace VirtualTour.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ApplicationContext db = new ApplicationContext();
        public ActionResult Index()
        {
            var images = db.Images;
            return View(images.ToList());
        }
        public ActionResult Image(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Gallery");

            }
            else
            {
                var img = db.Images.Find(id);
                return View(img);
            }
            
        }
        [HttpGet]
        [Authorize]
        public ActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult UploadImageMethod()
        {
            if (Request.Files.Count != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    file.SaveAs(Server.MapPath("~/Upload_Files/" + fileName));
                    Image image = new Image();
                    image.ID = Guid.NewGuid();
                    image.Name = fileName;
                    image.ImagePath = "~/Upload_Files/" + fileName;
                    image.UserId = User.Identity.GetUserId();
                    db.Images.Add(image);
                    db.SaveChanges();
                }
                return Content("Success");
            }
            return Content("failed");
        }
    }
}
