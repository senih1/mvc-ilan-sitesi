using ilan_sitesi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ilan_sitesi.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View(GetAds());
        }

        [HttpPost]
        public IActionResult AddAd(Ad model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Hata"] = $"<div class=\"alert alert-warning\" role=\"alert\">\r\n   Hatalı veya eksik ilan girişi!\r\n</div>\r\n";
                return View();
            }
            else
            {
                TempData["Hata"] = $"<div class=\"alert alert-success\" role=\"alert\">\r\n  İlanınız başarı ile eklendi!\r\n</div>\r\n";
                AddAdToList(model);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult DeleteAd(string adName)
        {
            var ads = GetAds();
            Ad? adToDelete = FindAd(adName, ads);

            if (adToDelete != null)
            {
                ads.Remove(adToDelete);
                using (StreamWriter writer = new StreamWriter("App_Data/ad.txt"))
                {
                    foreach (var ad in ads)
                    {
                        writer.WriteLine($"{ad.Name}|{ad.Price}|{ad.Detail}|{ad.Image}");
                    }
                }
                TempData["Hata"] = $"<div class=\"alert alert-success\" role=\"alert\">\r\n  İlanınız başarı ile silindi!\r\n</div>\r\n";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public Ad? FindAd(string adName, List<Ad> ads)
        {
            foreach (var ad in ads)
            {
                if (ad.Name == adName)
                {
                    return ad;
                }
            }
            return null;
        }


        public void AddAdToList(Ad model)
        {
            string newAd = $"{model.Name}|{model.Price}|{model.Detail}|{model.Image}";
            string existingAds;

            using (StreamReader reader = new StreamReader("App_Data/ad.txt"))
            {
                existingAds = reader.ReadToEnd();
            }

            string updatedAds = newAd + "\n" + existingAds;
            using (StreamWriter writer = new StreamWriter("App_Data/ad.txt"))
            {
                writer.Write(updatedAds);
            }
        }

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
                    if (parts.Length >= 3)
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
    }
}
