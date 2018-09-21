#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM microsoft/dotnet:2.1-runtime-nanoserver-1803 AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY NasaPhotoReader/NasaPhotoReader.csproj NasaPhotoReader/
COPY NasaPhotoReader.Utilities/NasaPhotoReader.Utilities.csproj NasaPhotoReader.Utilities/
COPY NasaPhotos.Models/NasaPhotos.Models.csproj NasaPhotos.Models/
RUN dotnet restore NasaPhotoReader/NasaPhotoReader.csproj
COPY . .
WORKDIR /src/NasaPhotoReader
RUN dotnet build NasaPhotoReader.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish NasaPhotoReader.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NasaPhotoReader.dll"]
