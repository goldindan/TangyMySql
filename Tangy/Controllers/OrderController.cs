using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
using Tangy.Models;
using Tangy.Models.OrderDetailsViewModel;
using Tangy.Utility;

namespace Tangy.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;
        private int PageSize = 2;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET: Confirm
        [Authorize]
        public async Task<IActionResult> Confirm(int id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel()
            {
                OrderHeader = _db.OrderHeader.Where(o => o.Id == id && o.UserId == claim.Value).FirstOrDefault(),
                OrderDetail = _db.OrderDetails.Where(o => o.OrderId == id).ToList()
            };
            return View(orderDetailsViewModel);
        }

        [Authorize]
        public IActionResult OrderHistory(int productPage=1)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            OrderListViewModel OrderListVM = new OrderListViewModel()
            {
                Orders = new List<OrderDetailsViewModel>()
            };


            List<OrderHeader> orderHeaders = _db.OrderHeader.Where(oh => oh.UserId == claim.Value).OrderByDescending(o => o.OrderDate).ToList();

            foreach (var orderHeader in orderHeaders)
            {
                List<OrderDetails> orderDetails = _db.OrderDetails.Where(od => od.OrderId == orderHeader.Id).ToList();
                OrderListVM.Orders.Add(new OrderDetailsViewModel()
                {
                    OrderHeader = orderHeader,
                    OrderDetail = orderDetails
                });
            }

            var count = OrderListVM.Orders.Count;
            OrderListVM.Orders = OrderListVM.Orders.OrderBy(o => o.OrderHeader.Id)
                                                   .Skip((productPage - 1) * PageSize)
                                                   .Take(PageSize).ToList();

            OrderListVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = count
            };

            return View(OrderListVM);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        public IActionResult ManageOrder()
        {
            List<OrderDetailsViewModel> OrderDetailsVM = new List<OrderDetailsViewModel>();

            List<OrderHeader> orderHeaders = _db.OrderHeader.Where(oh => oh.Status.Equals(SD.StatusSubmitted) || oh.Status.Equals(SD.StatusInProcess))
                .OrderByDescending(o => o.PickUpTime).ToList();

            foreach (var orderHeader in orderHeaders)
            {
                List<OrderDetails> orderDetails = _db.OrderDetails.Where(od => od.OrderId == orderHeader.Id).ToList();
                OrderDetailsVM.Add(new OrderDetailsViewModel()
                {
                    OrderHeader = orderHeader,
                    OrderDetail = orderDetails
                });
            }
            return View(OrderDetailsVM);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        public async Task<IActionResult> OrderPrepared(int orderId)
        {
            OrderHeader orderHeader = _db.OrderHeader.Find(orderId);
            orderHeader.Status = SD.StatusInProcess;
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageOrder", "Order");
        }

        [Authorize(Roles = SD.AdminEndUser)]
        public async Task<IActionResult> OrderCancel(int orderId)
        {
            OrderHeader orderHeader = _db.OrderHeader.Find(orderId);
            orderHeader.Status = SD.StatusCanceled;
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageOrder", "Order");
        }

        [Authorize(Roles = SD.AdminEndUser)]
        public async Task<IActionResult> OrderReady(int orderId)
        {
            OrderHeader orderHeader = _db.OrderHeader.Find(orderId);
            orderHeader.Status = SD.StatusReady;
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageOrder", "Order");
        }


        [Authorize(Roles = SD.AdminEndUser)]
        public IActionResult OrderPickup(string searchEmail = null, string searchPhone = null, string searchOrder = null)
        {
            List<OrderDetailsViewModel> OrderDetailsVM = new List<OrderDetailsViewModel>();

            if (searchEmail != null || searchPhone != null || searchOrder != null)
            {
                //filter criteria
                var user = new ApplicationUser();
                List<OrderHeader> OrderHeaderList = new List<OrderHeader>();

                if (searchOrder != null)
                {
                    OrderHeaderList = _db.OrderHeader.Where(o => o.Id == Int32.Parse(searchOrder)).ToList();
                }
                else
                {
                    if (searchEmail != null)
                    {
                        user = _db.Users.Where(u => u.Email.ToLower().Contains(searchEmail.ToLower())).FirstOrDefault();
                    }
                    else
                    {
                        user = _db.Users.Where(u => u.PhoneNumber.Contains(searchPhone)).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        OrderHeaderList = _db.OrderHeader.Where(oh => oh.UserId == user.Id).ToList();
                    }
                }
                if (OrderHeaderList != null && OrderHeaderList.Count > 0)
                {
                    foreach (var orderHeader in OrderHeaderList)
                    {
                        List<OrderDetails> orderDetails = _db.OrderDetails.Where(od => od.OrderId == orderHeader.Id).ToList();
                        OrderDetailsVM.Add(new OrderDetailsViewModel()
                        {
                            OrderHeader = orderHeader,
                            OrderDetail = orderDetails
                        });
                    }
                }
            }
            else
            {
                List<OrderHeader> orderHeaders = _db.OrderHeader.Where(oh => oh.Status.Equals(SD.StatusReady))
                    .OrderByDescending(o => o.PickUpTime).ToList();

                foreach (var orderHeader in orderHeaders)
                {
                    List<OrderDetails> orderDetails = _db.OrderDetails.Where(od => od.OrderId == orderHeader.Id).ToList();
                    OrderDetailsVM.Add(new OrderDetailsViewModel()
                    {
                        OrderHeader = orderHeader,
                        OrderDetail = orderDetails
                    });
                }
            }
            return View(OrderDetailsVM);
        }


        [Authorize(Roles = SD.AdminEndUser)]
        public IActionResult OrderPickupDetails(int orderId)
        {
            OrderDetailsViewModel OrderDetailsVM = new OrderDetailsViewModel()
            {
                OrderHeader = _db.OrderHeader.Include(oh => oh.ApplicationUser).Where(oh => oh.Id == orderId).FirstOrDefault(),
                OrderDetail = _db.OrderDetails.Where(od => od.OrderId == orderId).ToList()
            };

            return View(OrderDetailsVM);
        }

        [HttpPost]
        [Authorize(Roles = SD.AdminEndUser)]
        [ActionName("OrderPickupDetails")]
        public async Task<IActionResult> OrderPickupDetailsPOST(int orderId)
        {
            OrderHeader orderHeader = _db.OrderHeader.Find(orderId);
            orderHeader.Status = SD.StatusCompleted;
            await _db.SaveChangesAsync();
            return RedirectToAction("OrderPickup", "Order");
        }
    }
}