using demowebapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demowebapi.Controllers
{
    /// <summary>
    /// API các truy vấn dữ liệu Northwind Database
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class Bt309Controller : ControllerBase
    {
        private readonly NorthwindContext _context;

        public Bt309Controller(NorthwindContext context)
        {
            _context = context;
        }

        /// <summary>
        /// API 1: Đếm số lượng khách hàng theo từng quốc gia
        /// </summary>
        /// <returns>Danh sách quốc gia với số lượng khách hàng</returns>
        [HttpGet("1")]
        public async Task<ActionResult> Api1()
        {
            var result = await _context.Customers
                .GroupBy(c => c.Country)
                .Select(g => new { Country = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 2: Tính tổng số đơn hàng của từng nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên với số lượng đơn hàng</returns>
        [HttpGet("2")]
        public async Task<ActionResult> Api2()
        {
            var result = await _context.Orders
                .GroupBy(o => new { o.EmployeeID })
                .Select(g => new
                {
                    EmployeeID = g.Key.EmployeeID,
                    TotalOrders = g.Count()
                })
                .OrderByDescending(x => x.TotalOrders)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 3: Tính trung bình đơn giá của mỗi danh mục sản phẩm
        /// </summary>
        /// <returns>Danh sách danh mục với giá trung bình</returns>
        [HttpGet("3")]
        public async Task<ActionResult> Api3()
        {
            var result = await _context.Products
                .GroupBy(p => new { p.CategoryID, CategoryName = p.Category.CategoryName })
                .Select(g => new
                {
                    CategoryID = g.Key.CategoryID,
                    CategoryName = g.Key.CategoryName,
                    AveragePrice = g.Average(p => (decimal?)p.UnitPrice) ?? 0
                })
                .OrderBy(x => x.CategoryName)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 4: Tìm số lượng sản phẩm trong mỗi danh mục
        /// </summary>
        /// <returns>Danh sách danh mục với số lượng sản phẩm</returns>
        [HttpGet("4")]
        public async Task<ActionResult> Api4()
        {
            var result = await _context.Products
                .GroupBy(p => new { p.CategoryID, CategoryName = p.Category.CategoryName })
                .Select(g => new
                {
                    CategoryID = g.Key.CategoryID,
                    CategoryName = g.Key.CategoryName,
                    ProductCount = g.Count()
                })
                .OrderBy(x => x.CategoryName)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 5: Tính tổng số tiền bán được của mỗi sản phẩm
        /// </summary>
        /// <returns>Danh sách sản phẩm với tổng doanh thu</returns>
        [HttpGet("5")]
        public async Task<ActionResult> Api5()
        {
            var result = await _context.OrderDetails
                .GroupBy(od => new { od.ProductID, ProductName = od.Product.ProductName })
                .Select(g => new
                {
                    ProductID = g.Key.ProductID,
                    ProductName = g.Key.ProductName,
                    TotalSales = g.Sum(od => (decimal?)od.Quantity * od.UnitPrice * (1 - od.Discount)) ?? 0
                })
                .OrderByDescending(x => x.TotalSales)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 6: Liệt kê 5 sản phẩm có đơn giá cao nhất
        /// </summary>
        /// <returns>Danh sách 5 sản phẩm đắt nhất</returns>
        [HttpGet("6")]
        public async Task<ActionResult> Api6()
        {
            var result = await _context.Products
                .OrderByDescending(p => p.UnitPrice)
                .Take(5)
                .Select(p => new { p.ProductID, p.ProductName, p.UnitPrice })
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 7: Liệt kê 5 sản phẩm có đơn giá thấp nhất
        /// </summary>
        /// <returns>Danh sách 5 sản phẩm rẻ nhất</returns>
        [HttpGet("7")]
        public async Task<ActionResult> Api7()
        {
            var result = await _context.Products
                .Where(p => p.UnitPrice > 0)
                .OrderBy(p => p.UnitPrice)
                .Take(5)
                .Select(p => new { p.ProductID, p.ProductName, p.UnitPrice })
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 8: Tìm các sản phẩm có đơn giá từ 50 đến 100
        /// </summary>
        /// <returns>Danh sách sản phẩm trong khoảng giá 50-100</returns>
        [HttpGet("8")]
        public async Task<ActionResult> Api8()
        {
            var result = await _context.Products
                .Where(p => p.UnitPrice >= 50 && p.UnitPrice <= 100)
                .Select(p => new { p.ProductID, p.ProductName, p.UnitPrice })
                .OrderBy(p => p.UnitPrice)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 9: Tìm các khách hàng có tên bắt đầu bằng chữ "A"
        /// </summary>
        /// <returns>Danh sách khách hàng có tên bắt đầu bằng A</returns>
        [HttpGet("9")]
        public async Task<ActionResult> Api9()
        {
            var result = await _context.Customers
                .Where(c => c.ContactName.StartsWith("A"))
                .Select(c => new { c.CustomerID, c.CompanyName, c.ContactName })
                .OrderBy(c => c.ContactName)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 10: Tìm đơn hàng có ngày đặt hàng trong năm 1997
        /// </summary>
        /// <returns>Danh sách đơn hàng năm 1997</returns>
        [HttpGet("10")]
        public async Task<ActionResult> Api10()
        {
            var result = await _context.Orders
                .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1997)
                .Select(o => new { o.OrderID, o.CustomerID, o.OrderDate })
                .OrderBy(o => o.OrderDate)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 11: Tính doanh thu của từng đơn hàng (Quantity * UnitPrice * (1 - Discount))
        /// </summary>
        /// <returns>Danh sách đơn hàng với tổng doanh thu</returns>
        [HttpGet("11")]
        public async Task<ActionResult> Api11()
        {
            var result = await _context.OrderDetails
                .GroupBy(od => od.OrderID)
                .Select(g => new
                {
                    OrderID = g.Key,
                    TotalRevenue = g.Sum(od => (decimal?)od.Quantity * od.UnitPrice * (1 - od.Discount)) ?? 0
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 12: Tính tuổi của từng nhân viên (dựa trên BirthDate)
        /// </summary>
        /// <returns>Danh sách nhân viên với tuổi</returns>
        [HttpGet("12")]
        public async Task<ActionResult> Api12()
        {
            var result = await _context.Employees
                .Select(e => new
                {
                    e.EmployeeID,
                    e.FirstName,
                    e.LastName,
                    BirthDate = e.BirthDate,
                    Age = e.BirthDate.HasValue ? DateTime.Now.Year - e.BirthDate.Value.Year : 0
                })
                .OrderBy(e => e.LastName)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 13: Hiển thị số năm làm việc của mỗi nhân viên (từ HireDate đến hiện tại)
        /// </summary>
        /// <returns>Danh sách nhân viên với năm kinh nghiệm</returns>
        [HttpGet("13")]
        public async Task<ActionResult> Api13()
        {
            var result = await _context.Employees
                .Select(e => new
                {
                    e.EmployeeID,
                    e.FirstName,
                    e.LastName,
                    HireDate = e.HireDate,
                    YearsOfService = e.HireDate.HasValue ? DateTime.Now.Year - e.HireDate.Value.Year : 0
                })
                .OrderBy(e => e.LastName)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 14: Tính tổng doanh thu của từng khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng với tổng doanh thu</returns>
        [HttpGet("14")]
        public async Task<ActionResult> Api14()
        {
            var result = await _context.Orders
                .Join(_context.OrderDetails, o => o.OrderID, od => od.OrderID, (o, od) => new { o, od })
                .GroupBy(x => x.o.CustomerID)
                .Select(g => new
                {
                    CustomerID = g.Key,
                    TotalRevenue = g.Sum(x => (decimal?)x.od.Quantity * x.od.UnitPrice * (1 - x.od.Discount)) ?? 0
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 15: Tính tổng doanh thu của từng năm
        /// </summary>
        /// <returns>Danh sách năm với tổng doanh thu</returns>
        [HttpGet("15")]
        public async Task<ActionResult> Api15()
        {
            var result = await _context.Orders
                .Where(o => o.ShippedDate.HasValue)
                .Join(_context.OrderDetails, o => o.OrderID, od => od.OrderID, (o, od) => new { o, od })
                .GroupBy(x => x.o.ShippedDate.Value.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    TotalRevenue = g.Sum(x => (decimal?)x.od.Quantity * x.od.UnitPrice * (1 - x.od.Discount)) ?? 0
                })
                .OrderBy(x => x.Year)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 16: Tìm sản phẩm có đơn giá lớn hơn đơn giá trung bình
        /// </summary>
        /// <returns>Danh sách sản phẩm có giá trên trung bình</returns>
        [HttpGet("16")]
        public async Task<ActionResult> Api16()
        {
            var avgPrice = await _context.Products.AverageAsync(p => (decimal?)p.UnitPrice) ?? 0;
            var result = await _context.Products
                .Where(p => p.UnitPrice > avgPrice)
                .Select(p => new { p.ProductID, p.ProductName, p.UnitPrice, AveragePrice = avgPrice })
                .OrderByDescending(p => p.UnitPrice)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 17: Tìm khách hàng có số lượng đơn hàng lớn hơn trung bình
        /// </summary>
        /// <returns>Danh sách khách hàng có đơn hàng trên trung bình</returns>
        [HttpGet("17")]
        public async Task<ActionResult> Api17()
        {
            var avgOrders = await _context.Orders
                .GroupBy(o => o.CustomerID)
                .Select(g => new { Count = g.Count() })
                .AverageAsync(x => (decimal?)x.Count) ?? 0;

            var result = await _context.Orders
                .GroupBy(o => new { o.CustomerID, CustomerName = o.Customer.CompanyName })
                .Select(g => new { CustomerID = g.Key.CustomerID, CustomerName = g.Key.CustomerName, OrderCount = g.Count() })
                .Where(x => x.OrderCount > (int)avgOrders)
                .OrderByDescending(x => x.OrderCount)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 18: Tìm nhân viên có tổng số đơn hàng nhiều nhất
        /// </summary>
        /// <returns>Nhân viên có nhiều đơn hàng nhất</returns>
        [HttpGet("18")]
        public async Task<ActionResult> Api18()
        {
            var result = await _context.Orders
                .GroupBy(o => new { o.EmployeeID, EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName })
                .Select(g => new { EmployeeID = g.Key.EmployeeID, EmployeeName = g.Key.EmployeeName, OrderCount = g.Count() })
                .OrderByDescending(x => x.OrderCount)
                .Take(1)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 19: Tìm các đơn hàng chứa sản phẩm có đơn giá > 100
        /// </summary>
        /// <returns>Danh sách đơn hàng với sản phẩm giá > 100</returns>
        [HttpGet("19")]
        public async Task<ActionResult> Api19()
        {
            var result = await _context.OrderDetails
                .Where(od => od.UnitPrice > 100)
                .Select(od => new { od.OrderID, od.ProductID, od.Product.ProductName, od.UnitPrice })
                .Distinct()
                .OrderBy(x => x.OrderID)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 20: Tìm danh mục có nhiều hơn 10 sản phẩm
        /// </summary>
        /// <returns>Danh sách danh mục có > 10 sản phẩm</returns>
        [HttpGet("20")]
        public async Task<ActionResult> Api20()
        {
            var result = await _context.Products
                .GroupBy(p => new { p.CategoryID, CategoryName = p.Category.CategoryName })
                .Select(g => new { CategoryID = g.Key.CategoryID, CategoryName = g.Key.CategoryName, ProductCount = g.Count() })
                .Where(x => x.ProductCount > 10)
                .OrderBy(x => x.CategoryName)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 21: Tìm tất cả sản phẩm chưa từng được bán
        /// </summary>
        /// <returns>Danh sách sản phẩm không có trong OrderDetails</returns>
        [HttpGet("21")]
        public async Task<ActionResult> Api21()
        {
            var soldProductIds = await _context.OrderDetails.Select(od => od.ProductID).Distinct().ToListAsync();
            var result = await _context.Products
                .Where(p => !soldProductIds.Contains(p.ProductID))
                .Select(p => new { p.ProductID, p.ProductName, p.UnitPrice })
                .OrderBy(p => p.ProductName)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 22: Tìm khách hàng chưa từng đặt đơn hàng nào
        /// </summary>
        /// <returns>Danh sách khách hàng không có đơn hàng</returns>
        [HttpGet("22")]
        public async Task<ActionResult> Api22()
        {
            var customerWithOrders = await _context.Orders.Select(o => o.CustomerID).Distinct().ToListAsync();
            var result = await _context.Customers
                .Where(c => !customerWithOrders.Contains(c.CustomerID))
                .Select(c => new { c.CustomerID, c.CompanyName, c.ContactName })
                .OrderBy(c => c.CompanyName)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 23: Hiển thị 3 nhân viên có nhiều đơn hàng nhất
        /// </summary>
        /// <returns>Danh sách 3 nhân viên hàng đầu</returns>
        [HttpGet("23")]
        public async Task<ActionResult> Api23()
        {
            var result = await _context.Orders
                .GroupBy(o => new { o.EmployeeID, EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName })
                .Select(g => new { EmployeeID = g.Key.EmployeeID, EmployeeName = g.Key.EmployeeName, OrderCount = g.Count() })
                .OrderByDescending(x => x.OrderCount)
                .Take(3)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 24: Hiển thị danh sách các khách hàng có nhiều hơn 5 đơn hàng
        /// </summary>
        /// <returns>Danh sách khách hàng có > 5 đơn hàng</returns>
        [HttpGet("24")]
        public async Task<ActionResult> Api24()
        {
            var result = await _context.Orders
                .GroupBy(o => new { o.CustomerID, CompanyName = o.Customer.CompanyName })
                .Select(g => new { CustomerID = g.Key.CustomerID, CompanyName = g.Key.CompanyName, OrderCount = g.Count() })
                .Where(x => x.OrderCount > 5)
                .OrderByDescending(x => x.OrderCount)
                .ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// API 25: Liệt kê đơn hàng và tổng doanh thu của từng đơn
        /// </summary>
        /// <returns>Danh sách đơn hàng với tổng doanh thu chi tiết</returns>
        [HttpGet("25")]
        public async Task<ActionResult> Api25()
        {
            var result = await _context.OrderDetails
                .GroupBy(od => od.OrderID)
                .Select(g => new
                {
                    OrderID = g.Key,
                    ItemCount = g.Count(),
                    TotalRevenue = g.Sum(od => (decimal?)od.Quantity * od.UnitPrice * (1 - od.Discount)) ?? 0
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToListAsync();
            return Ok(result);
        }
    }
}