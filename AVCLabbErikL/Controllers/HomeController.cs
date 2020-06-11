using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AVCLabbErikL.Models;
using AVCLabbErikL.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AVCLabbErikL.Areas.Identity.Pages.Account.Manage;
using System.Net.Http;
using System.Text.Json;

namespace AVCLabbErikL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public IEnumerable<ProductModel> Products { get; private set; }
        public bool GetProductsError { get; private set; }
        
        // List of the products
        public List<ProductModel> productList = new List<ProductModel>();

        // List of the items in shopping cart
        public static List<ProductModel> orderList = new List<ProductModel>();
        public static List<Adress> adressList = new List<Adress>();

        // varibale to store total amount to pay
        public static double totalAmount;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _clientFactory = clientFactory;

        }
        public async Task OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:53445/Product");
            request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    Products = await JsonSerializer.DeserializeAsync
                    <IEnumerable<ProductModel>>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    productList = Products.ToList();
                }
            }
            else
            {
                GetProductsError = true;
                Products = Array.Empty<ProductModel>();
            }
        }

        public async Task<IActionResult> Index()
        {
            await OnGet();


            //ProductModel prod1 = new ProductModel()

            //ProductModel prod2 = new ProductModel()
            //{ Id = 2, Name = "Photo2", Description = "beatufil picture by:", Price = 249.5, ImgUrl = "https://i.picsum.photos/id/200/200/300.jpg" };
            //ProductModel prod3 = new ProductModel()
            //{ Id = 3, Name = "Photo3", Description = "beatufil picture by:", Price = 179.5, ImgUrl = "https://i.picsum.photos/id/225/200/300.jpg" };
            //ProductModel prod4 = new ProductModel()
            //{ Id = 4, Name = "Photo4", Description = "beatufil picture by:", Price = 349, ImgUrl = "https://i.picsum.photos/id/240/200/300.jpg" };
            //ProductModel prod5 = new ProductModel()
            //{ Id = 5, Name = "Photo5", Description = "beatufil picture by:", Price = 129.5, ImgUrl = "https://i.picsum.photos/id/199/200/300.jpg" };
            //ProductModel prod6 = new ProductModel()
            //{ Id = 6, Name = "Photo6", Description = "beatufil picture by:", Price = 349.5, ImgUrl = "https://i.picsum.photos/id/175/200/300.jpg" };
            //ProductModel prod7 = new ProductModel()
            //{ Id = 7, Name = "Photo7", Description = "beatufil picture by:", Price = 129, ImgUrl = "https://i.picsum.photos/id/270/200/300.jpg" };
            //ProductModel prod8 = new ProductModel()
            //{ Id = 8, Name = "Photo8", Description = "beatufil picture by:", Price = 349, ImgUrl = "https://i.picsum.photos/id/300/200/300.jpg" };
            //ProductModel prod9 = new ProductModel()
            //{ Id = 9, Name = "Photo9", Description = "beatufil picture by:", Price = 99.5, ImgUrl = "https://i.picsum.photos/id/284/200/300.jpg" };
            //ProductModel prod10 = new ProductModel()
            //{ Id = 10, Name = "Photo10", Description = "beatufil picture by:", Price = 149, ImgUrl = "https://i.picsum.photos/id/198/200/300.jpg" };

            // Show content of productList
            return View("Index", productList);

        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult Adress()
        {
            adressList.Clear();
            var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // If userID don't excist site will not load any userdata
                var getAdressinfo = from a in db.Adresses
                                    where a.UserID == Guid.Parse(signedInUserID)
                                    select a;

                foreach (var item in getAdressinfo)
                {
                    Adress adress = new Adress();

                    adress.ID = item.ID;
                    adress.Street = item.Street;
                    adress.ZipCode = Convert.ToInt32(item.ZipCode);
                    adress.City = item.City;
                    adress.CareOf = item.CareOf;
                    adress.UserID = item.UserID;
                    adressList.Add(adress);
                }
                return View();

            }
        }
        [HttpPost]
        public IActionResult Adress(string Adress, string CareOf, int ZipCode, string City)
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                adressList.Clear();
                Adress adress = new Adress();
                var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

                var checkEmptyAdress = from e in db.Adresses
                                       select e;
                if (checkEmptyAdress.Count() == 0)
                {
                    adress.isAdressEmpty = true;
                }

                if (adress.isAdressEmpty == true)
                {


                    var query = from user in db.Adresses
                                where user.UserID == Guid.Parse(signedInUserID)
                                select user;


                    adress.Street = Adress;
                    adress.Street = Adress;
                    adress.CareOf = CareOf;
                    adress.ZipCode = ZipCode;
                    adress.City = City;
                    adress.UserID = Guid.Parse(signedInUserID);
                    db.Adresses.Add(adress);
                    db.SaveChanges();
                }
                else
                {
                    var query = from user in db.Adresses
                                where user.UserID == Guid.Parse(signedInUserID)
                                select user;

                    foreach (var item in query)
                    {
                        item.Street = Adress;
                        item.CareOf = CareOf;
                        item.ZipCode = ZipCode;
                        item.City = City;
                        item.UserID = Guid.Parse(signedInUserID);
                    }
                    db.SaveChanges();
                }


            }
            return RedirectToAction("Adress");
        }
    }
}
