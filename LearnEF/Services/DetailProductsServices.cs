using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LearnEF.Models;

namespace LearnEF.Services
{
    public class DetailProductsServices
    {
        public IQueryable<Detailproduct> getDetail(ProductsDBEntities db)
        {
            //IQueryable<Detailproduct> res = db.Detailproducts.Where(x => x.Vendor.Contains("a"));
            IQueryable<Detailproduct> res = db.Detailproducts.OrderBy(x => x.Price);
            return res;
        }
    }
}