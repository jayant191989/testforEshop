using HR_Management.Context;
using HR_Management.Models;
using HR_Management.ShoppingCartBL.ViewModels;
using HR_Management.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HR_Management.ShoppingCartBL
{
    public class CartBL
    {
        public Guid AddCart(CartViewModel model)
        {
            Guid cartId;
            UserCart cart = new UserCart();
            cart.Id = Guid.NewGuid();
            cart.UserId = model.UserId;
            cart.ProductId = model.ProductId;

            if (model.Qty == 0)
            {
                cart.Qty = 1;
            }
            else
            {
                cart.Qty = model.Qty;
            }
            cart.SessionId = model.SessionId;
            //  if ((model.ShippingCharges ?? 0) == 0) { cart.ShippingCharges = 0; } else { cart.ShippingCharges = model.ShippingCharges; }           

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                UserCart crt = db.UserCarts.Where(w => w.ProductId == model.ProductId && w.SessionId == model.SessionId).FirstOrDefault();
                UserCart userCart = new UserCart(); ;
                if (model.UserId != null)
                {
                    userCart = db.UserCarts.Where(w => w.ProductId == model.ProductId && w.UserId == model.UserId).FirstOrDefault();
                }

                if (userCart != null)
                {
                    userCart.Qty += 1;
                    db.SaveChanges();
                    cartId = userCart.Id;
                }
                else if (crt != null)
                {
                    crt.Qty += 1;
                    db.SaveChanges();
                    cartId = crt.Id;
                }
                else
                {
                    db.UserCarts.Add(cart);
                    db.SaveChanges();
                    cartId = cart.Id;
                }
            }
            return cartId;
        }

        public List<CartViewModel> GetCartlist(string SessionId, ApplicationUser user)
        {
            List<CartViewModel> cartList = new List<CartViewModel>();

            using (var db = new ApplicationDbContext())
            {
                if (user != null)
                {
                    var userCarts = db.UserCarts.Where(wl => wl.UserId == user.Id).ToList();
                    foreach (var p in userCarts)
                    {
                        CartViewModel viewModel = new CartViewModel();
                        viewModel.Id = p.Id;
                        viewModel.ProductId = p.ProductId;
                        viewModel.ProductName = p.Product.Name;
                        viewModel.Title = p.Product.Title;
                        viewModel.ProductImage = p.Product.ThumbMainImageName;
                        viewModel.CategoryName = p.Product.ProductCategory.Name;
                        viewModel.Qty = p.Qty;
                        viewModel.ShippingCharges = p.Product.StandardShippingCharges;
                        // ThumbMainImagePath = "Images/" + p.Product.ProductCategory.Name + "/" + p.Product.ThumbMainImageName,
                        //  viewModel.MRPPerUnit = p.m,                    
                        viewModel.DisPer = p.Product.DiscountPerUnitInPercent;
                        viewModel.SalePrice = p.Product.SalePrice;
                        cartList.Add(viewModel);
                    }
                }
                else
                {
                    var userCarts = db.UserCarts.Where(wl => wl.SessionId == SessionId).ToList();
                    foreach (var p in userCarts)
                    {
                        CartViewModel viewModel = new CartViewModel();
                        viewModel.Id = p.Id;
                        viewModel.ProductName = p.Product.Name;
                        viewModel.Title = p.Product.Title;
                        viewModel.ProductImage = p.Product.ThumbMainImageName;
                        viewModel.Qty = p.Qty;
                        // ThumbMainImagePath = "Images/" + p.Product.ProductCategory.Name + "/" + p.Product.ThumbMainImageName,
                        //  viewModel.MRPPerUnit = p.m,                    
                        viewModel.DisPer = p.Product.DiscountPerUnitInPercent;
                        viewModel.SalePrice = p.Product.SalePrice;
                        cartList.Add(viewModel);
                    }
                }
            }
            return cartList;
        }

        public int UpdateCart(Guid? cartId, string SessionId, ApplicationUser user, int Qty)
        {
            int result = 0;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                UserCart cart = db.UserCarts.Where(w => w.Id == cartId && w.SessionId == SessionId).FirstOrDefault();
                UserCart userCart = null;
                if (user != null)
                {
                    userCart = db.UserCarts.Where(w => w.Id == cartId && w.UserId == user.Id).FirstOrDefault();
                }
                if (userCart != null)
                {
                    userCart.Qty = Qty;
                    db.SaveChanges();
                    result = 1;
                }
                else if (cart != null)
                {
                    cart.Qty = Qty;
                    db.SaveChanges();
                    result = 1;
                }
            }
            return result;
        }

        public Int64 UpdateCartWithUserId(string SessionId, string UserId)
        {
            Int64 cartId = 0;
            using (var txn = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        var cart = db.UserCarts.Where(w => w.SessionId == SessionId).ToList();
                        if (cart != null)
                        {
                            foreach (var c in cart)
                            {
                                c.UserId = UserId;
                                db.SaveChanges();
                            }
                            cartId = 1;
                            txn.Complete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            return cartId;
        }

        public int DeleteCart(Guid cartId, string SessionId, ApplicationUser user)
        {
            int value = 0;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                UserCart cart = db.UserCarts.Where(w => w.Id == cartId && w.SessionId == SessionId).FirstOrDefault();
                UserCart ct = null;
                if (user != null)
                {
                    ct = db.UserCarts.Where(w => w.Id == cartId && w.UserId == user.Id).FirstOrDefault();
                }
                if (ct != null)
                {
                    db.UserCarts.Remove(ct);
                    db.SaveChanges();
                    value = 1;
                }
                else if (cart != null)
                {
                    db.UserCarts.Remove(cart);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }
    }
}
