using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NasaPhotos.Models;
using NasaPhotoReader.Utilities;
using Microsoft.AspNetCore.Mvc;
using NasaPhotoReader.Web.Models;

namespace NasaPhotoReader.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ProcessImages();
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
        private void ProcessImages()
        {
            var dateList = new List<string>();

            PhotoUtility.GetConfigurationSection("ImageDates:queryDates").Bind(dateList);

            dateList.ForEach(delegate (string date)
            {
                GetImages(date);
            });
        }
        /// <summary>
        /// Retrieve the  images from the Satellite API.
        /// </summary>
        /// <param name="ImageDate"></param>
        private void GetImages(string ImageDate)
        {

            NasaPhotos.Models.NasaMarsPhotos NasaPhotos = (NasaPhotos.Models.NasaMarsPhotos)PhotoUtility.ExecuteService(PhotoUtility.configuration.GetSection("NasaUrl:url").Value,
                                                                                                                        String.Format("&earth_date={0}", ImageDate),
                                                                                                                        typeof(NasaPhotos.Models.NasaMarsPhotos));


            foreach (var photo in NasaPhotos.Photos)
            {
                var fileName = photo.Img_Src.Substring(photo.Img_Src.LastIndexOf('/') + 1);
                Console.WriteLine(photo.Img_Src);
                if (!PhotoUtility.SaveImage(photo.Img_Src, String.Format(@"{0}\{1}_{2}", PhotoUtility.configuration.GetSection("SaveLocation:path").Value, ImageDate, fileName)))
                {
                    Console.WriteLine(String.Format("Unable to save the image : {0}_{1}", ImageDate, fileName));
                }
                else
                {
                    Console.WriteLine(String.Format("Successfully saved file : {0}_{1}", ImageDate, fileName));
                }


            }
        }
}
