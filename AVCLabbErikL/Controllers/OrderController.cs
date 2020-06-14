using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AVCLabbErikL.Data;
using AVCLabbErikL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

//using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AVCLabbErikL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly SignInManager<ApplicationUsers> _signInManager;
        

        public List<OrderModel> postOrderList = new List<OrderModel>();
        public static List<OrderModel> getOrdersList = new List<OrderModel>();
        public static List<OrderModel> ordersList = new List<OrderModel>();

        public IEnumerable<OrderModel> Orders { get; private set; }
        public bool GetOrderError { get; private set; }
        // GET: /<controller>/

        public OrderController(ILogger<HomeController> logger, UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signInManager, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            await PlaceOrder();
            return View(postOrderList);
        }
        public async Task<IActionResult> GetOrders()
        {
            await OnGet();
            return View(ordersList);
        }

        // Async Task to Get all previous orders
        public async Task<IActionResult> OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:53445/Order");
            request.Headers.Add("Accept", "application/json");

            //Get Api Key from Config
            var orderApiKey = _configuration.GetValue<string>("ApiKeys:OrderApiKey");
            
            //Add Key To Request
            request.Headers.Add("ApiKey", orderApiKey);

            // Create the Client
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            // IF Successful Deserialzie Jsonstring to C# object List
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    Orders = await System.Text.Json.JsonSerializer.DeserializeAsync
                    <IEnumerable<OrderModel>>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    // Filter OrderList to only display orders from signed in user
                    ordersList = Orders.Where(x=>x.UserID == Guid.Parse(_userManager.GetUserId(User))).ToList();
                }
            }
            // Set to Empty Array if no Match
            else
            {
                GetOrderError = true;
                Orders = Array.Empty<OrderModel>();
            }
            return View("GetOrders", ordersList);
        }



        // Logic for Posting an Order
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            // Get signed in user. Used for assigning orderer to correct User
            var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            HttpClient client = new HttpClient();
            OrderModel om = new OrderModel();

            // Grab all items from cartlist
            var cart = from e in AVCLabbErikL.Controllers.CartController.cartList
                       select e;

            //Populate postOrderList (used for showing orderconfirmation after order is complete)

                om.OrderAmount = AVCLabbErikL.Controllers.HomeController.totalAmount;
                om.OrderDate = DateTime.Now;
                om.UserID = Guid.Parse(signedInUserID);
                postOrderList.Add(om);
            

            // Convert The ordermodelobject to Json and Place order through Api Gateway
            string orderobject = JsonConvert.SerializeObject(om);
            var content = new StringContent(orderobject, Encoding.UTF8, "application/json");
            
            // Get the apiKey
            var orderApiKey = _configuration.GetValue<string>("ApiKeys:OrderApiKey");
            
            //Add The header containing the Api Key to Request
            client.DefaultRequestHeaders.Add("ApiKey", orderApiKey);
            await client.PostAsync("http://localhost:53445/PlaceOrder", content);
            
            string response = await client.GetStringAsync("http://localhost:53445/Order");
            
            // Empty cartList and set Total amont back to 0 after purchase has been made
            AVCLabbErikL.Controllers.CartController.cartList.Clear();
            AVCLabbErikL.Controllers.HomeController.totalAmount = 0;
            return View(postOrderList);
        }
    }
}

