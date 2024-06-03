using bll.Admin.bl_Order;
using Entity.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Controllers.Admin.OrderManagement
{
    [Authorize(Policy = "AdminOnly")]
    public class OrderManagementController : Controller
    {
        public OrderManagementController()
        {
            StiLicense.LoadFromString("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");
        }

        /////////////////
        #region OrderManagement
        [Route("/OrderManagement/{StateOrder}")]
        public IActionResult OrderManagement(string StateOrder)
        {
            bl_Order bl_Order = new bl_Order();
            List<Order> orders = new List<Order>();
            foreach (var item in bl_Order.ReadOrder())
            {
                if (StateOrder == "IsFinally")
                {
                    if (item.IsFinally == false)
                    {
                        orders.Add(item);
                    }
                }
                if (StateOrder == "Processing" )
                {
                    if (item.State == Order.state.Processing && item.IsFinally == true)
                    {
                        orders.Add(item);
                    }
                }
                else if (StateOrder == "Preparation")
                {
                    if (item.State == Order.state.Preparation && item.IsFinally == true)
                    {
                        orders.Add(item);
                    }
                }
                else if (StateOrder == "Delivered")
                {
                    if (item.State == Order.state.Delivered && item.IsFinally == true)
                    {
                        orders.Add(item);
                    }
                }
                else if (StateOrder == "Canceled")
                {
                    if (item.State == Order.state.Canceled && item.IsFinally == true)
                    {
                        orders.Add(item);
                    }
                }
            }
            ViewBag.Orders = orders;
            return View();
        }
        #endregion
        /////////////////

        /////////////////
        #region SearchOrder
        [Route("/SearchOrder")]
        public IActionResult SearchOrder(long OrderCode, string Number)
        {
            bl_Order bl_Order = new bl_Order();
            List<Order> orders = new List<Order>();
            if (OrderCode == 0)
            {
                orders = bl_Order.SearchOrderByNumber(Number);
            }
            else if (Number == null)
            {
                orders = bl_Order.SearchOrderByCode(OrderCode);
            }
            else
            {
                orders = bl_Order.SearchOrder(OrderCode, Number);
            }
            ViewBag.Orders = orders;
            return View("OrderManagement");
        }
        #endregion
        /////////////////

        /////////////////
        #region DetailsOrder
        [Route("/ReadOrder/{OrderId}")]
        public IActionResult DetailsOrder(long OrderId)
        {
            bl_Order bl_Order = new bl_Order();
            Order order = new Order();
            order = bl_Order.DetailsOrder(OrderId);
            ViewBag.Order = order;
            List<ProductOrder> productOrders = new List<ProductOrder>();
            productOrders = bl_Order.ProductsOrder(OrderId);
            ViewBag.ProductOrders = productOrders;
            return View();
        }
        #endregion
        /////////////////

        /////////////////
        #region SearchOrderByNumber
        [Route("/SearchOrderByNumber")]
        public IActionResult SearchOrderByNumber(string Number)
        {
            bl_Order bl_Order = new bl_Order();
            List<Order> orders = new List<Order>();
            ViewBag.Orders = orders;
            return View("OrderManagement");
        }
        #endregion
        /////////////////

        /////////////////
        #region UpdateState
        [Route("/OrderManagement/UpdateState")]
        public IActionResult UpdateStateOrder(long OrderId, Order.state State)
        {
            bl_Order bl_Order = new bl_Order();
            bl_Order.UpdateStateOrder(OrderId, State);
            return Redirect("/ReadOrder/" + OrderId);
        }
        #endregion
        /////////////////

        /////////////////
        #region PrintPage
        public IActionResult PrintPage()
        {
            return View();
        }

        class amir
        {
            public string name { get; set; }
            public string family { get; set; }
            public string age { get; set; }
        }
        public IActionResult Print()
        {
            StiReport stiReport = new StiReport();
            stiReport.Load(StiNetCoreHelper.MapPath(this, @"\wwwroot\Factor\Report.mrt"));
            amir amir = new amir()
            {
                name = "amir",
                family = "kaboli",
                age = "18",
            };
            stiReport.RegData("amir", amir);
            return StiNetCoreViewer.GetReportResult(this, stiReport);
        }
        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
        #endregion
        /////////////////

    }
}
