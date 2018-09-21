using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;

namespace NasaPhotoReader.Utilities
{
    
    public static class PhotoUtility
    {
        public static IConfiguration configuration;
    
        public static IConfiguration GetConfigurationSection(string Section)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();

            return configuration.GetSection(Section);
        }
        
        /// <summary>
        /// Save the images to the file system.
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="saveLocation"></param>
        /// <returns></returns>
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
                return false;
            }

            return true;
        }
        public static object ExecuteService(string serviceUrl, string operation, Type returnObject)
        {
            
            object deserializedObject = null;
            try
            {
                using (WebClient webclient = new WebClient())
                {
                    var response = webclient.DownloadString(String.Format("{0}{1}", serviceUrl, operation));
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
