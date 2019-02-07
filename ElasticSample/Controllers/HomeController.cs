using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElasticSample.Models;
using ElasticSample.Extensions;
using Nest;
using System.Net.Http;

namespace ElasticSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly MSSqlContext _context;

        public HomeController(MSSqlContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult InsertBulkData()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            var client = new ElasticClient(settings);

            string index = "employee";


            if (client.IndexExists(index).Exists)
            {
                client.DeleteIndex(index);
            }

            var indexDescriptor = new CreateIndexDescriptor(index)
                .Settings(s => s.NumberOfReplicas(0).NumberOfShards(1))
                .Mappings(mappings => mappings
                    .Map<Employee>(m => m.AutoMap()));

            var response = client.CreateIndex(index, i => indexDescriptor);

            List<Employee> employees = _context.Employee.ToList();
            foreach (var employee in employees)
            {
                employee.Suggest = new CompletionField()
                {
                    Input = HomeExtension.GetKeywords(employee.Name, employee.LastName)
                };
                var indexResponse = client.Index(employee, i => i.Index(index));
            }

            return RedirectToAction("Index");
        }

    }
}
