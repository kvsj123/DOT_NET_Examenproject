using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DOT_NET_Examenproject.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
   
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [NotMapped]
    public string? RoleId { get; set; }
    [NotMapped]
    public string? Role { get; set; }
    [NotMapped]
    public IEnumerable<SelectListItem>? RoleList { get; set; }
}

