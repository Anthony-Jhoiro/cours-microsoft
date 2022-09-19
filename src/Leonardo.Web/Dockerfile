FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build 

WORKDIR /src  
COPY . . 

RUN dotnet publish "./src/Leonardo.Web/Leonardo.Web.csproj" -c Release -r linux-musl-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true /p:PublishReadyToRun=true -o /publish  

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine3.16 AS final 
WORKDIR /app  
COPY --from=build /publish . 
ENTRYPOINT ["/app/Leonardo.Web"]