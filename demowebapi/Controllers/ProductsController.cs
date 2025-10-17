using demowebapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demowebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly NorthwindContext _northwindContext;
        
        public ProductsController(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        [HttpGet("exercise1")]
        public IActionResult Exercise1()
        {
            var employees = _northwindContext.Employees
                .Select(e => new
                {
                    FullName = (e.LastName + " " + e.FirstName).ToLower(),
                    e.TitleOfCourtesy
                })
                .ToList();
            return Ok(employees);
        }


        [HttpGet("exercise2")]
        public IActionResult Exercise2()
        {
            var employees = _northwindContext.Employees
                .Select(e => new
                {
                    FullName = (e.LastName + " " + e.FirstName).ToUpper()
                })
                .ToList();

            return Ok(employees);
        }


        [HttpGet("exercise3")]
        public IActionResult Exercise3()
        {
            var employees = _northwindContext.Employees
                .Where(e => e.Country == "USA")
                .Select(e => new
                {
                    e.EmployeeId,
                    e.LastName,
                    e.FirstName,
                    e.Title,
                    e.City,
                    e.Country
                })
                .ToList();

            return Ok(employees);
        }


        [HttpGet("exercise4")]
        public IActionResult Exercise4()
        {
            var customers = _northwindContext.Customers
                .Where(c => c.Country == "UK")
                .Select(c => new
                {
                    c.CustomerId,
                    c.CompanyName,
                    c.ContactName,
                    c.ContactTitle,
                    c.Country
                })
                .ToList();

            return Ok(customers);
        }


        [HttpGet("exercise5")]
        public IActionResult Exercise5()
        {
            var customers = _northwindContext.Customers
                .Where(c => c.Country == "Mexico")
                .Select(c => new
                {
                    c.CustomerId,
                    c.CompanyName,
                    c.Address,
                    c.City,
                    c.Country
                })
                .ToList();

            return Ok(customers);
        }


        [HttpGet("exercise6")]
        public IActionResult Exercise6()
        {
            var customers = _northwindContext.Customers
                .Where(c => c.Country == "Sweden")
                .Select(c => new
                {
                    c.CustomerId,
                    c.CompanyName,
                    c.Phone,
                    c.Address,
                    c.City,
                    c.Country
                })
                .ToList();

            return Ok(customers);
        }


        [HttpGet("exercise7")]
        public IActionResult Exercise7()
        {
            var products = _northwindContext.Products
                .Where(p => p.UnitsInStock >= 5 && p.UnitsInStock <= 10)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.UnitPrice,
                    p.UnitsInStock
                })
                .ToList();

            return Ok(products);
        }


        [HttpGet("exercise8")]
        public IActionResult Exercise8()
        {
            var products = _northwindContext.Products
                .Where(p => p.UnitsOnOrder >= 60 && p.UnitsOnOrder <= 100)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.UnitPrice,
                    p.ReorderLevel,
                    p.UnitsOnOrder
                })
                .ToList();

            return Ok(products);
        }


        [HttpGet("exercise9")]
        public IActionResult Exercise9()
        {
            var results = _northwindContext.Orders
                .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1996)
                .GroupBy(o => new
                {
                    o.Employee.EmployeeId,
                    o.Employee.LastName,
                    o.Employee.FirstName,
                    o.Employee.Title
                })
                .Select(g => new
                {
                    g.Key.EmployeeId,
                    g.Key.LastName,
                    g.Key.FirstName,
                    g.Key.Title,
                    Year = 1996,
                    TotalOrders = g.Count()
                })
                .OrderBy(x => x.EmployeeId)
                .ToList();

            return Ok(results);
        }


        [HttpGet("exercise10")]
        public IActionResult Exercise10()
        {
            var results = _northwindContext.Orders
                .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1998)
                .GroupBy(o => new
                {
                    o.Employee.EmployeeId,
                    o.Employee.LastName,
                    o.Employee.FirstName,
                    o.Employee.City,
                    o.Employee.Country
                })
                .Select(g => new
                {
                    g.Key.EmployeeId,
                    g.Key.LastName,
                    g.Key.FirstName,
                    g.Key.City,
                    g.Key.Country,
                    TotalOrders = g.Count()
                })
                .OrderBy(x => x.EmployeeId)
                .ToList();

            return Ok(results);
        }


        [HttpGet("exercise11")]
        public IActionResult Exercise11()
        {
            DateTime startDate = new DateTime(1998, 1, 1);
            DateTime endDate = new DateTime(1998, 7, 31);

            var results = _northwindContext.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .GroupBy(o => new
                {
                    o.Employee.EmployeeId,
                    o.Employee.LastName,
                    o.Employee.FirstName,
                    o.Employee.City,
                    o.Employee.Country
                })
                .Select(g => new
                {
                    g.Key.EmployeeId,
                    g.Key.LastName,
                    g.Key.FirstName,
                    g.Key.City,
                    g.Key.Country,
                    TotalOrders = g.Count()
                })
                .OrderBy(x => x.EmployeeId)
                .ToList();

            return Ok(results);
        }


        [HttpGet("exercise12")]
        public IActionResult Exercise12()
        {
            DateTime startDate = new DateTime(1997, 1, 1);
            DateTime endDate = new DateTime(1997, 6, 30);

            var results = _northwindContext.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .GroupBy(o => new
                {
                    o.Employee.EmployeeId,
                    o.Employee.LastName,
                    o.Employee.FirstName,
                    o.Employee.City,
                    o.Employee.Country
                })
                .Select(g => new
                {
                    g.Key.EmployeeId,
                    g.Key.LastName,
                    g.Key.FirstName,
                    g.Key.City,
                    g.Key.Country,
                    TotalOrders = g.Count()
                })
                .OrderBy(x => x.EmployeeId)
                .ToList();

            return Ok(results);
        }


        [HttpGet("exercise13")]
        public IActionResult Exercise13()
        {
            DateTime startDate = new DateTime(1996, 8, 1);
            DateTime endDate = new DateTime(1996, 8, 5);

            var result = _northwindContext.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .Select(o => new
                {
                    o.OrderId,
                    o.OrderDate,
                    o.Freight,
                    FreightWithTax = o.Freight >= 100 ? o.Freight * 1.10m : o.Freight * 1.05m
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise14")]
        public IActionResult Exercise14()
        {
            var result = _northwindContext.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    FullName = e.LastName + " " + e.FirstName,
                    e.TitleOfCourtesy,
                    Sex = e.TitleOfCourtesy == "Mr." ? "Male" :
                          (e.TitleOfCourtesy == "Ms." || e.TitleOfCourtesy == "Mrs.") ? "Female" : "Unknown"
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise15")]
        public IActionResult Exercise15()
        {
            var result = _northwindContext.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    FullName = e.LastName + " " + e.FirstName,
                    e.TitleOfCourtesy,
                    Sex = (e.TitleOfCourtesy == "Mr." || e.TitleOfCourtesy == "Dr.") ? "M" :
                          (e.TitleOfCourtesy == "Ms." || e.TitleOfCourtesy == "Mrs.") ? "F" : "Unknown"
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise16")]
        public IActionResult Exercise16()
        {
            var result = _northwindContext.Employees
                .Select(e => new
                {
                    FullName = e.LastName + " " + e.FirstName,
                    e.TitleOfCourtesy,
                    Sex = e.TitleOfCourtesy == "Mr." ? "Male" :
                          (e.TitleOfCourtesy == "Ms." || e.TitleOfCourtesy == "Mrs.") ? "Female" :
                          "Unknown"
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise17")]
        public IActionResult Exercise17()
        {
            var result = _northwindContext.Employees
                .Select(e => new
                {
                    FullName = e.LastName + " " + e.FirstName,
                    e.TitleOfCourtesy,
                    Sex = e.TitleOfCourtesy == "Mr." ? 1 :
                          (e.TitleOfCourtesy == "Ms." || e.TitleOfCourtesy == "Mrs.") ? 0 : 2
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise18")]
        public IActionResult Exercise18()
        {
            var result = _northwindContext.Employees
                .Select(e => new
                {
                    FullName = e.LastName + " " + e.FirstName,
                    e.TitleOfCourtesy,
                    Sex = e.TitleOfCourtesy == "Mr." ? "M" :
                          (e.TitleOfCourtesy == "Ms." || e.TitleOfCourtesy == "Mrs.") ? "F" : "N/A"
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise21")]
        public IActionResult Exercise21()
        {
            var result = _northwindContext.OrderDetails
                .Join(_northwindContext.Orders,
                      od => od.OrderId,
                      o => o.OrderId,
                      (od, o) => new { od, o })
                .Join(_northwindContext.Products,
                      x => x.od.ProductId,
                      p => p.ProductId,
                      (x, p) => new { x.od, x.o, p })
                .Join(_northwindContext.Categories,
                      x => x.p.CategoryId,
                      c => c.CategoryId,
                      (x, c) => new { x.od, x.o, x.p, c })
                .Where(x => x.o.OrderDate >= new DateTime(1996, 7, 1)
                         && x.o.OrderDate <= new DateTime(1996, 7, 5))
                .GroupBy(x => new { x.c.CategoryId, x.p.ProductId, x.p.ProductName })
                .Select(g => new
                {
                    g.Key.CategoryId,
                    g.Key.ProductId,
                    g.Key.ProductName,
                    Revenue = g.Sum(x => x.od.Quantity * x.od.UnitPrice)
                })
                .OrderBy(r => r.CategoryId)
                .ThenBy(r => r.ProductId)
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise22")]
        public IActionResult Exercise22()
        {
            var result = _northwindContext.Orders
                .Where(o => o.ShippedDate != null && o.RequiredDate != null &&
                            EF.Functions.DateDiffDay(o.RequiredDate, o.ShippedDate) >= 7)
                .Join(_northwindContext.Employees,
                      o => o.EmployeeId,
                      e => e.EmployeeId,
                      (o, e) => new
                      {
                          o.OrderId,
                          e.EmployeeId,
                          FullName = e.LastName + " " + e.FirstName,
                          o.OrderDate,
                          o.RequiredDate,
                          o.ShippedDate,
                          DaysLate = EF.Functions.DateDiffDay(o.RequiredDate, o.ShippedDate)
                      })
                .OrderByDescending(x => x.DaysLate)
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise23")]
        public IActionResult Exercise23()
        {
            var employees = _northwindContext.Employees
                .Select(e => new
                {
                    FullName = e.LastName + " " + e.FirstName,
                    Phone = e.HomePhone
                });

            var customers = _northwindContext.Customers
                .Where(c => c.ContactName.StartsWith("W"))
                .Select(c => new
                {
                    FullName = c.ContactName,
                    Phone = c.Phone
                });

            var result = employees.Union(customers).ToList();

            return Ok(result);
        }


        [HttpGet("exercise24")]
        public IActionResult Exercise24()
        {
            var result = _northwindContext.Orders
                .Where(o => o.OrderId == 10643)
                .Join(
                    _northwindContext.Customers,
                    o => o.CustomerId,
                    c => c.CustomerId,
                    (o, c) => new
                    {
                        c.CustomerId,
                        c.CompanyName,
                        c.ContactName,
                        c.Address,
                        c.City,
                        c.Country
                    }
                )
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise25")]
        public IActionResult Exercise25()
        {
            var result = _northwindContext.OrderDetails
                .GroupBy(od => od.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalUnits = g.Sum(od => od.Quantity)
                })
                .Where(x => x.TotalUnits >= 1200)
                .Join(
                    _northwindContext.Products,
                    g => g.ProductId,
                    p => p.ProductId,
                    (g, p) => new
                    {
                        p.ProductId,
                        p.ProductName,
                        g.TotalUnits
                    }
                )
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise26")]
        public IActionResult Exercise26()
        {
            var result = _northwindContext.OrderDetails
                .GroupBy(od => od.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalUnits = g.Sum(od => od.Quantity)
                })
                .Where(x => x.TotalUnits >= 1400)
                .Join(
                    _northwindContext.Products,
                    g => g.ProductId,
                    p => p.ProductId,
                    (g, p) => new
                    {
                        p.ProductId,
                        p.ProductName,
                        p.SupplierId,
                        p.CategoryId,
                        g.TotalUnits
                    }
                )
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise27")]
        public IActionResult Exercise27()
        {
            // Bước 1: Gom nhóm sản phẩm theo CategoryId và đếm số lượng
            var categoryCounts = _northwindContext.Products
                .GroupBy(p => p.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    TotalProducts = g.Count()
                })
                .ToList();

            // Bước 2: Tìm giá trị lớn nhất của TotalProducts
            var maxCount = categoryCounts.Max(c => c.TotalProducts);

            // Bước 3: Lọc các Category có tổng sản phẩm = maxCount
            var result = categoryCounts
                .Where(c => c.TotalProducts == maxCount)
                .Join(
                    _northwindContext.Categories,
                    g => g.CategoryId,
                    c => c.CategoryId,
                    (g, c) => new
                    {
                        c.CategoryId,
                        c.CategoryName,
                        g.TotalProducts
                    }
                )
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise28")]
        public IActionResult Exercise28()
        {
            // Bước 1: Gom nhóm sản phẩm theo CategoryId và đếm số lượng
            var categoryCounts = _northwindContext.Products
                .GroupBy(p => p.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    TotalProducts = g.Count()
                })
                .ToList();

            // Bước 2: Tìm giá trị nhỏ nhất của TotalProducts
            var minCount = categoryCounts.Min(c => c.TotalProducts);

            // Bước 3: Lọc các Category có tổng sản phẩm = minCount
            var result = categoryCounts
                .Where(c => c.TotalProducts == minCount)
                .Join(
                    _northwindContext.Categories,
                    g => g.CategoryId,
                    c => c.CategoryId,
                    (g, c) => new
                    {
                        c.CategoryId,
                        c.CategoryName,
                        g.TotalProducts
                    }
                )
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise29")]
        public IActionResult Exercise29()
        {
            var totalCustomers = _northwindContext.Customers.Count();
            var totalEmployees = _northwindContext.Employees.Count();

            var result = new
            {
                TotalCustomers = totalCustomers,
                TotalEmployees = totalEmployees
            };

            return Ok(result);
        }


        [HttpGet("exercise30")]
        public IActionResult Exercise30()
        {
            // Tìm tổng đơn hàng tối thiểu
            var minOrders = _northwindContext.Orders
                .GroupBy(o => o.EmployeeId)  // CHÚ Ý: đúng chính tả EmployeeId
                .Select(g => g.Count())
                .Min();

            // Lấy thông tin nhân viên có số đơn hàng tối thiểu
            var result = _northwindContext.Employees
                .Where(e => _northwindContext.Orders.Count(o => o.EmployeeId == e.EmployeeId) == minOrders)
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.Title
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise31")]
        public IActionResult Exercise31()
        {
            var maxOrders = _northwindContext.Orders
                .GroupBy(o => o.EmployeeId)
                .Select(g => g.Count())
                .Max();

            // Lấy thông tin nhân viên có số đơn hàng tối đa
            var result = _northwindContext.Employees
                .Where(e => _northwindContext.Orders.Count(o => o.EmployeeId == e.EmployeeId) == maxOrders)
                .Select(e => new
                {
                    e.EmployeeId,
                    e.LastName,
                    e.FirstName,
                    e.Title,
                    Total_Orders = _northwindContext.Orders.Count(o => o.EmployeeId == e.EmployeeId)
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise32")]
        public IActionResult Exercise32()
        {
            var maxUnits = _northwindContext.Products.Max(p => p.UnitsInStock);

            var result = _northwindContext.Products
                .Where(p => p.UnitsInStock == maxUnits)
                .Select(p => new
                {
                    ProductID = p.ProductId,
                    p.ProductName,
                    SupplierID = p.SupplierId,
                    CategoryID = p.CategoryId,
                    p.UnitsInStock
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise33")]
        public IActionResult Exercise33()
        {
            var minUnits = _northwindContext.Products.Min(p => p.UnitsInStock);

            var result = _northwindContext.Products
                .Where(p => p.UnitsInStock == minUnits)
                .Select(p => new
                {
                    ProductID = p.ProductId,
                    p.ProductName,
                    SupplierID = p.SupplierId,
                    CategoryID = p.CategoryId,
                    p.UnitsInStock
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise34")]
        public IActionResult Exercise34()
        {
            var maxUnitsOnOrder = _northwindContext.Products.Max(p => p.UnitsOnOrder);

            var result = _northwindContext.Products
                .Where(p => p.UnitsOnOrder == maxUnitsOnOrder)
                .Select(p => new
                {
                    ProductID = p.ProductId,
                    p.ProductName,
                    SupplierID = p.SupplierId,
                    CategoryID = p.CategoryId,
                    p.UnitsOnOrder
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise35")]
        public IActionResult Exercise35()
        {
            var maxReOrderLevel = _northwindContext.Products.Max(p => p.ReorderLevel);

            var result = _northwindContext.Products
                .Where(p => p.ReorderLevel == maxReOrderLevel)
                .Select(p => new
                {
                    ProductID = p.ProductId,
                    p.ProductName,
                    SupplierID = p.SupplierId,
                    CategoryID = p.CategoryId,
                    reorderlevel = p.ReorderLevel
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise36")]
        public IActionResult Exercise36()
        {
            var delayedOrdersPerEmployee = _northwindContext.Orders
                .Where(o => o.ShippedDate > o.RequiredDate)
                .GroupBy(o => new
                {
                    o.EmployeeId,
                    o.Employee.LastName,
                    o.Employee.FirstName
                })
                .Select(g => new {
                    EmployeeId = g.Key,
                    g.Key.LastName,
                    g.Key.FirstName,
                    DelayedOrders = g.Count()
                })
                .ToList();

            var maxDelayed = delayedOrdersPerEmployee.Max(e => e.DelayedOrders);

            var result = delayedOrdersPerEmployee
                .Where(e => e.DelayedOrders == maxDelayed)
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise37")]
        public IActionResult Exercise37()
        {
            var delayedOrdersPerEmployee = _northwindContext.Orders
                .Where(o => o.ShippedDate > o.RequiredDate)
                .GroupBy(o => new
                {
                    o.EmployeeId,
                    o.Employee.LastName,
                    o.Employee.FirstName
                })
                .Select(g => new { 
                    EmployeeId = g.Key,
                    g.Key.LastName,
                    g.Key.FirstName,
                    DelayedOrders = g.Count()
                })
                .ToList();

            var minDelayed = delayedOrdersPerEmployee.Min(e => e.DelayedOrders);

            var result = delayedOrdersPerEmployee
                .Where(e => e.DelayedOrders == minDelayed)
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise38")]
        public IActionResult Exercise38()
        {
            var result = _northwindContext.OrderDetails
                .GroupBy(od => new { od.ProductId, od.Product.ProductName })
                .Select(g => new
                {
                    g.Key.ProductId,
                    g.Key.ProductName,
                    TotalOrderDetails = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(p => p.TotalOrderDetails)
                .Take(3)
                .AsEnumerable()
                .OrderBy(p => p.TotalOrderDetails)
                .ToList();

            return Ok(result);
        }


        [HttpGet("exercise39")]
        public IActionResult Exercise39()
        {
            var result = _northwindContext.OrderDetails
                .GroupBy(od => new { od.ProductId, od.Product.ProductName })
                .Select(g => new
                {
                    g.Key.ProductId,
                    g.Key.ProductName,
                    TotalOrderDetails = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(p => p.TotalOrderDetails)
                .Take(5)
                .AsEnumerable()
                .OrderBy(p => p.TotalOrderDetails)
                .ToList();

            return Ok(result);
        }


    }
}
