using MusicShop.DAL;
using MusicShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicShop.Infrastructure
{
    public class ShoppingCartManager
    {
        private StoreContext db;
        private ISessionManager session;

        public const string CartSessionKey = "CarData";

        public ShoppingCartManager(ISessionManager session, StoreContext db )
        {
            this.session = session;
            this.db = db;
        }

        public void AddToCart(int albumId)
        {
            var cart = this.GetCart();
            var cartItem = cart.Find(c => c.Album.AlbumId == albumId);

            if (cartItem != null)
                cartItem.Quantity++;
            else
            {
                var albumToAdd = db.Albums.Where(a => a.AlbumId == albumId).SingleOrDefault();
                if( albumToAdd != null )
                {
                    var newCartItem = new CartItems()
                    {
                        Album = albumToAdd,
                        Quantity = 1,
                        TotalPrice = albumToAdd.Price
                    };

                    cart.Add(newCartItem);
                }
            }

            session.Set(CartSessionKey, cart);
        }

        public List<CartItems> GetCart()
        {
            List<CartItems> cart;

            if( session.Get<List<CartItems>>(CartSessionKey) == null)
            {
                cart = new List<CartItems>();
            }
            else
            {
                cart = session.Get<List<CartItems>>(CartSessionKey) as List<CartItems>;
            }

            return cart;
        }

        public int RemoveFromCart (int albumId)
        {
            var cart = this.GetCart();
            var cartItem = cart.Find(c => c.Album.AlbumId == albumId);

            if( cartItem != null )
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    return cartItem.Quantity;
                }
                else
                    cart.Remove(cartItem);
            }

            return 0;
        }

        public decimal GetCartTotalPrice()
        {
            var cart = this.GetCart();
            return cart.Sum(c => (c.Quantity * c.Album.Price));
        }

        public int GetCartItemsCount()
        {
            var cart = this.GetCart();
            int count = cart.Sum(c => c.Quantity);

            return count;
        }

        public Order CreateOrder(Order newOrder, string userId)
        {
            var cart = this.GetCart();

            newOrder.DateCreated = DateTime.Now;
            //newOrder.UserId = userId;

            this.db.Orders.Add(newOrder);

            if (newOrder.OrderItems == null)
                newOrder.OrderItems = new List<OrderItem>();

            decimal cartTotal = 0;

            foreach (var cartItem in cart)
            {
                var newOrderItem = new OrderItem()
                {
                    AlbumId = cartItem.Album.AlbumId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Album.Price
                };

                cartTotal += (cartItem.Quantity * cartItem.Album.Price);
                newOrder.OrderItems.Add(newOrderItem);
            }

            newOrder.TotalPrice = cartTotal;
            this.db.SaveChanges();

            return newOrder;
        }

        public void EmptyCart()
        {
            session.Set<List<CartItems>>(CartSessionKey, null);
        }
    }
}