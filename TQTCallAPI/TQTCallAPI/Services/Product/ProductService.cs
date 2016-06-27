using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TQTCallAPI.Common;
using TQTCallAPI.ViewModel;

namespace TQTCallAPI.Services.Product
{
    interface IProductService
    {
        List<ProductViewModel> GetListProducts();
    }
    public class ProductService : IProductService
    {
        public List<ProductViewModel> GetListProducts()
        {
            return ProductApiHelper.GetAllProduct();
        }
    }
}