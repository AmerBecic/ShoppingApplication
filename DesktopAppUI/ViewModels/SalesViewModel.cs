using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DesktopAppUI.Library.Api;
using DesktopAppUI.Library.Models;

namespace DesktopAppUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private readonly IProductApi _productApi;
        public SalesViewModel(IProductApi productApi)
        {
            _productApi = productApi;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productList = await _productApi.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        private BindingList<ProductModel> _products;
        private int _productQuantity = 1;
        private BindingList<string> _cart = new BindingList<string>();

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        public int ProductQuantity
        {
            get { return _productQuantity; }
            set
            {
                _productQuantity = value;
                NotifyOfPropertyChange(() => ProductQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public BindingList<string> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                //Make sure something is selected
                //Make sure tere is an product quantity

                //if (ProductQuantity > 0 && SelectedProduct?.QuantityInStock >= ProductQuantity)
                //{
                //    output = true;
                //}

                return output;
            }
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                //if (SelectedCartProduct != null && SelectedCartProduct?.QuantityInCart > 0)
                //{
                //    output = true;
                //}

                return output;
            }
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                if (Cart.Count > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public string SubTotal
        {
            get
            {
                return CalculateSubTotal().ToString("C");
            }
        }

        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;

            //foreach (var product in Cart)
            //{
            //    subTotal += product.Product.RetailPrice * product.QuantityInCart;
            //}
            return subTotal;
        }

        public string Tax
        {
            get
            {
                return CalculateTax().ToString("C");
            }
        }

        private decimal CalculateTax()
        {
            decimal tax = 0;
            //decimal taxRate = _configHelper.GetTaxRate();

            //tax = Cart
            //    .Where(x => x.Product.IsTaxable)
            //    .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);

            //foreach (var product in Cart)
            //{
            //    if (product.Product.IsTaxable)
            //    {
            //        tax += product.Product.RetailPrice * product.QuantityInCart * taxRate;
            //    }
            //}

            return tax;
        }

        public string Total
        {
            get
            {
                decimal total = CalculateSubTotal() + CalculateTax();
                return total.ToString("C");
            }
        }
    }
}
