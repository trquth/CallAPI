using SSC.Core.Api.Models.SSC;
using SSC.StudyRecords.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TQTCallAPI.ViewModel;

namespace TQTCallAPI.Common
{

    public class ProductApiHelper
    {
        public static List<ProductViewModel> GetAllProduct()
        {
            return  WebApiHelper.GetListCoreObject<ProductViewModel>("product/getlistproducts", new TheSscRequest());
        }
    }
}