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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

//using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AVCLabbErikL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly SignInManager<ApplicationUsers> _signInManager;

        public List<OrderModel> postOrderList = new List<OrderModel>();
        public static List<OrderModel> getOrdersList = new List<OrderModel>();


        public IEnumerable<OrderModel> Orders { get; private set; }
        public bool GetOrderError { get; private set; }
        // GET: /<controller>/

        public OrderController(ILogger<HomeController> logger, UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signInManager, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _clientFactory = clientFactory;

        }
        public IActionResult ConfirmOrder()
        {
            PlaceOrder();

            return View(getOrdersList);
        }

        public async Task OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:53445/Order");
            request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    Orders = await System.Text.Json.JsonSerializer.DeserializeAsync
                    <IEnumerable<OrderModel>>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    getOrdersList = Orders.ToList();
                }
            }
            else
            {
                GetOrderError = true;
                Orders = Array.Empty<OrderModel>();
            }
        }

        public async Task<IActionResult> Index()
        {
            await OnGet();
            return View("GetOrders", getOrdersList);
        }


        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            HttpClient client = new HttpClient();
            OrderModel om = new OrderModel();

            // Grab all items from cartlist
            var cart = from e in AVCLabbErikL.Controllers.CartController.cartList
                       select e;

            foreach (var item in cart)
            { 
                om.OrderAmount = AVCLabbErikL.Controllers.HomeController.totalAmount;
                om.OrderDate = DateTime.Now;
                om.UserID = Guid.Parse(signedInUserID);
                postOrderList.Add(om);
            }
            

            string orderobject = JsonConvert.SerializeObject(om);
            var content = new StringContent(orderobject, Encoding.UTF8, "application/json");

            await client.PostAsync("http://localhost:53445/PlaceOrder", content);

            string response = await client.GetStringAsync("http://localhost:53445/Order");

            List<OrderModel> ordersList = JsonConvert.DeserializeObject<List<OrderModel>>(response);

            AVCLabbErikL.Controllers.CartController.cartList.Clear();
            AVCLabbErikL.Controllers.HomeController.totalAmount = 0;
            return View("ConfirmOrder", postOrderList); 
        }
    }
}

