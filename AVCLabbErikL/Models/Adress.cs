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
    public class Adress
    {
        public Adress()
        { }

        public int ID { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string CareOf { get; set; }
        public string City { get; set; }
        public Guid UserID { get; set; }

        public bool isAdressEmpty;


    }
}