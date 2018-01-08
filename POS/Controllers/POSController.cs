using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using POS.Models;

namespace POS.Controllers
{
    public class POSController : Controller
    {
        private ApplicationDbContext context;

        public POSController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Customer()
        {
            var items = context.Stocks.Include(t => t.Item).ToList();
            return View(items);
        }

        //[Route("POS/ListingItems/{ItemId}")]
        public ActionResult ListingItems(int ItemId, int quantity)
        {
            var checking = context.Stocks.FirstOrDefault(p => p.ItemId == ItemId);
            if (checking.Quantity < quantity)
            {
                return RedirectToAction("Customer");
            }
            else
            {
                var listingItems = new BoughtItem();
                var checkIfExists = context.BoughtItems.FirstOrDefault(p => p.ItemId == ItemId);

                checkIfExists.Quantity += quantity;
                checking.Quantity -= quantity;
                context.SaveChanges();
                return RedirectToAction("Customer");

                /*
                if (checkIfExists != null)
                {
                    checkIfExists.Quantity += quantity;
                    checking.Quantity -= quantity;
                    context.SaveChanges();
                    return RedirectToAction("Customer");
                }
                else
                {
                    listingItems.ItemId = ItemId;
                    listingItems.Quantity = quantity;
                    context.BoughtItems.Add(listingItems);
                    checking.Quantity -= quantity;
                    context.SaveChanges();
                    return RedirectToAction("Customer");
                } */
            }
        }

        public ActionResult IncreaseStockSuccess(int ItemId, int quantity)
        {
            var stockid = context.Stocks.FirstOrDefault(c => c.ItemId == ItemId);

            stockid.Quantity += quantity;
            return RedirectToAction("Customer");
        }

        public ActionResult AddNewItem()
        {
            return View();
        }
        public ActionResult IncreaseStock()
        {
            return View();
        }
        public ActionResult SuccessfullyAddedNewItem(string itemName, int itemPrice, int itemQuentity)
        {
            var newItem = new Item();
            var newItemStock = new Stock();
            var q = context.Items.FirstOrDefault(x => x.Name == itemName);
            
            if(q == null)
            {
                newItem.Name = itemName;
                newItem.Price = itemPrice;
                newItemStock.Quantity = itemQuentity;
                context.Items.Add(newItem);
                context.SaveChanges();
                var getId = context.Items.FirstOrDefault(c => c.Name == itemName);
                newItemStock.Quantity = itemQuentity;
                newItemStock.ItemId = getId.Id;
                context.Stocks.Add(newItemStock);
                context.SaveChanges();
                return RedirectToAction("Customer");
                var showAllItems = context.Stocks.Include(c => c.Item).ToList();
                //ViewBag.msg = showAllItems;
                return View(showAllItems);
            }
            else
            {
                return RedirectToAction("AlreadyExist");
            }
        }

        public ActionResult AlreadyExist()
        {
            return View();
        }
        public ActionResult AddItems()
        {
            return View();
        }
        //[Authorize]
        public ActionResult Checkout()
        {
            var allSelectedItems = context.BoughtItems.Include(c => c.Item).ToList();
            return View(allSelectedItems);
        }

        /*public ActionResult Checkout()
        {
            var clearCart = context.BoughtItems.ToList();
            context.BoughtItems.RemoveRange(clearCart);
            context.SaveChanges();
            return RedirectToAction("Customer");
        }*/
    }
}