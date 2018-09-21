# nasafileviewer
Application to download images from Mars API
Console application to call NASA's Mars API . 
The image file dates are specified in the appsettings.json file along with the location to saved the images.
{
  "ImageDates": {
    "queryDates": [
      "2017-02-27",
      "2018-06-02",
      "2016-07-13",
      "2018-04-31",
      "2017-02-17"
    ]
  },
  "NasaUrl": {
    "url": "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol=1000&camera=fhaz&api_key=DEMO_KEY&"
  },
  "SaveLocation": {
    "path" : "d:\\saveimages"
  }
}
