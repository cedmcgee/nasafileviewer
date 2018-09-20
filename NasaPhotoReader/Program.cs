using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NasaPhotos.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;

namespace NasaPhotoReader
{
    class Program
    {
        private static IServiceProvider provider;
        static void Main(string[] args)
        {
            // Get the Configuration 
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            // provider = new ServiceCollectionServiceExtensions()
            


            var queryDates = configuration.GetSection("ImageDates:QueryDates");
            //foreach (ImageDate query_dates in imageData.AsEnumerable())
            //{
            //   // Console.Read();
            //}

           // var imageDates = (List<ImageDate>)JsonConvert.DeserializeObject(imageData.ToString(), typeof(List<ImageDate>));
            // List<ImageDate> dates =  (List<ImageDate>)configuration.GetSection("ImageDates:image_date").GetChildren().GetEnumerator();



            NasaPhotos.Models.NasaMarsPhotos NasaPhotos = (NasaPhotos.Models.NasaMarsPhotos)Program.ExecuteService("&earth_date=2018-06-02", typeof(NasaPhotos.Models.NasaMarsPhotos));


            foreach (var photo in NasaPhotos.Photos)
            {
                var fileName = photo.Img_Src.Substring(photo.Img_Src.LastIndexOf('/') + 1);
                Console.WriteLine(photo.Img_Src);
                if (!SaveImage(photo.Img_Src, String.Format(@"D:\SaveImages\{0}",fileName)))
                {
                    Console.WriteLine(String.Format("Unable to save the image : {0}",fileName));
                }
                else
                {
                    Console.WriteLine(String.Format("Successfully saved file : {0}", fileName));
                }
            

            }

            Console.Read();

        }
        public static List<ImageDate> GetImageDates()
        {

            return null;
        }

        public static bool SaveImage(string imageUrl, string saveLocation)
        {
            try
            {
                byte[] imageData;
                using (WebClient webClient = new WebClient())
                {
                    imageData = webClient.DownloadData(imageUrl);
                }

                System.IO.File.WriteAllBytes(saveLocation, imageData);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false; }

            return true;
        }
        public static object ExecuteService(string operation, Type returnObject)
        {

            
            object deserializedObject = null;
            try
            {
                using (WebClient webclient = new WebClient())
                {
                    // var webclient = new WebClient();

                    var response = webclient.DownloadString(String.Format("{0}{1}", "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol=1000&camera=fhaz&api_key=DEMO_KEY&", operation));
                    if (String.IsNullOrEmpty(response)) return null;


                    deserializedObject = JsonConvert.DeserializeObject(response, returnObject);
                }
            }
            catch
                (JsonSerializationException)
            { return null; }
            catch
                (Exception)
            { throw; }

            return deserializedObject;
        }
    }
}
