using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using VirtualTour.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public IEnumerable<Image> Images;
    public ApplicationUser()
    {
        Images = new List<Image>();
    }
}