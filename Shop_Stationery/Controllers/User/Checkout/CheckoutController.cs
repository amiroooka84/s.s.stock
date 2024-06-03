using bll.Admin.bl_Shop;
using bll.User.bl_Address;
using bll.User.bl_Product;
using Entity;
using Entity.Address;
using Entity.AttributeProduct;
using Entity.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;
using Entity.Basket;
using bll.User.bl_Order;
using Shop_Stationery.Models.Classes.ClassesUser.ConvertShamsiToMiladi;
using ZarinPal;
using ZarinPal.Class;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Shop_Stationery.Models.Classes.ClassesUser.ZarinPal;

namespace Shop_Stationery.Controllers.User
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly SignInManager<UserApp> _SignInManager;
        private readonly IHttpClientFactory _clientFactory;
        public CheckoutController(UserManager<UserApp> userManager, SignInManager<UserApp> SignInManager, IHttpClientFactory clientFactory)
        {
            _userManager = userManager;
            _SignInManager = SignInManager;
            _clientFactory = clientFactory;
        }

        //////////////////
        #region Transport
        [Route("/Checkout/Shipping")]
        public async Task<IActionResult> Shipping()
        {
            List<Address> addresses = new List<Address>();
            bl_Address bl_Address = new bl_Address();
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            addresses = bl_Address.ReadAddress(userApp);
            bl_Product bl_Product = new bl_Product();
            List<Entity.Basket.Basket> baskets = new List<Entity.Basket.Basket>();
            baskets = bl_Product.ReadBasket(userApp.Id);
            if (baskets.Count == 0)
            {
                return RedirectToAction("basket", "account");
            }
            List<Product> products = new List<Product>();
            foreach (Product item in bl_Product.ReadProduct())
            {
                foreach (var item1 in baskets)
                {
                    if (item.id == item1.ProductId)
                    {
                        products.Add(item);
                    }
                }
            }
            List<AttributeProduct> readattributeProducts = new List<AttributeProduct>();
            readattributeProducts = bl_Product.ReadAttribute();
            List<AttributeProduct> attributeProducts = new List<AttributeProduct>();
            foreach (var item in baskets)
            {
                foreach (var item1 in readattributeProducts)
                {
                    if (item.AttributeId == item1.id && item1.Number > 0)
                    {
                        attributeProducts.Add(item1);
                    }
                }
            }
            long Discountall = 0;
            long priceall = 0;
            foreach (var item in baskets)
            {
                foreach (var product in products)
                {
                    if (product.id == item.ProductId)
                    {
                        long price = (product.Price / 100) * (100 - product.Discount);
                        priceall += price * item.Number;


                        long discount = (product.Price / 100) * product.Discount;
                        Discountall += discount * item.Number;

                        break;
                    }
                }
            }
            bl_Shop bl_Shop = new bl_Shop();
            List<long> shopids = new List<long>();
            foreach (var item in bl_Shop.ReadShopAndProduct())
            {
                foreach (var product in products)
                {
                    if (product.id == item.Productid)
                    {
                        shopids.Add(item.Shopid);
                    }
                }
            }
            List<Shop> shops = new List<Shop>();
            foreach (var shopid in shopids)
            {
                foreach (var shop in bl_Shop.ReadShop())
                {
                    if (shopid == shop.id)
                    {
                        shops.Add(shop);
                    }
                }
            }

            var PostingDay = from i in shops orderby i.PostingDay ascending select i;
            ViewBag.Warning = TempData["Warning"];
            ViewBag.PostingDay = 3;
            ViewBag.priceall = priceall;
            ViewBag.Discountall = Discountall;
            ViewBag.Addresses = addresses;
            ViewBag.baskets = baskets;
            ViewBag.AttributeProduct = attributeProducts;
            ViewBag.Products = products;
            return View();
        }
        #endregion
        //////////////////

        //////////////////
        #region submit
        [Route("/Checkout/submit")]
        [HttpPost]
        public async Task<IActionResult> submit(string FirstName, string LastName, string Province, string City, string Address, string PhoneNumber)
        {
            if (FirstName == null || LastName == null || Province == null || City == null || Address == null || PhoneNumber == null || PhoneNumber.Length != 11)
            {
                TempData["Warning"] = true;
                TempData.Keep();
                return Redirect("/Checkout/Shipping");
            }
            bl_Product bl_Product = new bl_Product();
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            List<Entity.Basket.Basket> baskets = new List<Entity.Basket.Basket>();
            baskets = bl_Product.ReadBasket(userApp.Id);
            if (baskets.Count == 0)
            {
                return RedirectToAction("basket", "account");
            }
            List<Product> products = new List<Product>();
            foreach (Product item in bl_Product.ReadProduct())
            {
                foreach (var item1 in baskets)
                {
                    if (item.id == item1.ProductId)
                    {
                        products.Add(item);
                    }
                }
            }
            long Discountall = 0;
            long priceall = 0;
            foreach (var item in baskets)
            {
                foreach (var product in products)
                {
                    if (product.id == item.ProductId)
                    {
                        long price = (product.Price / 100) * (100 - product.Discount);
                        priceall += price * item.Number;


                        long discount = (product.Price / 100) * product.Discount;
                        Discountall += discount * item.Number;

                        break;
                    }
                }
            }

            TempData["Price"] = priceall.ToString();
            TempData["Discount"] = Discountall.ToString();
            TempData["FullName"] = FirstName + " " + LastName;
            TempData["PhoneNumber"] = PhoneNumber;
            TempData["Address"] = Province + " " + City + " " + Address;
            TempData.Keep();
            return Redirect("/Checkout/PaymentPort");
        }
        #endregion
        //////////////////

        //////////////////
        #region PaymentPort
        [Route("/Checkout/PaymentPort")]
        public async Task<IActionResult> PaymentPort()
        {

            string referer = Request.Headers["Referer"].ToString();
            TempData["Price"] = TempData["Price"];
            TempData["Discount"] = TempData["Discount"];
            TempData["FullName"] = TempData["FullName"];
            TempData["PhoneNumber"] = TempData["PhoneNumber"];
            TempData["Address"] = TempData["Address"];
            TempData.Keep();

            ////////////////////////
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            bl_Product bl_Product = new bl_Product();
            List<Entity.Basket.Basket> baskets = new List<Entity.Basket.Basket>();
            baskets = bl_Product.ReadBasket(userApp.Id);
            if (baskets.Count == 0)
            {
                return RedirectToAction("basket", "account");
            }
            List<Product> products = new List<Product>();
            products = bl_Product.ReadProduct();
            List<ProductOrder> ProductOrders = new List<ProductOrder>();
            foreach (var product in products)
            {
                List<AttributeProduct> attributeProducts = new List<AttributeProduct>();
                attributeProducts = await bl_Product.ReadAttributeProduct(product.id);
                foreach (var basket in baskets)
                {
                    foreach (var attr in attributeProducts)
                    {
                        if (product.id == basket.ProductId && basket.AttributeId == attr.id)
                        {
                            ProductOrder productOrder = new ProductOrder();
                            productOrder.Name = product.Name;
                            productOrder.UserId = basket.UserId;
                            productOrder.Code = product.Code;
                            productOrder.Price = product.Price;
                            productOrder.Discount = product.Discount;
                            productOrder.Color = attr.Color;
                            productOrder.Size = attr.Size;
                            productOrder.AttrId = attr.id;
                            productOrder.ImagePath = product.ImagePath;
                            productOrder.Number = basket.Number;
                            ProductOrders.Add(productOrder);
                        }
                    }
                }
            }
            Order order = new Order();
            order.UserId = userApp.Id;
            order.PhoneNumber = TempData["PhoneNumber"].ToString();
            order.Date = ConvertDateTime.ConvertMiladiToShamsi(DateTime.Today, "yyyy/MM/dd");
            order.FullName = TempData["FullName"].ToString();
            order.Address = TempData["Address"].ToString();
            order.Price = Convert.ToInt64(TempData["Price"]);
            order.Discount = Convert.ToInt64(TempData["Discount"]);
            order.State = Order.state.Processing;
            order.IsFinally = false;
            bl_Order bl_Order = new bl_Order();
            long OrderId = await bl_Order.OrderRegistration(order, ProductOrders);
            ////////////////////////
            if (OrderId != 0)
            {
                ZarinPalRequest zarinPalRequest = new ZarinPalRequest();
                zarinPalRequest.merchant_id = "be38fe13-49fc-4bcf-9613-66739395597d";
                zarinPalRequest.amount = Convert.ToInt32(TempData["Price"]);
                zarinPalRequest.description = "پرداخت فاکتور";
                zarinPalRequest.callback_url = Url.Action("PaymentVerify", "Checkout", new { OrderId = OrderId, Amount = Convert.ToInt32(TempData["Price"]) }, this.Request.Scheme, "stockssoheil.com");
                var data = new StringContent(JsonSerializer.Serialize(zarinPalRequest), Encoding.UTF8, "application/json");
                //var client = _clientFactory.CreateClient();
                //var response = await client.PostAsync("https://api.zarinpal.com/pg/v4/payment/request.json", data);




                var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    "https://api.zarinpal.com/pg/v4/payment/request.json"
                );

                request.Headers.Add("accept", "application/json");
                request.Content = data;
                await request.Content.ReadAsStringAsync();
                request.Properties.Add("merchant_id", "be38fe13-49fc-4bcf-9613-66739395597d");
                request.Properties.Add("amount", Convert.ToInt32(TempData["Price"]));
                request.Properties.Add("description", "پرداخت فاکتور");
                request.Properties.Add("callback_url", Url.Action("PaymentVerify", "Checkout", new { OrderId = OrderId, Amount = Convert.ToInt32(TempData["Price"]) }, this.Request.Scheme, "stockssoheil.com"));
                var client1 = _clientFactory.CreateClient();
                var response1 = await client1.SendAsync(request);

                string BackRequest = await response1.Content.ReadAsStringAsync();

                ZarinPalBackRequest convert = JsonSerializer.Deserialize<ZarinPalBackRequest>(BackRequest);



                if (convert.data.code == 100)
                {
                    Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + convert.data.authority);
                    //Response.Redirect();
                    //Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + status.Authority);
                }
                else
                {
                    BadRequest($"خطا در پرداخت. کد خطا:{convert.data.code}");
                }








                //var client = _clientFactory.CreateClient();
                //var response = await client.PostAsync("https://api.zarinpal.com/pg/v4/payment/request.json" , data);



                //Encoding.UTF8, "application/json"
                //var httpClient = new HttpClient();
                //Payment payment = new Payment();
                //var status = payment.Request(new { merchant_id = "be38fe13-49fc-4bcf-9613-66739395597d" , amount = Convert.ToInt32(TempData["Price"]) , description = "فروشگاه استوک ستاره سهیل" , callback_url = Url.Action(nameof(PaymentVerify), "Checkout", new { OrderId = OrderId, Amount = Convert.ToInt32(TempData["Price"]) }) } , Payment.Mode.zarinpal).Result;
                //using var httpResponse =
                //    await httpClient.PostAsync("https://api.zarinpal.com/pg/v4/payment/request.json", todoItemJson);
                //request.Content.Headers.Add("amount", Convert.ToInt32(TempData["Price"]).ToString());
                //request.Content.Headers.Add("description", "فروشگاه استوک ستاره سهیل");
                //request.Content.Headers.Add("callback_url", Url.Action(nameof(PaymentVerify), "Checkout", new { OrderId = OrderId, Amount = Convert.ToInt32(TempData["Price"]) }));


                //if (status.Status == 100)
                //{
                //    Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + status.Authority);
                //    //Response.Redirect();
                //    //Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + status.Authority);
                //}
                //else
                //{
                //    BadRequest($"خطا در پرداخت. کد خطا:{status.Status}");
                //}
            }
            else
            {
                ViewBag.Error = false;
                return Redirect("/Checkout/Shipping");
            }
            return View();
        }
        #endregion
        //////////////////

        //////////////////
        #region OrderRegistration
        [Route("/Checkout/OrderRegistration")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> PaymentVerify(string Authority, string Status, long OrderId, int Amount)
        {
            //https://stockssoheil.com/Checkout/OrderRegistration?OrderId=8&Amount=1000&Authority=A00000000000000000000000000402620949&Status=OK
            ViewBag.price = TempData["Price"];
            ViewBag.Discount = TempData["Discount"];
            TempData["Price"] = TempData["Price"];
            TempData["Discount"] = TempData["Discount"];
            TempData["Address"] = TempData["Address"];
            TempData.Keep();
            if (Status == "NOK") return View("Error");

            ZarinPalRequestVerify zarinPalRequest = new ZarinPalRequestVerify();
            zarinPalRequest.merchant_id = "be38fe13-49fc-4bcf-9613-66739395597d";
            zarinPalRequest.amount = Amount;
            zarinPalRequest.authority = Authority;
            var data = new StringContent(JsonSerializer.Serialize(zarinPalRequest), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(
                   HttpMethod.Post,
                   "https://api.zarinpal.com/pg/v4/payment/verify.json"
               );

            request.Headers.Add("accept", "application/json");
            request.Content = data;
            await request.Content.ReadAsStringAsync();
            var client1 = _clientFactory.CreateClient();
            var response1 = await client1.SendAsync(request);


            string BackRequest = await response1.Content.ReadAsStringAsync();


            ZarinPalVerify convert = JsonSerializer.Deserialize<ZarinPalVerify>(BackRequest);

            //ارسال به صفحه خطا

            ////ارسال کد تراکنش به جهت نمایش به کاربر
            //var refId = status.RefId;

            if (convert.data.code == 100)
            {
                bl_Order bl_Order = new bl_Order();
                bool Result = bl_Order.IsFinallyOrder(OrderId , convert.data.ref_id);
                ViewData["ResultOrder"] = "ok";
                TempData["ResultOrder"] = "ok";
                TempData.Keep();
                return Redirect("/Account/Order");
            }
            else
            {
                BadRequest($"خطا در پرداخت. کد خطا:{convert.data.code}");
                return View();
            }
            #endregion
            //////////////////
        }
    }
}
