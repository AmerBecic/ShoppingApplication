using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Caliburn.Micro;
using DesktopAppUI.Library.Api;
using DesktopAppUI.Library.Helpers;
using DesktopAppUI.Library.Models;
using DesktopAppUI.Models;

namespace DesktopAppUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private readonly IProductApi _productApi;
        private readonly ISaleApi _saleApi;
        private readonly IConfigHelper _configHelper;
        private readonly IMapper _mapper;
        public SalesViewModel(IProductApi productApi, ISaleApi saleApi, IConfigHelper configHelper, IMapper mapper)
        {
            _mapper = mapper;
            _configHelper = configHelper;
            _productApi = productApi;
            _saleApi = saleApi;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productList = await _productApi.GetAll();
            var products = _mapper.Map<List<ProductDisplayModel>>(productList);
            Products = new BindingList<ProductDisplayModel>(products);
        }

        private BindingList<ProductDisplayModel> _products;
        private int _productQuantity = 1;
        private ProductDisplayModel _selectedProduct;
        private BindingList<CartProductDisplayModel> _cart = new BindingList<CartProductDisplayModel>();


        public BindingList<ProductDisplayModel> Products
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

        public BindingList<CartProductDisplayModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }
        public ProductDisplayModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public void RemoveFromCart()
        {
            //SelectedCartProduct.Product.QuantityInStock += 1;

            //if (SelectedCartProduct.QuantityInCart > 1)
            //{
            //    SelectedCartProduct.QuantityInCart -= 1;
            //}

            //else
            //{
            //    Cart.Remove(SelectedCartProduct);
            //}

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanAddToCart);
        }

        public void AddToCart()
        {
            CartProductDisplayModel existingProduct = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

            if (existingProduct != null)
            {
                existingProduct.QuantityInCart += ProductQuantity;
                Cart.Remove(existingProduct);
                Cart.Add(existingProduct);
            }

            else
            {
                CartProductDisplayModel Product = new CartProductDisplayModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ProductQuantity
                };
                Cart.Add(Product);
            }

            SelectedProduct.QuantityInStock -= ProductQuantity;
            ProductQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public async Task CheckOut()
        {
            //Create a SaleModel and post to the API
            SaleModel sale = new SaleModel();
            foreach (var product in Cart)
            {
                sale.SaleDetails.Add(new SaleDetailModel
                {
                    ProductId = product.Product.Id,
                    Quantity = product.QuantityInCart
                });
            }

            await _saleApi.PostSale(sale);

            //await ResetSalesViewModel();
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                //Make sure something is selected
                //Make sure tere is an product quantity

                if (ProductQuantity > 0 && SelectedProduct?.QuantityInStock >= ProductQuantity)
                {
                    output = true;
                }

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

            foreach (var product in Cart)
            {
                subTotal += product.Product.RetailPrice * product.QuantityInCart;
            }
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
            decimal taxRate = _configHelper.GetTaxRate();

            tax = Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);

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
