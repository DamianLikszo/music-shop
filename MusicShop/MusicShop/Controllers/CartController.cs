using MusicShop.DAL;
using MusicShop.Infrastructure;
using MusicShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicShop.Controllers
{
    public class CartController : Controller
    {
        private ShoppingCartManager shoppingCartManager { get; set; }
        private ISessionManager SessionManager { get; set; }
        private StoreContext db = new StoreContext();

        public CartController()
        {
            this.SessionManager = new SessionManager();
            this.shoppingCartManager = new ShoppingCartManager(this.SessionManager, db);
        }

        public ActionResult Index()
        {
            var cartItems = shoppingCartManager.GetCart();
            var cartTotalPrice = shoppingCartManager.GetCartTotalPrice();

            CartViewModel cartVM = new CartViewModel() { CartItems = cartItems, TotalPrice = cartTotalPrice };

            return View(cartVM);
        }

        public ActionResult AddToCart(int id)
        {
            shoppingCartManager.AddToCart(id);

            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public int GetCartItemsCount()
        {
            return shoppingCartManager.GetCartItemsCount();
        }

        public ActionResult RemoveFromCart(int albumId)
        {
            int itemCount = shoppingCartManager.RemoveFromCart(albumId);
            int cartItemsCount = shoppingCartManager.GetCartItemsCount();
            decimal cartTotal = shoppingCartManager.GetCartTotalPrice();

            // Return JSON
            var result = new CartRemoveViewModel
            {
                RemoveItemId = albumId,
                RemovedItemCount = itemCount,
                CartTotal = cartTotal,
                CartItemsCount = cartItemsCount
            };

            return Json(result);
        }
    }
}