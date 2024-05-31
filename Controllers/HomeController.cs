using ilan_sitesi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ilan_sitesi.Controllers
{
    public class HomeController : Controller
    {
        public List<Ad> GetAds()
        {
            var ads = new List<Ad>();
            using StreamReader reader = new("App_Data/ad.txt");
            var txt = reader.ReadToEnd();
            var txtLines = txt.Split('\n');
            foreach (var line in txtLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 4)
                    {
                        ads.Add(new Ad
                        {
                            Name = parts[0],
                            Price = int.Parse(parts[1]),
                            Detail = parts[2],
                            Image = parts[3]
                        });
                    }
                }
            }
            return ads;
        }

        public IActionResult Index()
        {
            return View(GetAds());
        }
    }
}
