using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public string? EmployeeNumber { get; set; }

        public string? City { get; set; }

        public ApplicationUser(string userName, string name)
        {
            UserName = userName;
            Name = name;
        }
    }
}
