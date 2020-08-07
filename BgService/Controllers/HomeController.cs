using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using BgService.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using BgService.Data;
using Microsoft.EntityFrameworkCore;

namespace BgService.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly BgServiceDbContext _context;

        public HomeController( BgServiceDbContext context)
        {
          
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);

                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please select CSV file";
                        return View();
                    }



                    var employees = new List<Employee>();
                    using (var sreader = new StreamReader(postedFile.OpenReadStream()))
                    {
                        //First line is header. If header is not passed in csv then we can neglect the below line.
                        string[] headers = sreader.ReadLine().Split(',');
                        //Loop through the records
                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(',');

                            employees.Add(new Employee
                            {
                               // Id = int.Parse(rows[0].ToString()),
                                Name = rows[0].ToString(),
                                JobRole = rows[1].ToString()
                            });
                            
                        }
                        foreach (var employee in employees)
                        {

                            // var defEmp = _context.Employee.AsNoTracking().FirstOrDefault(e => e.Name == employee.Name);
                            var defEmp = _context.Employee.Where(e => e.Name == employee.Name).FirstOrDefault();
                            if (defEmp != null)
                            {
                                defEmp.Name = employee.Name;
                                defEmp.JobRole = employee.JobRole;
                               // _context.Employee.Update(employee);
                            }
                            else
                            {
                                _context.Employee.Add(employee);
                            }

                        }
                        await _context.SaveChangesAsync();
                    }

                    return View("View", employees);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please select the file first to upload.";
            }
            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
