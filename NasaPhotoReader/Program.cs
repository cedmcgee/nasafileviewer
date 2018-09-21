using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NasaPhotos.Models;
using NasaPhotoReader.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;

namespace NasaPhotoReader
{
    class Program
    {
        static void Main(string[] args)
        {

            ProcessImages();

        }
        /// <summary>
        /// 
        /// </summary>
        private static void ProcessImages()
        {
            var dateList = new List<string>();

            PhotoUtility.GetConfigurationSection("ImageDates:queryDates").Bind(dateList);
            try
            {
                dateList.ForEach(delegate (string date)
                {
                    GetImages(date);
                });

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }
        }
        /// <summary>
        /// Retrieve the  images from the Satellite API.
        /// </summary>
        /// <param name="ImageDate"></param>
        private static void GetImages(string ImageDate)
        {
            try
            {

                NasaPhotos.Models.NasaMarsPhotos NasaPhotos = (NasaPhotos.Models.NasaMarsPhotos)PhotoUtility.ExecuteService(PhotoUtility.configuration.GetSection("NasaUrl:url").Value,
                                                                                                                            String.Format("&earth_date={0}", ImageDate),
                                                                                                                          typeof(NasaPhotos.Models.NasaMarsPhotos));
                // if there was an exception
                if (NasaPhotos != null)
                {

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
            }catch(Exception) { throw; }
           
        }       
    }
}
