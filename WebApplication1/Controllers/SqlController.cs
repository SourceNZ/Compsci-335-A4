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
                    .OrderBy(sortCol + " " + sortDir + ", " + "ProductID" + " " + sortDir)
                    .Skip((page - 1) * rowsPerPage)
                    .Take(rowsPerPage)
                    .Select(o => new MyModel
                    {
                        ProductID = o.ProductID,
                        ProductName = o.ProductName,
                        SupplierID = !o.SupplierID.HasValue ? (int?)null : (int?)o.SupplierID,
                        CategoryID = !o.CategoryID.HasValue ? (int?)null : (int?)o.CategoryID,
                       // SupplierID = o.SupplierID,
                       // CategoryID = o.CategoryID,
                        CategoryName = !o.CategoryID.HasValue ? (string)null : (string) o.Category.CategoryName,
                        UnitPrice = o.UnitPrice,
                        UnitsInStock = o.UnitsInStock,
                        UnitsOnOrder = o.UnitsOnOrder,
                        CompanyName = !o.SupplierID.HasValue ? (string)null : (string)o.Supplier.CompanyName,
                        ContactName = !o.SupplierID.HasValue ? (string)null : (string)o.Supplier.ContactName,
                        Country = o.Supplier.Country,

                    });
                
                res = _res.ToList();
                count = nwd.Products.Count();
                sql = nwd.Products.AsQueryable().OrderBy(sortCol + " " + sortDir + ", " + "ProductID" + " " + sortDir).Skip((page - 1) * rowsPerPage).Take(rowsPerPage).ToString();
                ViewBag.sql = sql;
                
            }
            //Sorting by the supplier and category can return nulls so need to account for that.

            ViewBag.sortDir = sortDir;
            ViewBag.sql = sql;
            ViewBag.sortCol = sortCol;
            ViewBag.rowsPerPage = rowsPerPage;
            ViewBag.count = count;
            return View(res);
        }
    }
}


// var query = from item in nwd.Products
//           orderby (sortCol + " " + sortDir + ", " + "ProductID" + " " + sortDir) 
//            select item;
//var topTwo = query.Take(rowsPerPage);


//sql = topTwo.ToString();
//sql = nwd.Products.AsQueryable().Where()
//sql = nwd.Products.OrderBy(o => o.ProductID ).ThenBy(o => o.ProductName).ToString();


