using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Linq.Dynamic;


namespace WebApplication1.Controllers
{
    public class SqlController : Controller
    {
        //GET: Sql
        public ActionResult Index()
        {

            return View();
        }

        // GET: WebGrid?page=1&rowsPerPage=3&sort=OrderID&sortDir=ASC
        public ActionResult WebGrid(int page = 1, int rowsPerPage = 10, string sortCol = "ProductID", string sortDir = "ASC")
        {
            List<MyModel> res;
            int count;
            string sql;

            using (var nwd = new NorthwindEntities())
            {
                var _res = nwd.Products
                    .OrderBy(sortCol + " " + sortDir)
                    .Skip((page - 1) * rowsPerPage)
                    .Take(rowsPerPage)
                    .Select(o => new MyModel
                    {
                        ProductID = o.ProductID,
                        ProductName = o.ProductName,
                        SupplierID = o.SupplierID,
                        CategoryID = o.CategoryID,
                        CategoryName = o.Category.CategoryName,
                        UnitPrice = o.UnitPrice,
                        UnitsInStock = o.UnitsInStock,
                        UnitsOnOrder = o.UnitsOnOrder,
                        CompanyName = o.Supplier.CompanyName,
                        ContactName = o.Supplier.ContactName,
                        Country = o.Supplier.Country,

                    });
                
                res = _res.ToList();
                count = nwd.Products.Count();
                
            }

            ViewBag.sortCol = sortCol;
            ViewBag.rowsPerPage = rowsPerPage;
            ViewBag.count = count;
            return View(res);
        }
    }
}





