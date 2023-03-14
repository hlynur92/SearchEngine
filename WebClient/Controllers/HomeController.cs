using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;

namespace WebClient.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("Search")]
    public string Search(string terms, int numberOfResults) { 
        HttpClient lb_api = new HttpClient();

        lb_api.BaseAddress = new Uri("loadbalancer-1");

        var task = lb_api.GetStringAsync("/Load/Search?terms=" + terms + "&numberOfResults=" + numberOfResults);
        task.Wait();

        return task.Result;
    }

    [HttpGet("Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
