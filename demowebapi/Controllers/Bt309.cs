using demowebapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demowebapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Bt309Controller : ControllerBase
    {
        private readonly NorthwindContext _context;

        public Bt309Controller(NorthwindContext context)
        {
            _context = context;
        }

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

        [HttpGet("2")]
        public async Task<ActionResult> Api2()
        {
            var result = await _context.Orders
                .GroupBy(o => new { o.EmployeeId })
                .Select(g => new
                {
                    EmployeeId = g.Key.EmployeeId,
                    TotalOrders = g.Count()
                })
                .OrderByDescending(x => x.TotalOrders)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("3")]
        public async Task<ActionResult> Api3()
        {
            var result = await _context.Products
                .GroupBy(p => new { p.CategoryId, CategoryName = p.Category.CategoryName })
                .Select(g => new
                {
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.CategoryName,
                    AveragePrice = g.Average(p => (decimal?)p.UnitPrice) ?? 0
                })
                .OrderBy(x => x.CategoryName)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("4")]
        public async Task<ActionResult> Api4()
        {
            var result = await _context.Products
                .GroupBy(p => new { p.CategoryId, CategoryName = p.Category.CategoryName })
                .Select(g => new
                {
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.CategoryName,
                    ProductCount = g.Count()
                })
                .OrderBy(x => x.CategoryName)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("5")]
        public async Task<ActionResult> Api5()
        {
            var result = await _context.OrderDetails
                .GroupBy(od => new { od.ProductId, ProductName = od.Product.ProductName })
                .Select(g => new
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    TotalSales = g.Sum(od => (decimal?)od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount)) ?? 0
                })
                .OrderByDescending(x => x.TotalSales)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("6")]
        public async Task<ActionResult> Api6()
        {
            var result = await _context.Products
                .OrderByDescending(p => p.UnitPrice)
                .Take(5)
                .Select(p => new { p.ProductId, p.ProductName, p.UnitPrice })
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("7")]
        public async Task<ActionResult> Api7()
        {
            var result = await _context.Products
                .Where(p => p.UnitPrice > 0)
                .OrderBy(p => p.UnitPrice)
                .Take(5)
                .Select(p => new { p.ProductId, p.ProductName, p.UnitPrice })
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("8")]
        public async Task<ActionResult> Api8()
        {
            var result = await _context.Products
                .Where(p => p.UnitPrice >= 50 && p.UnitPrice <= 100)
                .Select(p => new { p.ProductId, p.ProductName, p.UnitPrice })
                .OrderBy(p => p.UnitPrice)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("9")]
        public async Task<ActionResult> Api9()
        {
            var result = await _context.Customers
                .Where(c => c.ContactName.StartsWith("A"))
                .Select(c => new { c.CustomerId, c.CompanyName, c.ContactName })
                .OrderBy(c => c.ContactName)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("10")]
        public async Task<ActionResult> Api10()
        {
            var result = await _context.Orders
                .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1997)
                .Select(o => new { o.OrderId, o.CustomerId, o.OrderDate })
                .OrderBy(o => o.OrderDate)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("11")]
        public async Task<ActionResult> Api11()
        {
            var result = await _context.OrderDetails
                .GroupBy(od => od.OrderId)
                .Select(g => new
                {
                    OrderId = g.Key,
                    TotalRevenue = g.Sum(od => (decimal?)od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount)) ?? 0
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("12")]
        public async Task<ActionResult> Api12()
        {
            var result = await _context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    BirthDate = e.BirthDate,
                    Age = e.BirthDate.HasValue ? DateTime.Now.Year - e.BirthDate.Value.Year : 0
                })
                .OrderBy(e => e.LastName)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("13")]
        public async Task<ActionResult> Api13()
        {
            var result = await _context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    HireDate = e.HireDate,
                    YearsOfService = e.HireDate.HasValue ? DateTime.Now.Year - e.HireDate.Value.Year : 0
                })
                .OrderBy(e => e.LastName)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("14")]
        public async Task<ActionResult> Api14()
        {
            var result = await _context.Orders
                .Join(_context.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => new { o, od })
                .GroupBy(x => x.o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    TotalRevenue = g.Sum(x => (decimal?)x.od.Quantity * x.od.UnitPrice * (1 - (decimal)x.od.Discount)) ?? 0
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("15")]
        public async Task<ActionResult> Api15()
        {
            var result = await _context.Orders
                .Where(o => o.ShippedDate.HasValue)
                .Join(_context.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => new { o, od })
                .GroupBy(x => x.o.ShippedDate.Value.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    TotalRevenue = g.Sum(x => (decimal?)x.od.Quantity * x.od.UnitPrice * (1 - (decimal)x.od.Discount)) ?? 0
                })
                .OrderBy(x => x.Year)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("16")]
        public async Task<ActionResult> Api16()
        {
            var avgPrice = await _context.Products.AverageAsync(p => (decimal?)p.UnitPrice) ?? 0;
            var result = await _context.Products
                .Where(p => p.UnitPrice > avgPrice)
                .Select(p => new { p.ProductId, p.ProductName, p.UnitPrice, AveragePrice = avgPrice })
                .OrderByDescending(p => p.UnitPrice)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("17")]
        public async Task<ActionResult> Api17()
        {
            var avgOrders = await _context.Orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new { Count = g.Count() })
                .AverageAsync(x => (decimal?)x.Count) ?? 0;

            var result = await _context.Orders
                .GroupBy(o => new { o.CustomerId, CustomerName = o.Customer.CompanyName })
                .Select(g => new { CustomerId = g.Key.CustomerId, CustomerName = g.Key.CustomerName, OrderCount = g.Count() })
                .Where(x => x.OrderCount > (int)avgOrders)
                .OrderByDescending(x => x.OrderCount)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("18")]
        public async Task<ActionResult> Api18()
        {
            var result = await _context.Orders
                .GroupBy(o => new { o.EmployeeId, EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName })
                .Select(g => new { EmployeeId = g.Key.EmployeeId, EmployeeName = g.Key.EmployeeName, OrderCount = g.Count() })
                .OrderByDescending(x => x.OrderCount)
                .Take(1)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("19")]
        public async Task<ActionResult> Api19()
        {
            var result = await _context.OrderDetails
                .Where(od => od.UnitPrice > 100)
                .Select(od => new { od.OrderId, od.ProductId, od.Product.ProductName, od.UnitPrice })
                .Distinct()
                .OrderBy(x => x.OrderId)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("20")]
        public async Task<ActionResult> Api20()
        {
            var result = await _context.Products
                .GroupBy(p => new { p.CategoryId, CategoryName = p.Category.CategoryName })
                .Select(g => new { CategoryId = g.Key.CategoryId, CategoryName = g.Key.CategoryName, ProductCount = g.Count() })
                .Where(x => x.ProductCount > 10)
                .OrderBy(x => x.CategoryName)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("21")]
        public async Task<ActionResult> Api21()
        {
            var soldProductIds = await _context.OrderDetails.Select(od => od.ProductId).Distinct().ToListAsync();
            var result = await _context.Products
                .Where(p => !soldProductIds.Contains(p.ProductId))
                .Select(p => new { p.ProductId, p.ProductName, p.UnitPrice })
                .OrderBy(p => p.ProductName)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("22")]
        public async Task<ActionResult> Api22()
        {
            var customerWithOrders = await _context.Orders.Select(o => o.CustomerId).Distinct().ToListAsync();
            var result = await _context.Customers
                .Where(c => !customerWithOrders.Contains(c.CustomerId))
                .Select(c => new { c.CustomerId, c.CompanyName, c.ContactName })
                .OrderBy(c => c.CompanyName)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("23")]
        public async Task<ActionResult> Api23()
        {
            var result = await _context.Orders
                .GroupBy(o => new { o.EmployeeId, EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName })
                .Select(g => new { EmployeeId = g.Key.EmployeeId, EmployeeName = g.Key.EmployeeName, OrderCount = g.Count() })
                .OrderByDescending(x => x.OrderCount)
                .Take(3)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("24")]
        public async Task<ActionResult> Api24()
        {
            var result = await _context.Orders
                .GroupBy(o => new { o.CustomerId, CompanyName = o.Customer.CompanyName })
                .Select(g => new { CustomerId = g.Key.CustomerId, CompanyName = g.Key.CompanyName, OrderCount = g.Count() })
                .Where(x => x.OrderCount > 5)
                .OrderByDescending(x => x.OrderCount)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("25")]
        public async Task<ActionResult> Api25()
        {
            var result = await _context.OrderDetails
                .GroupBy(od => od.OrderId)
                .Select(g => new
                {
                    OrderId = g.Key,
                    ItemCount = g.Count(),
                    TotalRevenue = g.Sum(od => (decimal?)od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount)) ?? 0
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToListAsync();
            return Ok(result);
        }
    }
}