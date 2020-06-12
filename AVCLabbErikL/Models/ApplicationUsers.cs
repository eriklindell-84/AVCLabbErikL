using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AVCLabbErikL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace AVCLabbErikL.Areas.Identity.Pages.Account.Manage
namespace AVCLabbErikL.Models
{
    public class ApplicationUsers : IdentityUser
    {
        public ApplicationUsers()
        { }

        [PersonalData]
        public string Street { get; set; }
        [PersonalData]
        public int ZipCode { get; set; }
        [PersonalData]
        public string CareOf { get; set; }
        [PersonalData]
        public string City { get; set; }
        
        public Guid UserId { get; set; }

        public bool isAdressEmpty;
    }
}
