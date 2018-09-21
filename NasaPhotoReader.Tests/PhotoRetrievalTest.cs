using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using NasaPhotoReader.Utilities;
using NasaPhotos.Models;
using System.Text;
using System;
namespace NasaPhotoReader.Tests
{
    [TestClass]
    public class PhotoRetrievalTest
    {
        [TestMethod]
        public void SaveImages()
        {
            var imageDate = "2017-02-27";
            NasaPhotos.Models.NasaMarsPhotos NasaPhotos = (NasaPhotos.Models.NasaMarsPhotos)PhotoUtility.ExecuteService("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol=1000&camera=fhaz&api_key=DEMO_KEY&",
                                                                                                                         String.Format("&earth_date={0}", "2017-02-27"),
                                                                                                                         typeof(NasaPhotos.Models.NasaMarsPhotos));
            var fileName = "";
            var fileLocation = "D:\\SaveImages";
            foreach (var photo in NasaPhotos.Photos)
            {
                fileName = photo.Img_Src.Substring(photo.Img_Src.LastIndexOf('/') + 1);
                Console.WriteLine(photo.Img_Src);
                if (!PhotoUtility.SaveImage(photo.Img_Src, String.Format(@"{0}\{1}_{2}", fileLocation, imageDate, fileName)))
                {
                    Console.WriteLine(String.Format("Unable to save the image : {0}_{1}", imageDate, fileName));
                }
                else
                {
                    Console.WriteLine(String.Format("Successfully saved file : {0}_{1}", imageDate, fileName));
                }


            }
            var lastCreatedFile = String.Format(@"{0}\{1}_{2}", fileLocation, imageDate, fileName);
            Assert.IsTrue(System.IO.File.Exists(lastCreatedFile));

        }
        [TestMethod]
        public void GetImageByDate()
        {
      
            NasaPhotos.Models.NasaMarsPhotos NasaPhotos = (NasaPhotos.Models.NasaMarsPhotos)PhotoUtility.ExecuteService("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol=1000&camera=fhaz&api_key=DEMO_KEY&",
                                                                                                                        String.Format("&earth_date={0}", "2017-02-27"),
                                                                                                                        typeof(NasaPhotos.Models.NasaMarsPhotos));

            Assert.IsNotNull(NasaPhotos);
        }
        [TestMethod]
        public void GetConfigurationSection()
        {
            IConfiguration configuration = PhotoUtility.GetConfigurationSection("NasaUrl:url");
            Assert.IsNotNull(configuration);
        }
    }
}
