﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAppUI.Library.Models
{
    public class CartProductModel
    {
        public ProductModel Product { get; set; }
        public int QuantityInCart { get; set; }
    }
}
